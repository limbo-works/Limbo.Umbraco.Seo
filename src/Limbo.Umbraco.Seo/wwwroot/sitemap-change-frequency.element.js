import { repeat as _, html as m, css as y, property as l, customElement as f } from "@umbraco-cms/backoffice/external/lit";
import { UmbLitElement as b } from "@umbraco-cms/backoffice/lit-element";
import { UmbChangeEvent as E } from "@umbraco-cms/backoffice/event";
import { UMB_VALIDATION_EMPTY_LOCALIZATION_KEY as S, UmbFormControlMixin as g } from "@umbraco-cms/backoffice/validation";
import { S as C } from "./manifests.js";
var M = Object.defineProperty, w = Object.getOwnPropertyDescriptor, c = (e) => {
  throw TypeError(e);
}, s = (e, t, a, i) => {
  for (var o = i > 1 ? void 0 : i ? w(t, a) : t, p = e.length - 1, u; p >= 0; p--)
    (u = e[p]) && (o = (i ? u(t, a, o) : u(o)) || o);
  return i && o && M(t, a, o), o;
}, A = (e, t, a) => t.has(e) || c("Cannot " + a), L = (e, t, a) => t.has(e) ? c("Cannot add the same private member more than once") : t instanceof WeakSet ? t.add(e) : t.set(e, a), h = (e, t, a) => (A(e, t, "access private method"), a), n, v, d;
const O = g(b, void 0);
let r = class extends O {
  constructor() {
    super(), L(this, n), this.readonly = !1, this.mandatoryMessage = S, this.addValidator(
      "valueMissing",
      () => this.mandatoryMessage,
      () => !!this.mandatory && !this.value
    );
  }
  set value(e) {
    super.value = e ?? "";
  }
  get value() {
    return super.value ?? "";
  }
  render() {
    const e = this.value;
    return m`
            <div class="limbo-seo-button-list" role="group">
                ${_(
      C,
      (t) => t,
      (t) => m`
                        <uui-button
                            type="button"
                            look=${t === e ? "primary" : "outline"}
                            color=${t === e ? "positive" : "default"}
                            label=${h(this, n, d).call(this, t)}
                            ?disabled=${this.readonly}
                            @click=${() => h(this, n, v).call(this, t)}></uui-button>
                    `
    )}
            </div>
        `;
  }
};
n = /* @__PURE__ */ new WeakSet();
v = function(e) {
  this.readonly || e === this.value || (this.value = e, this.dispatchEvent(new E()));
};
d = function(e) {
  return this.localize.term(`limboSeo_frequency_${e === "" ? "unspecified" : e}`);
};
r.styles = [
  y`
            .limbo-seo-button-list {
                display: flex;
                flex-wrap: wrap;
                gap: var(--uui-size-space-2, 6px);
            }
        `
];
s([
  l({ type: String })
], r.prototype, "value", 1);
s([
  l({ type: Boolean, reflect: !0 })
], r.prototype, "readonly", 2);
s([
  l({ type: Boolean })
], r.prototype, "mandatory", 2);
s([
  l({ type: String })
], r.prototype, "mandatoryMessage", 2);
r = s([
  f("limbo-seo-sitemap-change-frequency")
], r);
export {
  r as default
};
//# sourceMappingURL=sitemap-change-frequency.element.js.map
