using System;
using System.Globalization;
using System.Xml.Linq;
using Limbo.Umbraco.Seo.Models.Sitemaps;
using Skybrud.Essentials.Strings.Extensions;

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
        public SitemapChangeFrequency ChangeFrequency { get; set; }

        /// <summary>
        /// Gets or sets the priority of the item.
        /// </summary>
        public float? PagePriority { get; set; }

        /// <summary>
        /// Returns an instance of <see cref="XElement"/> representing the sitemap item.
        /// </summary>
        /// <returns>An instance of <see cref="XElement"/>.</returns>
        public XElement ToXml() {

            XElement xml = new(
                SitemapConstants.XNamespace + "url",
                new XElement(SitemapConstants.XNamespace + "loc", Url),
                new XElement(SitemapConstants.XNamespace + "lastmod", LastModified.ToString("yyyy-MM-dd"))
            );

            if (ChangeFrequency > 0) xml.Add(new XElement(SitemapConstants.XNamespace + SitemapConstants.Properties.ChangeFrequency, ChangeFrequency.ToLower()));
            if (PagePriority != null) xml.Add(new XElement(SitemapConstants.XNamespace + SitemapConstants.Properties.Priority, PagePriority.Value.ToString("N1", CultureInfo.InvariantCulture)));

            return xml;

        }

    }

}