# Robots

The robots module isn't enabled by default. You enable it by adding the following line to your `Program.cs` file:

```csharp
app.UseMiddleware<RobotsMiddleware>();
```

The `RobotsMiddleware` class can be found in the `Limbo.Umbraco.Seo.Robots` namespace. When enabled, the robots file can be found at `/robots.txt`.

## Properties

By default the sitemap module is backed by the `SitemapService` class, which looks for a number of properties that you can then use to control the sitemap:

- `robotsTxt`  
Add a textarea property with this alias to the site node to allow users to add custom rules to the `robots.txt` file.