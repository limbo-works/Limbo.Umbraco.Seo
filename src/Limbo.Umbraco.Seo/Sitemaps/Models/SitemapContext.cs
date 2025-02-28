using Limbo.Umbraco.Seo.Sites;
using Microsoft.AspNetCore.Http;

namespace Limbo.Umbraco.Seo.Sitemaps.Models;

/// <summary>
/// Class representing a sitemap context.
/// </summary>
public class SitemapContext : ISitemapContext {

    #region Properties

    /// <summary>
    /// Gets a reference to the current <see cref="HttpContext"/>.
    /// </summary>
    public HttpContext HttpContext { get; }

    /// <summary>
    /// Gets a reference to the site.
    /// </summary>
    public ISite Site { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new sitemap context.
    /// </summary>
    /// <param name="context">A reference to the current <see cref="HttpContext"/>.</param>
    /// <param name="site">A reference to the site.</param>
    public SitemapContext(HttpContext context, ISite site) {
        HttpContext = context;
        Site = site;
    }

    #endregion

}