import { repeat as y, html as m, css as b, property as l, customElement as E } from "@umbraco-cms/backoffice/external/lit";
import { UmbLitElement as S } from "@umbraco-cms/backoffice/lit-element";
import { UmbChangeEvent as P } from "@umbraco-cms/backoffice/event";
import { UMB_VALIDATION_EMPTY_LOCALIZATION_KEY as g, UmbFormControlMixin as I } from "@umbraco-cms/backoffice/validation";
import { a as M, b as O } from "./manifests.js";
var T = Object.defineProperty, A = Object.getOwnPropertyDescriptor, c = (e) => {
  throw TypeError(e);
}, s = (e, t, r, n) => {
  for (var a = n > 1 ? void 0 : n ? A(t, r) : t, p = e.length - 1, u; p >= 0; p--)
    (u = e[p]) && (a = (n ? u(t, r, a) : u(a)) || a);
  return n && a && T(t, r, a), a;
}, v = (e, t, r) => t.has(e) || c("Cannot " + r), L = (e, t, r) => (v(e, t, "read from private field"), r ? r.call(e) : t.get(e)), w = (e, t, r) => t.has(e) ? c("Cannot add the same private member more than once") : t instanceof WeakSet ? t.add(e) : t.set(e, r), d = (e, t, r) => (v(e, t, "access private method"), r), i, f, h, _;
const x = I(
  S,
  void 0
);
let o = class extends x {
  constructor() {
    super(), w(this, i), this.readonly = !1, this.mandatoryMessage = g, this.addValidator(
      "valueMissing",
      () => this.mandatoryMessage,
      () => !!this.mandatory && this.value === void 0
    );
  }
  set value(e) {
    super.value = d(this, i, h).call(this, e);
  }
  get value() {
    return super.value;
  }
  render() {
    const e = L(this, i, f);
    return m`
            <div class="limbo-seo-button-list" role="group">
                ${y(
      M,
      (t) => t,
      (t) => m`
                        <uui-button
                            type="button"
                            look=${t === e ? "primary" : "outline"}
                            color=${t === e ? "positive" : "default"}
                            label=${t.toFixed(1)}
                            ?disabled=${this.readonly}
                            @click=${() => d(this, i, _).call(this, t)}></uui-button>
                    `
    )}
            </div>
        `;
  }
};
i = /* @__PURE__ */ new WeakSet();
f = function() {
  return this.value ?? O;
};
h = function(e) {
  if (typeof e == "number" && Number.isFinite(e)) return e;
  if (typeof e == "string" && e.trim() !== "") {
    const t = Number.parseFloat(e);
    if (Number.isFinite(t)) return t;
  }
};
_ = function(e) {
  this.readonly || e === this.value || (this.value = e, this.dispatchEvent(new P()));
};
o.styles = [
  b`
            .limbo-seo-button-list {
                display: flex;
                flex-wrap: wrap;
                gap: var(--uui-size-space-2, 6px);
            }
        `
];
s([
  l({ type: Number })
], o.prototype, "value", 1);
s([
  l({ type: Boolean, reflect: !0 })
], o.prototype, "readonly", 2);
s([
  l({ type: Boolean })
], o.prototype, "mandatory", 2);
s([
  l({ type: String })
], o.prototype, "mandatoryMessage", 2);
o = s([
  E("limbo-seo-sitemap-priority")
], o);
export {
  o as default
};
//# sourceMappingURL=sitemap-priority.element.js.map
