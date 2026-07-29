import { css, customElement, html, property, repeat } from '@umbraco-cms/backoffice/external/lit';
import { UmbLitElement } from '@umbraco-cms/backoffice/lit-element';
import { UmbChangeEvent } from '@umbraco-cms/backoffice/event';
import { UMB_VALIDATION_EMPTY_LOCALIZATION_KEY, UmbFormControlMixin } from '@umbraco-cms/backoffice/validation';
import type { UmbPropertyEditorUiElement } from '@umbraco-cms/backoffice/property-editor';

import { SITEMAP_CHANGE_FREQUENCIES, type SitemapChangeFrequency } from '../constants.js';

const LimboSeoSitemapChangeFrequencyElementBase = UmbFormControlMixin<
    SitemapChangeFrequency | string,
    typeof UmbLitElement,
    undefined
>(UmbLitElement, undefined);

/**
 * Lets an editor pick the sitemap change frequency of a page.
 *
 * Replaces the AngularJS `Limbo.Umbraco.Seo.SitemapChangeFrequency` controller and its
 * `SitemapChangeFrequency.html` view from the Umbraco 13 version of this package. The stored value
 * is unchanged, so existing content keeps working.
 *
 * A property that has never been saved arrives here as `null` or `undefined` rather than as the
 * empty string the "Unspecified" button carries, so incoming values are normalised. Without that,
 * an unsaved property renders with no button selected at all.
 */
@customElement('limbo-seo-sitemap-change-frequency')
export default class LimboSeoSitemapChangeFrequencyElement extends LimboSeoSitemapChangeFrequencyElementBase implements UmbPropertyEditorUiElement {

    @property({ type: String })
    public override set value(value: SitemapChangeFrequency | string | undefined) {
        super.value = value ?? '';
    }
    public override get value(): SitemapChangeFrequency | string {
        return super.value ?? '';
    }

    @property({ type: Boolean, reflect: true })
    public readonly = false;

    @property({ type: Boolean })
    public mandatory?: boolean;

    @property({ type: String })
    public mandatoryMessage = UMB_VALIDATION_EMPTY_LOCALIZATION_KEY;

    constructor() {
        super();
        // "Unspecified" is stored as an empty string, so a mandatory property is only satisfied by
        // one of the real frequencies.
        this.addValidator(
            'valueMissing',
            () => this.mandatoryMessage,
            () => !!this.mandatory && !this.value
        );
    }

    #onSelect(frequency: SitemapChangeFrequency) {
        if (this.readonly || frequency === this.value) return;
        this.value = frequency;
        this.dispatchEvent(new UmbChangeEvent());
    }

    #label(frequency: SitemapChangeFrequency): string {
        return this.localize.term(`limboSeo_frequency_${frequency === '' ? 'unspecified' : frequency}`);
    }

    override render() {
        const selected = this.value;
        return html`
            <div class="limbo-seo-button-list" role="group">
                ${repeat(
                    SITEMAP_CHANGE_FREQUENCIES,
                    (frequency) => frequency,
                    (frequency) => html`
                        <uui-button
                            type="button"
                            look=${frequency === selected ? 'primary' : 'outline'}
                            color=${frequency === selected ? 'positive' : 'default'}
                            label=${this.#label(frequency)}
                            ?disabled=${this.readonly}
                            @click=${() => this.#onSelect(frequency)}></uui-button>
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
        'limbo-seo-sitemap-change-frequency': LimboSeoSitemapChangeFrequencyElement;
    }
}
