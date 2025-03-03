# Sitemaps

The sitemap module isn't enabled by default. You enable it by adding the following line to your `Program.cs` file:

```csharp
app.UseMiddleware<SitemapMiddleware>();
```

The `SitemapMiddleware` class can be found in the `Limbo.Umbraco.Seo.Sitemaps` namespace. When enabled, the sitemap be found at `/sitemap.xml`.

## Properties

By default the sitemap module is backed by the `SitemapService` class, which looks for a number of properties that you can then use to control the sitemap:

- `hideFromSitemap`  
Add a boolean value to the page. If this property is `true` for a given page, the page will be ignored in the sitemap.

- `sitemapPageChangeFrequency`  
Combine this with our [**Sitemap Change Frequency**](./../property-editors/sitemap-change-frequency/) property editor, and users can specify the change frequency of each page, which will then be included in the sitemap.

- `sitemapPagePriority`  
Combine this our [**Sitemap Priority**](./../property-editors/sitemap-page-priority/) property editor to let users specify the priority of each page. This will instruct (read: suggest) search engines how often to check your pages.