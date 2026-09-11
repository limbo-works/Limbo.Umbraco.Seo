import { css, html, repeat } from "@umbraco-cms/backoffice/external/lit";
import { UmbLitElement } from "@umbraco-cms/backoffice/lit-element";
import { UmbChangeEvent } from "@umbraco-cms/backoffice/event";
import { UMB_VALIDATION_EMPTY_LOCALIZATION_KEY, UmbFormControlMixin } from "@umbraco-cms/backoffice/validation";

import { SITEMAP_DEFAULT_PRIORITY, SITEMAP_PRIORITIES } from "@limbo/seo/constants";

const LimboSeoSitemapPriorityElementBase = UmbFormControlMixin(
    UmbLitElement,
    undefined
);

export default class LimboSeoSitemapPriorityElement extends LimboSeoSitemapPriorityElementBase {

    set value(value) {
        super.value = this.#parse(value);
    }

    get value() {
        return super.value;
    }

    readonly = false;
    mandatory;
    mandatoryMessage = UMB_VALIDATION_EMPTY_LOCALIZATION_KEY;

    constructor() {
        super();
        this.addValidator(
            "valueMissing",
            () => this.mandatoryMessage,
            () => !!this.mandatory && this.value === undefined
        );
    }

    get #selected() {
        return this.value ?? SITEMAP_DEFAULT_PRIORITY;
    }

    #parse(value) {

        if (typeof value === "number" && Number.isFinite(value)) {
            return value;
        }

        if (typeof value === "string" && value.trim() !== "") {
            const parsed = Number.parseFloat(value);

            if (Number.isFinite(parsed)) {
                return parsed;
            }
        }

        return undefined;

    }

    #onSelect(priority) {
        if (this.readonly || priority === this.value) return;
        this.value = priority;
        this.dispatchEvent(new UmbChangeEvent());
    }

    render() {
        const selected = this.#selected;
        return html`
            <div class="limbo-seo-button-list" role="group">
                ${repeat(SITEMAP_PRIORITIES, (priority) => priority, (priority) => html`
                    <uui-button
                        type="button"
                        look=${priority === selected ? "primary" : "outline"}
                        color=${priority === selected ? "positive" : "default"}
                        label=${priority.toFixed(1)}
                        ?disabled=${this.readonly}
                        @click=${() => this.#onSelect(priority)}></uui-button>
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

customElements.define("limbo-seo-sitemap-priority", LimboSeoSitemapPriorityElement);