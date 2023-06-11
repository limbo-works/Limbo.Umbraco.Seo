using System;
using Limbo.Umbraco.Seo.Models.Sitemaps;

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Class representing a sitemap item.
    /// </summary>
    public class SitemapItem : ISitemapItem {

        /// <summary>
        /// Gets or sets the URL of the item.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a timestamp for when the item was last modified.
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets the change frequency of the item.
        /// </summary>
        public SitemapChangeFrequency? ChangeFrequency { get; set; }

        /// <summary>
        /// Gets or sets the priority of the item.
        /// </summary>
        public float? PagePriority { get; set; }

        /// <summary>
        /// Initializes a new instance based on the specified URL.
        /// </summary>
        /// <param name="url"></param>
        public SitemapItem(string url) {
            Url = url;
        }

    }

}