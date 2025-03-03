# Sitemaps

The package will generate a sitemap that is available `/sitemap.xml`, and it will list pages under the site node.

## Properties

By default the sitemap is backed by the `SitemapService` class, which looks for a number of properties that you can then use to control the sitemap:

- `hideFromSitemap`  
Add a boolean value to the page. If this property is `true` for a given page, the page will be ignored in the sitemap.

- `sitemapPageChangeFrequency`  
Combine this with our [**Sitemap Change Frequency**](./../property-editors/sitemap-change-frequency/) property editor, and users can specify the change frequency of each page, which will then be included in the sitemap.

- `sitemapPagePriority`  
Combine this our [**Sitemap Priority**](./../property-editors/sitemap-page-priority/) property editor to let users specify the priority of each page. This will instruct (read: suggest) search engines how often to check your pages.