using Limbo.Umbraco.Seo.Models.Sitemaps;
using Limbo.Umbraco.Seo.Sitemaps;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Limbo.Umbraco.Seo.Extensions {

    /// <summary>
    /// Static class with various SEO related extension methods for <see cref="IPublishedContent"/>.
    /// </summary>
    public static class PublishedContentExtensions {

        /// <summary>
        /// Returns an instance of <see cref="SitemapChangeFrequency"/> representing the sitemap change frequency of the specified <paramref name="content"/> node.
        /// </summary>
        /// <param name="content">The content node.</param>
        /// <returns>An instance of <see cref="SitemapChangeFrequency"/>.</returns>
        public static SitemapChangeFrequency GetSitemapChangeFrequency(this IPublishedContent content) {
            return content?.Value<SitemapChangeFrequency>(SitemapConstants.Properties.ChangeFrequency) ?? SitemapChangeFrequency.Unspecified;
        }

        /// <summary>
        /// Attempts to get the <see cref="SitemapChangeFrequency"/> representing the sitemap change frequency of the specified <paramref name="content"/> node.
        /// </summary>
        /// <param name="content">The content node.</param>
        /// <param name="result">When this method returns, holds an instance of <see cref="SitemapChangeFrequency"/> if successful; otherwise, the default value of <see cref="SitemapChangeFrequency"/>.</param>
        /// <returns><see langword="true"/> if the method was successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetSitemapChangeFrequency(this IPublishedContent content, out SitemapChangeFrequency result) {
            result = content?.Value<SitemapChangeFrequency>(SitemapConstants.Properties.ChangeFrequency) ?? SitemapChangeFrequency.Unspecified;
            return result != SitemapChangeFrequency.Unspecified;
        }

        /// <summary>
        /// Attempts to get the <see cref="SitemapChangeFrequency"/> representing the sitemap change frequency of the specified <paramref name="content"/> node.
        /// </summary>
        /// <param name="content">The content node.</param>
        /// <param name="result">When this method returns, holds an instance of <see cref="SitemapChangeFrequency"/> if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the method was successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetSitemapChangeFrequency(this IPublishedContent content, out SitemapChangeFrequency? result) {
            result = content?.Value<SitemapChangeFrequency>(SitemapConstants.Properties.ChangeFrequency);
            return result != null && result != SitemapChangeFrequency.Unspecified;
        }

        /// <summary>
        /// Returns the sitemap priority of the specified <paramref name="content"/> node.
        /// </summary>
        /// <param name="content">The content node.</param>
        /// <returns>The sitemap priority of <paramref name="content"/>.</returns>
        public static float GetSitemapPriority(this IPublishedContent content) {
            return content?.Value<float>(SitemapConstants.Properties.Priority) ?? 0.5f;
        }

        /// <summary>
        /// Attempts to get the sitemap priority of the specified <paramref name="content"/> node.
        /// </summary>
        /// <param name="content">The content node.</param>
        /// <param name="result">When this method returns, holds an instance of <see cref="float"/> if successful; otherwise, <c>0</c>.</param>
        /// <returns><see langword="true"/> if the method was successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetSitemapPriority(this IPublishedContent content, out float result) {

            result = 0;

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

        /// <summary>
        /// Attempts to get the sitemap priority of the specified <paramref name="content"/> node.
        /// </summary>
        /// <param name="content">The content node.</param>
        /// <param name="result">When this method returns, holds an instance of <see cref="float"/> if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the method was successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetSitemapPriority(this IPublishedContent content, out float? result) {

            result = null;

            switch (content?.Value(SitemapConstants.Properties.Priority)) {

                case null:
                    return false;

                case float f:
                    result = f;
                    return true;

                case string str:
                    if (!float.TryParse(str, out float priority)) return false;
                    result = priority;
                    return true;

                default:
                    return false;

            }

        }

    }

}