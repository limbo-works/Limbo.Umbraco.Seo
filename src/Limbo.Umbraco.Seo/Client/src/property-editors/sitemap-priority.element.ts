import { css, customElement, html, property, repeat } from '@umbraco-cms/backoffice/external/lit';
import { UmbLitElement } from '@umbraco-cms/backoffice/lit-element';
import { UmbChangeEvent } from '@umbraco-cms/backoffice/event';
import { UMB_VALIDATION_EMPTY_LOCALIZATION_KEY, UmbFormControlMixin } from '@umbraco-cms/backoffice/validation';
import type { UmbPropertyEditorUiElement } from '@umbraco-cms/backoffice/property-editor';

import { SITEMAP_DEFAULT_PRIORITY, SITEMAP_PRIORITIES } from '../constants.js';

const LimboSeoSitemapPriorityElementBase = UmbFormControlMixin<number, typeof UmbLitElement, undefined>(
    UmbLitElement,
    undefined
);

/**
 * Lets an editor pick the sitemap priority of a page.
 *
 * Replaces the AngularJS `Limbo.Umbraco.Seo.SitemapPriority` controller and its
 * `SitemapPriority.html` view from the Umbraco 13 version of this package.
 *
 * The schema stores this property as a decimal, but content saved by older versions of the package
 * may still hold a string such as `"0.5"`. Incoming values are therefore normalised to a number,
 * which matches what `SitemapPriorityValueConverter` expects on the server.
 *
 * A property that has never been saved holds no value at all, and that state is kept distinct from
 * an explicitly saved `0.5`. The default priority only decides which button *looks* selected; it is
 * never handed back to Umbraco as the value. Folding it into the value instead would make the
 * editor claim a value the server doesn't have — `SitemapPriorityValueConverter` maps a missing
 * value to `null`, not to `0.5` — and would leave the default button unclickable, since selecting
 * the value the editor already appears to hold is a no-op and so never dispatches a change event.
 */
@customElement('limbo-seo-sitemap-priority')
export default class LimboSeoSitemapPriorityElement extends LimboSeoSitemapPriorityElementBase implements UmbPropertyEditorUiElement {

    @property({ type: Number })
    public override set value(value: number | undefined) {
        super.value = this.#parse(value);
    }
    public override get value(): number | undefined {
        return super.value;
    }

    @property({ type: Boolean, reflect: true })
    public readonly = false;

    @property({ type: Boolean })
    public mandatory?: boolean;

    @property({ type: String })
    public mandatoryMessage = UMB_VALIDATION_EMPTY_LOCALIZATION_KEY;

    constructor() {
        super();
        // Checked against undefined rather than falsiness: 0 is a legitimate priority.
        this.addValidator(
            'valueMissing',
            () => this.mandatoryMessage,
            () => !!this.mandatory && this.value === undefined
        );
    }

    /** The priority the buttons render as selected, which falls back to the default when unsaved. */
    get #selected(): number {
        return this.value ?? SITEMAP_DEFAULT_PRIORITY;
    }

    /**
     * Accepts the numbers the schema stores today as well as the strings older versions of this
     * package wrote, and keeps anything unparseable as "no value".
     */
    #parse(value: number | undefined): number | undefined {
        if (typeof value === 'number' && Number.isFinite(value)) return value;
        if (typeof value === 'string' && (value as string).trim() !== '') {
            const parsed = Number.parseFloat(value as string);
            if (Number.isFinite(parsed)) return parsed;
        }
        return undefined;
    }

    #onSelect(priority: number) {
        if (this.readonly || priority === this.value) return;
        this.value = priority;
        this.dispatchEvent(new UmbChangeEvent());
    }

    override render() {
        const selected = this.#selected;
        return html`
            <div class="limbo-seo-button-list" role="group">
                ${repeat(
                    SITEMAP_PRIORITIES,
                    (priority) => priority,
                    (priority) => html`
                        <uui-button
                            type="button"
                            look=${priority === selected ? 'primary' : 'outline'}
                            color=${priority === selected ? 'positive' : 'default'}
                            label=${priority.toFixed(1)}
                            ?disabled=${this.readonly}
                            @click=${() => this.#onSelect(priority)}></uui-button>
                    `
                )}
            </div>
        `;
    }

    static override styles = [
        css`
            .limbo-seo-button-list {
                display: flex;
                flex-wrap: wrap;
                gap: var(--uui-size-space-2, 6px);
            }
        `
    ];

}

declare global {
    interface HTMLElementTagNameMap {
        'limbo-seo-sitemap-priority': LimboSeoSitemapPriorityElement;
    }
}
