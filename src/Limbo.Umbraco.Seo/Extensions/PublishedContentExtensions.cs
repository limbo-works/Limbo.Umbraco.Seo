using Limbo.Umbraco.Seo.Models.Sitemaps;
using Limbo.Umbraco.Seo.Sitemaps;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Limbo.Umbraco.Seo.Extensions {
    
    public static class PublishedContentExtensions {

        public static SitemapChangeFrequency GetSitemapChangeFrequency(this IPublishedContent content) {
            return content?.Value<SitemapChangeFrequency>(SitemapConstants.Properties.ChangeFrequency) ?? SitemapChangeFrequency.Unspecified;
        }

        public static bool TryGetSitemapChangeFrequency(this IPublishedContent content, out SitemapChangeFrequency result) {
            result = content?.Value<SitemapChangeFrequency>(SitemapConstants.Properties.ChangeFrequency) ?? SitemapChangeFrequency.Unspecified;
            return result != SitemapChangeFrequency.Unspecified;
        }

        public static float GetSitemapPriority(this IPublishedContent content) {
            return content?.Value<float>(SitemapConstants.Properties.Priority) ?? 0.5f;
        }

        public static bool TryGetSitemapPriority(this IPublishedContent content, out float result) {

            result = 0;

            if (content == null) return false;


            switch (content?.Value(SitemapConstants.Properties.Priority)) {

                case null:
                    return false;

                case float f:
                    result = f;
                    return true;

                case string str:
                    return float.TryParse(str, out result);

                default:
                    return false;
 
            }

        }

    }

}