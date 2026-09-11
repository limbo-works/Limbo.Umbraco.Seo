export const LIMBO_SEO_PREVIEW_ALIAS = "Limbo.Umbraco.Seo.Preview";
export const LIMBO_SEO_SITEMAP_CHANGE_FREQUENCY_ALIAS = "Limbo.Umbraco.Seo.SitemapChangeFrequency";
export const LIMBO_SEO_SITEMAP_PRIORITY_ALIAS = "Limbo.Umbraco.Seo.SitemapPriority";

export const LIMBO_SEO_PREVIEW_UI_ALIAS = "Limbo.Umbraco.Seo.PropertyEditorUi.Preview";
export const LIMBO_SEO_SITEMAP_CHANGE_FREQUENCY_UI_ALIAS = "Limbo.Umbraco.Seo.PropertyEditorUi.SitemapChangeFrequency";
export const LIMBO_SEO_SITEMAP_PRIORITY_UI_ALIAS = "Limbo.Umbraco.Seo.PropertyEditorUi.SitemapPriority";

/** The change frequency values supported by the sitemap protocol. */
export const SITEMAP_CHANGE_FREQUENCIES = [
    "",
    "always",
    "hourly",
    "daily",
    "weekly",
    "monthly",
    "yearly",
    "never"
];

/** The priorities offered by the sitemap priority editor. */
export const SITEMAP_PRIORITIES = [0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1];

/** The priority applied when a page has no explicit priority. */
export const SITEMAP_DEFAULT_PRIORITY = 0.5;