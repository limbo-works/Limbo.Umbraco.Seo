import { css, html } from "@umbraco-cms/backoffice/external/lit";
import { UmbLitElement } from "@umbraco-cms/backoffice/lit-element";
import { UMB_PROPERTY_DATASET_CONTEXT } from "@umbraco-cms/backoffice/property";

const DEFAULT_TITLE_ALIASES = ["seoTitle", "title"];
const DEFAULT_DESCRIPTION_ALIASES = ["seoMetaDescription", "teaser", "introTeaser"];

const TITLE_MAX_LENGTH = 60;
const DESCRIPTION_MAX_LENGTH = 160;

function parseAliases(value, fallback) {
    const aliases = (value ?? "")
        .split(",")
        .map((alias) => alias.trim())
        .filter((alias) => alias.length > 0);

    return aliases.length > 0 ? aliases : fallback;
}

export default class LimboSeoPreviewElement extends UmbLitElement {

    #datasetContext;
    #config;
    #titleAliases = DEFAULT_TITLE_ALIASES;
    #descriptionAliases = DEFAULT_DESCRIPTION_ALIASES;
    #removeAsteriscs = false;

    #values = new Map();
    #observedAliases = new Set();
    #observeToken = 0;

    _name = "";
    _title = "";
    _description = "";

    set config(config) {
        if (!config) return;

        this.#config = config;
        this.#titleAliases = parseAliases(
            config.getValueByAlias("title"),
            DEFAULT_TITLE_ALIASES
        );
        this.#descriptionAliases = parseAliases(
            config.getValueByAlias("description"),
            DEFAULT_DESCRIPTION_ALIASES
        );
        this.#removeAsteriscs = config.getValueByAlias("removeAsteriscs") === true;

        this.#observeProperties();
    }

    get config() {
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
                    this._name = name ?? "";
                    this.#update();
                },
                "_limboSeoName"
            );

            this.#observeProperties();
        });
    }

    async #observeProperties() {
        const context = this.#datasetContext;
        if (!context) return;

        const token = ++this.#observeToken;
        const aliases = new Set([
            ...this.#titleAliases,
            ...this.#descriptionAliases
        ]);

        for (const alias of this.#observedAliases) {
            if (aliases.has(alias)) continue;

            this.removeUmbControllerByAlias(`_limboSeoProperty_${alias}`);
            this.#observedAliases.delete(alias);
            this.#values.delete(alias);
        }

        for (const alias of aliases) {
            const observable = await context.propertyValueByAlias(alias);

            if (token !== this.#observeToken) return;
            if (!observable) continue;

            this.#observedAliases.add(alias);

            this.observe(
                observable,
                (value) => {
                    this.#values.set(alias, typeof value === "string" ? value : "");
                    this.#update();
                },
                `_limboSeoProperty_${alias}`
            );
        }
    }

    #forgetProperties() {
        this.#observeToken++;

        for (const alias of this.#observedAliases) {
            this.removeUmbControllerByAlias(`_limboSeoProperty_${alias}`);
        }

        this.#observedAliases.clear();
        this.#values.clear();
    }

    #first(aliases) {
        for (const alias of aliases) {
            const value = this.#values.get(alias);

            if (value) return value;
        }

        return "";
    }

    #update() {
        let title = this.#first(this.#titleAliases) || this._name;

        if (this.#removeAsteriscs) {
            title = title.replace(/\*/g, "");
        }

        this._title = title;
        this._description = this.#first(this.#descriptionAliases);
    }

    #truncate(value, maxLength) {
        return value.length > maxLength
            ? `${value.substring(0, maxLength).trimEnd()}…`
            : value;
    }

    render() {
        return html`
            <div class="preview-window">
                <p class="title">${this.#truncate(this._title, TITLE_MAX_LENGTH)}</p>
                <p class="description">${this.#truncate(this._description, DESCRIPTION_MAX_LENGTH)}</p>
            </div>
        `;
    }

    static styles = [
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

customElements.define("limbo-seo-preview", LimboSeoPreviewElement);