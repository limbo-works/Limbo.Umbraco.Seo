using System.Xml.Linq;
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

        /// <summary>
        /// Returns an <see cref="XDocument"/> representing the specified <paramref name="sitemap"/>.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <returns>An instance of <see cref="XDocument"/>.</returns>
        XDocument ToXmlDocument(ISitemapResult sitemap);

        /// <summary>
        /// Returns an <see cref="XElement"/> representing the specified <paramref name="sitemap"/>.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <returns>An instance of <see cref="XElement"/>.</returns>
        XElement ToXmlElement(ISitemapResult sitemap);

        /// <summary>
        /// Returns an <see cref="XElement"/> representing the specified sitemap <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The sitemap item.</param>
        /// <returns>An instance of <see cref="XElement"/>.</returns>
        XElement ToXmlElement(ISitemapItem item);

    }

}