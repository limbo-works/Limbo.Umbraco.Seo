import { html as D, css as O, state as S, property as U, customElement as B } from "@umbraco-cms/backoffice/external/lit";
import { UmbLitElement as N } from "@umbraco-cms/backoffice/lit-element";
import { UMB_PROPERTY_DATASET_CONTEXT as $ } from "@umbraco-cms/backoffice/property";
var G = Object.defineProperty, R = Object.getOwnPropertyDescriptor, x = (t) => {
  throw TypeError(t);
}, w = (t, e, s, i) => {
  for (var a = i > 1 ? void 0 : i ? R(e, s) : e, c = t.length - 1, A; c >= 0; c--)
    (A = t[c]) && (a = (i ? A(e, s, a) : A(a)) || a);
  return i && a && G(e, s, a), a;
}, P = (t, e, s) => e.has(t) || x("Cannot " + s), r = (t, e, s) => (P(t, e, "read from private field"), s ? s.call(t) : e.get(t)), n = (t, e, s) => e.has(t) ? x("Cannot add the same private member more than once") : e instanceof WeakSet ? e.add(t) : e.set(t, s), f = (t, e, s, i) => (P(t, e, "write to private field"), e.set(t, s), s), l = (t, e, s) => (P(t, e, "access private method"), s), I = (t, e, s, i) => ({
  set _(a) {
    f(t, e, a);
  },
  get _() {
    return r(t, e, i);
  }
}), _, y, u, v, g, d, h, m, o, b, L, E, M, T;
const k = ["seoTitle", "title"], C = ["seoMetaDescription", "teaser", "introTeaser"], V = 60, z = 160;
function W(t, e) {
  const s = (t ?? "").split(",").map((i) => i.trim()).filter((i) => i.length > 0);
  return s.length > 0 ? s : e;
}
let p = class extends N {
  constructor() {
    super(), n(this, o), n(this, _), n(this, y), n(this, u, k), n(this, v, C), n(this, g, !1), n(this, d, /* @__PURE__ */ new Map()), n(this, h, /* @__PURE__ */ new Set()), n(this, m, 0), this._name = "", this._title = "", this._description = "", this.consumeContext($, (t) => {
      f(this, _, t ?? void 0), l(this, o, L).call(this), r(this, _) && (this.observe(
        r(this, _).name,
        (e) => {
          this._name = e ?? "", l(this, o, M).call(this);
        },
        "_limboSeoName"
      ), l(this, o, b).call(this));
    });
  }
  set config(t) {
    t && (f(this, y, t), f(this, u, W(t.getValueByAlias("title"), k)), f(this, v, W(t.getValueByAlias("description"), C)), f(this, g, t.getValueByAlias("removeAsteriscs") === !0), l(this, o, b).call(this));
  }
  get config() {
    return r(this, y);
  }
  render() {
    return D`
            <div class="preview-window">
                <p class="title">${l(this, o, T).call(this, this._title, V)}</p>
                <p class="description">${l(this, o, T).call(this, this._description, z)}</p>
            </div>
        `;
  }
};
_ = /* @__PURE__ */ new WeakMap();
y = /* @__PURE__ */ new WeakMap();
u = /* @__PURE__ */ new WeakMap();
v = /* @__PURE__ */ new WeakMap();
g = /* @__PURE__ */ new WeakMap();
d = /* @__PURE__ */ new WeakMap();
h = /* @__PURE__ */ new WeakMap();
m = /* @__PURE__ */ new WeakMap();
o = /* @__PURE__ */ new WeakSet();
b = async function() {
  const t = r(this, _);
  if (!t) return;
  const e = ++I(this, m)._, s = /* @__PURE__ */ new Set([...r(this, u), ...r(this, v)]);
  for (const i of r(this, h))
    s.has(i) || (this.removeUmbControllerByAlias(`_limboSeoProperty_${i}`), r(this, h).delete(i), r(this, d).delete(i));
  for (const i of s) {
    const a = await t.propertyValueByAlias(i);
    if (e !== r(this, m)) return;
    a && (r(this, h).add(i), this.observe(
      a,
      (c) => {
        r(this, d).set(i, typeof c == "string" ? c : ""), l(this, o, M).call(this);
      },
      `_limboSeoProperty_${i}`
    ));
  }
};
L = function() {
  I(this, m)._++;
  for (const t of r(this, h))
    this.removeUmbControllerByAlias(`_limboSeoProperty_${t}`);
  r(this, h).clear(), r(this, d).clear();
};
E = function(t) {
  for (const e of t) {
    const s = r(this, d).get(e);
    if (s) return s;
  }
  return "";
};
M = function() {
  let t = l(this, o, E).call(this, r(this, u)) || this._name;
  r(this, g) && (t = t.replace(/\*/g, "")), this._title = t, this._description = l(this, o, E).call(this, r(this, v));
};
T = function(t, e) {
  return t.length > e ? `${t.substring(0, e).trimEnd()}…` : t;
};
p.styles = [
  O`
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
w([
  S()
], p.prototype, "_name", 2);
w([
  S()
], p.prototype, "_title", 2);
w([
  S()
], p.prototype, "_description", 2);
w([
  U({ attribute: !1 })
], p.prototype, "config", 1);
p = w([
  B("limbo-seo-preview")
], p);
export {
  p as default
};
//# sourceMappingURL=preview.element.js.map
