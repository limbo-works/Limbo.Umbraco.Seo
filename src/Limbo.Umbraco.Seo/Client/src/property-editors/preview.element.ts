import { css, customElement, html, property, state } from '@umbraco-cms/backoffice/external/lit';
import { UmbLitElement } from '@umbraco-cms/backoffice/lit-element';
import { UMB_PROPERTY_DATASET_CONTEXT } from '@umbraco-cms/backoffice/property';
import type { UmbPropertyDatasetContext } from '@umbraco-cms/backoffice/property';
import type {
    UmbPropertyEditorConfigCollection,
    UmbPropertyEditorUiElement
} from '@umbraco-cms/backoffice/property-editor';

/** Aliases used for the SEO title when the data type doesn't configure any. */
const DEFAULT_TITLE_ALIASES = ['seoTitle', 'title'];

/** Aliases used for the SEO description when the data type doesn't configure any. */
const DEFAULT_DESCRIPTION_ALIASES = ['seoMetaDescription', 'teaser', 'introTeaser'];

/** Google truncates around these lengths, so the preview does too. */
const TITLE_MAX_LENGTH = 60;
const DESCRIPTION_MAX_LENGTH = 160;

function parseAliases(value: string | undefined, fallback: string[]): string[] {
    const aliases = (value ?? '')
        .split(',')
        .map((alias) => alias.trim())
        .filter((alias) => alias.length > 0);
    return aliases.length > 0 ? aliases : fallback;
}

/**
 * Renders a read-only preview of how the page is likely to appear in a search result.
 *
 * Replaces the AngularJS `Limbo.Umbraco.Seo.Preview` controller from the Umbraco 13 version of this
 * package. Where the old controller reached into `editorState.current.variants[0].tabs[*]` to find
 * sibling properties, this reads them through the property dataset context, which means it follows
 * the variant the editor is actually working on rather than always the first one.
 */
@customElement('limbo-seo-preview')
export default class LimboSeoPreviewElement extends UmbLitElement implements UmbPropertyEditorUiElement {

    #datasetContext?: UmbPropertyDatasetContext;
    #config?: UmbPropertyEditorConfigCollection;
    #titleAliases: string[] = DEFAULT_TITLE_ALIASES;
    #descriptionAliases: string[] = DEFAULT_DESCRIPTION_ALIASES;
    #removeAsteriscs = false;

    /** Values of the observed sibling properties, keyed by alias. */
    #values = new Map<string, string>();

    /** Aliases currently subscribed to, so subscriptions can be dropped when the config changes. */
    #observedAliases = new Set<string>();

    /**
     * Incremented on every `#observeProperties` run and whenever the dataset context changes.
     * `propertyValueByAlias` is async, so without this an in-flight run could register an observable
     * belonging to a dataset context (variant) that has since been replaced, leaving the preview
     * showing the previous variant's values.
     */
    #observeToken = 0;

    @state()
    private _name = '';

    @state()
    private _title = '';

    @state()
    private _description = '';

    @property({ attribute: false })
    public set config(config: UmbPropertyEditorConfigCollection | undefined) {
        if (!config) return;
        this.#config = config;
        this.#titleAliases = parseAliases(config.getValueByAlias<string>('title'), DEFAULT_TITLE_ALIASES);
        this.#descriptionAliases = parseAliases(config.getValueByAlias<string>('description'), DEFAULT_DESCRIPTION_ALIASES);
        this.#removeAsteriscs = config.getValueByAlias<boolean>('removeAsteriscs') === true;
        this.#observeProperties();
    }
    public get config(): UmbPropertyEditorConfigCollection | undefined {
        return this.#config;
    }

