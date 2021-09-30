using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {

    public static class SitemapConstants {

        public const string XmlNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

        public static readonly XNamespace XNamespace = XmlNamespace;

        public static class Properties {

            public const string ChangeFrequency = "sitemapPageUpdateFrequency";

            public const string Priority = "sitemapPagePriority";

        }

    }

}