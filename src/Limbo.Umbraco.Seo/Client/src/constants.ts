/**
 * Aliases shared between the property editor schemas (declared in C#) and the property editor UIs
 * (declared in `manifests.ts`).
 *
 * The UI aliases are deliberately identical to the schema aliases: when a site is upgraded from
 * Umbraco 13, Umbraco's data type migration copies the old editor alias into the new `EditorUiAlias`
 * column, so reusing the same string keeps existing data types working without a custom migration.
 */
export const LIMBO_SEO_PREVIEW_ALIAS = 'Limbo.Umbraco.Seo.Preview';
export const LIMBO_SEO_SITEMAP_CHANGE_FREQUENCY_ALIAS = 'Limbo.Umbraco.Seo.SitemapChangeFrequency';
export const LIMBO_SEO_SITEMAP_PRIORITY_ALIAS = 'Limbo.Umbraco.Seo.SitemapPriority';

/** The change frequency values supported by the sitemap protocol. */
export const SITEMAP_CHANGE_FREQUENCIES = [
    '',
    'always',
    'hourly',
    'daily',
    'weekly',
    'monthly',
    'yearly',
    'never'
] as const;

export type SitemapChangeFrequency = (typeof SITEMAP_CHANGE_FREQUENCIES)[number];

/** The priorities offered by the sitemap priority editor. */
export const SITEMAP_PRIORITIES = [0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1] as const;

/** The priority applied when a page has no explicit priority. */
export const SITEMAP_DEFAULT_PRIORITY = 0.5;
