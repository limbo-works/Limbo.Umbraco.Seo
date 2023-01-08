using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {

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

    }

}