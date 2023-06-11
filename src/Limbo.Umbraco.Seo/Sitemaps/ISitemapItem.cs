using System;
using Limbo.Umbraco.Seo.Models.Sitemaps;

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Interface describing a sitemap item.
    /// </summary>
    public interface ISitemapItem {

        /// <summary>
        /// Gets the URL of the item.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Gets a timestamp for when the item was last modified.
        /// </summary>
        DateTime LastModified { get; }

        /// <summary>
        /// Gets the change frequency of the item.
        /// </summary>
        SitemapChangeFrequency? ChangeFrequency { get; }

        /// <summary>
        /// Gets the priority of the item.
        /// </summary>
        float? PagePriority { get; }

    }

}