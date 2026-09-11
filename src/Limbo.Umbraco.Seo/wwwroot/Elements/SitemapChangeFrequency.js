import { css, html, repeat } from "@umbraco-cms/backoffice/external/lit";
import { UmbLitElement } from "@umbraco-cms/backoffice/lit-element";
import { UmbChangeEvent } from "@umbraco-cms/backoffice/event";
import { UMB_VALIDATION_EMPTY_LOCALIZATION_KEY, UmbFormControlMixin } from "@umbraco-cms/backoffice/validation";

import { SITEMAP_CHANGE_FREQUENCIES } from "@limbo/seo/constants";

const LimboSeoSitemapChangeFrequencyElementBase = UmbFormControlMixin(
    UmbLitElement,
    undefined
);

export default class LimboSeoSitemapChangeFrequencyElement extends LimboSeoSitemapChangeFrequencyElementBase {

    set value(value) {
        super.value = value ?? "";
    }

    get value() {
        return super.value ?? "";
    }

    readonly = false;
    mandatory;
    mandatoryMessage = UMB_VALIDATION_EMPTY_LOCALIZATION_KEY;

    constructor() {
        super();
        this.addValidator(
            "valueMissing",
            () => this.mandatoryMessage,
            () => !!this.mandatory && !this.value
        );
    }

    #onSelect(frequency) {
        if (this.readonly || frequency === this.value) return;
        this.value = frequency;
        this.dispatchEvent(new UmbChangeEvent());
    }

    #label(frequency) {
        return this.localize.term(`limboSeo_frequency_${frequency === "" ? "unspecified" : frequency}`);
    }

    render() {
        const selected = this.value;
        return html`
            <div class="limbo-seo-button-list" role="group">
                ${repeat(SITEMAP_CHANGE_FREQUENCIES, (frequency) => frequency, (frequency) => html`
                    <uui-button
                        type="button"
                        look=${frequency === selected ? "primary" : "outline"}
                        color=${frequency === selected ? "positive" : "default"}
                        label=${this.#label(frequency)}
                        ?disabled=${this.readonly}
                        @click=${() => this.#onSelect(frequency)}></uui-button>
                `)}
            </div>
        `;
    }

    static styles = [
        css`
            .limbo-seo-button-list {
                display: flex;
                flex-wrap: wrap;
                gap: var(--uui-size-space-2, 6px);
            }
        `
    ];

}

customElements.define("limbo-seo-sitemap-change-frequency", LimboSeoSitemapChangeFrequencyElement);