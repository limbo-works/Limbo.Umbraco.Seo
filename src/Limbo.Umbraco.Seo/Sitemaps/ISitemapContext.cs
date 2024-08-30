using Microsoft.AspNetCore.Http;
using Limbo.Umbraco.Seo.Sites;

namespace Limbo.Umbraco.Seo.Sitemaps;

/// <summary>
/// Interface a sitemap context.
/// </summary>
public interface ISitemapContext {

    /// <summary>
    /// Gets a reference to the current <see cref="HttpContext"/>.
    /// </summary>
    HttpContext HttpContext { get; }

    /// <summary>
    /// Gets a reference to the site.
    /// </summary>
    public ISite Site { get; }

}