    constructor() {
        super();
        this.consumeContext(UMB_PROPERTY_DATASET_CONTEXT, (context) => {
            this.#datasetContext = context ?? undefined;
            this.#forgetProperties();
            if (!this.#datasetContext) return;
            this.observe(
                this.#datasetContext.name,
                (name) => {
                    this._name = name ?? '';
                    this.#update();
                },
                '_limboSeoName'
            );
            this.#observeProperties();
        });
    }

    /**
     * Subscribes to every alias the data type is configured with. Aliases that don't exist on the
     * content type simply never emit a value, which is why the fallback chain is resolved in
     * `#update` rather than here.
     */
    async #observeProperties() {
        const context = this.#datasetContext;
        if (!context) return;

        const token = ++this.#observeToken;
        const aliases = new Set([...this.#titleAliases, ...this.#descriptionAliases]);

        // Drop aliases that a reconfigured data type no longer refers to, so their last known
        // values can't keep feeding the preview.
        for (const alias of this.#observedAliases) {
            if (aliases.has(alias)) continue;
            this.removeUmbControllerByAlias(`_limboSeoProperty_${alias}`);
            this.#observedAliases.delete(alias);
            this.#values.delete(alias);
        }

        for (const alias of aliases) {
            const observable = await context.propertyValueByAlias<unknown>(alias);
            // A newer run — or a changed dataset context — superseded this one while awaiting.
            if (token !== this.#observeToken) return;
            if (!observable) continue;
            this.#observedAliases.add(alias);
            this.observe(
                observable,
                (value) => {
                    this.#values.set(alias, typeof value === 'string' ? value : '');
                    this.#update();
                },
                `_limboSeoProperty_${alias}`
            );
        }
    }

    /** Drops every property subscription, used when the dataset context itself changes. */
    #forgetProperties() {
        // Abort any run still awaiting an observable from the outgoing dataset context.
        this.#observeToken++;
        for (const alias of this.#observedAliases) {
            this.removeUmbControllerByAlias(`_limboSeoProperty_${alias}`);
        }
        this.#observedAliases.clear();
        this.#values.clear();
    }

    /** Picks the first non-empty value in each configured alias chain. */
    #first(aliases: string[]): string {
        for (const alias of aliases) {
            const value = this.#values.get(alias);
            if (value) return value;
        }
        return '';
    }

    #update() {
        let title = this.#first(this.#titleAliases) || this._name;
        if (this.#removeAsteriscs) title = title.replace(/\*/g, '');
        this._title = title;
        this._description = this.#first(this.#descriptionAliases);
    }

    /** Mirrors how a search engine shortens overlong text, ellipsis included. */
    #truncate(value: string, maxLength: number): string {
        return value.length > maxLength ? `${value.substring(0, maxLength).trimEnd()}…` : value;
    }

    override render() {
        return html`
            <div class="preview-window">
                <p class="title">${this.#truncate(this._title, TITLE_MAX_LENGTH)}</p>
                <p class="description">${this.#truncate(this._description, DESCRIPTION_MAX_LENGTH)}</p>
            </div>
        `;
    }

    static override styles = [
        css`
            :host {
                display: block;
            }

            /*
             * The colours below are Google's, not Umbraco's, and are intentionally fixed rather
             * than themed: the point of this editor is to show how the page will look in a search
             * result, which is a light surface regardless of the backoffice theme. It is styled as
             * a card so it reads as an embedded preview rather than as part of the surrounding UI.
             */
            .preview-window {
                background: #ffffff;
                border: 1px solid rgba(136, 136, 136, 0.18);
                border-radius: var(--uui-border-radius, 3px);
                padding: var(--uui-size-space-4, 12px);
                max-width: 600px;
            }

            .title {
                color: #1a0dab;
                font-family: arial, sans-serif;
                font-size: medium;
                font-weight: normal;
                margin: 0;
                overflow-wrap: anywhere;
            }

            .description {
                color: #545454;
                font-family: arial, sans-serif;
                font-size: small;
                font-weight: normal;
                margin: 0;
                overflow-wrap: anywhere;
            }
        `
    ];

}

declare global {
    interface HTMLElementTagNameMap {
        'limbo-seo-preview': LimboSeoPreviewElement;
    }
}
