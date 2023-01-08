using Microsoft.AspNetCore.Http;

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Interface describing a helper for building XML sitemaps.
    /// </summary>
    public interface ISitemapHelper {

        /// <summary>
        /// Returns an instance of <see cref="ISitemapResult"/> for the specified HTTP <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>An instance of <see cref="ISitemapResult"/>.</returns>
        ISitemapResult BuildSitemap(HttpContext context);

    }

}