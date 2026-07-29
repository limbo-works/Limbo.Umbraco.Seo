const e = "Limbo.Umbraco.Seo.Preview", i = "Limbo.Umbraco.Seo.SitemapChangeFrequency", o = "Limbo.Umbraco.Seo.SitemapPriority", t = "Limbo.Umbraco.Seo.PropertyEditorUi.Preview", r = "Limbo.Umbraco.Seo.PropertyEditorUi.SitemapChangeFrequency", a = "Limbo.Umbraco.Seo.PropertyEditorUi.SitemapPriority", p = [
  "",
  "always",
  "hourly",
  "daily",
  "weekly",
  "monthly",
  "yearly",
  "never"
], m = [0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1], s = 0.5, l = [
  // Localization
  {
    type: "localization",
    alias: "Limbo.Umbraco.Seo.Localization.En",
    name: "Limbo SEO English",
    meta: { culture: "en" },
    js: () => import("./en.js")
  },
  // SEO preview
  {
    type: "propertyEditorSchema",
    alias: e,
    name: "Limbo SEO Preview",
    meta: {
      defaultPropertyEditorUiAlias: t,
      settings: {
        properties: [
          {
            alias: "title",
            label: "Title properties",
            description: "Specify a comma separated list of properties that should be used for determining the page's SEO title.",
            propertyEditorUiAlias: "Umb.PropertyEditorUi.TextBox",
            weight: 10
          },
          {
            alias: "description",
            label: "Description properties",
            description: "Specify a comma separated list of properties that should be used for determining the page's SEO description.",
            propertyEditorUiAlias: "Umb.PropertyEditorUi.TextBox",
            weight: 20
          },
          {
            alias: "removeAsteriscs",
            label: "Remove asteriscs?",
            description: "Feature used by Limbo. Should probably be documented.",
            propertyEditorUiAlias: "Umb.PropertyEditorUi.Toggle",
            weight: 30
          }
        ]
      }
    }
  },
  {
    type: "propertyEditorUi",
    alias: t,
    name: "Limbo SEO Preview",
    element: () => import("./preview.element.js"),
    meta: {
      label: "Limbo SEO Preview",
      icon: "icon-chart",
      group: "Limbo",
      propertyEditorSchemaAlias: e
    }
  },
  // Sitemap change frequency
  {
    type: "propertyEditorSchema",
    alias: i,
    name: "Limbo Sitemap Change Frequency",
    // No settings: SitemapFrequencyEditor deliberately doesn't expose a configuration editor,
    // matching the Umbraco 13 version of this package.
    meta: {
      defaultPropertyEditorUiAlias: r
    }
  },
  {
    type: "propertyEditorUi",
    alias: r,
    name: "Limbo Sitemap Change Frequency",
    element: () => import("./sitemap-change-frequency.element.js"),
    meta: {
      label: "Limbo Sitemap Change Frequency",
      icon: "icon-timer",
      group: "Limbo",
      propertyEditorSchemaAlias: i
    }
  },
  // Sitemap priority
  {
    type: "propertyEditorSchema",
    alias: o,
    name: "Limbo Sitemap Priority",
    meta: {
      defaultPropertyEditorUiAlias: a
    }
  },
  {
    type: "propertyEditorUi",
    alias: a,
    name: "Limbo Sitemap Priority",
    element: () => import("./sitemap-priority.element.js"),
    meta: {
      label: "Limbo Sitemap Priority",
      icon: "icon-caps-lock",
      group: "Limbo",
      propertyEditorSchemaAlias: o
    }
  }
];
export {
  p as S,
  m as a,
  s as b,
  l as m
};
//# sourceMappingURL=manifests.js.map
