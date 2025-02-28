using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps.Constants;

/// <summary>
/// Static class with various constants for building XML sitemaps.
/// </summary>
public static class SitemapConstants {

    /// <summary>
    /// Gets name of the sitemap XML namespace.
    /// </summary>
    public const string XmlNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

    /// <summary>
    /// Gets an instance of <see cref="XNamespace"/> representing the XML namespace.
    /// </summary>
    public static readonly XNamespace XNamespace = XmlNamespace;

    /// <summary>
    /// Static class with default property aliases used by this package.
    /// </summary>
    public static class Properties {

        /// <summary>
        /// Gets the default alias of the <strong>Hide from sitemap</strong> property.
        /// </summary>
        public const string HideFromSitemap = "hideFromSitemap";

        /// <summary>
        /// Gets the default alias of the <strong>Sitemap page change frequency</strong> property.
        /// </summary>
        public const string ChangeFrequency = "sitemapPageChangeFrequency";

        /// <summary>
        /// Gets the default alias of the <strong>Sitemap page priority</strong> property.
        /// </summary>
        public const string Priority = "sitemapPagePriority";

    }

    /// <summary>
    /// Static class with XML element names.
    /// </summary>
    public static class Xml {

        /// <summary>
        /// Equals <c>changefreq</c>.
        /// </summary>
        public const string ChangeFrequency = "changefreq";

        /// <summary>
        /// Equals <c>lastmod</c>.
        /// </summary>
        public const string LastModified = "lastmod";

        /// <summary>
        /// Equals <c>loc</c>.
        /// </summary>
        public const string Location = "loc";

        /// <summary>
        /// Equals <c>priority</c>.
        /// </summary>
        public const string Priority = "priority";

        /// <summary>
        /// Equals <c>url</c>.
        /// </summary>
        public const string Url = "url";

        /// <summary>
        /// Equals <c>urlset</c>.
        /// </summary>
        public const string UrlSet = "urlset";

    }

}