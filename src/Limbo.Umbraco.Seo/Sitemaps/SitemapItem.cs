using System;
using System.Globalization;
using System.Xml.Linq;
using Limbo.Umbraco.Seo.Models.Sitemaps;
using Skybrud.Essentials.Strings.Extensions;

namespace Limbo.Umbraco.Seo.Sitemaps {

    public class SitemapItem : ISitemapItem {

        public string Url { get; set; }

        public DateTime LastModified { get; set; }

        public SitemapChangeFrequency ChangeFrequency { get; set; }

        public float? PagePriority { get; set; }

        public XElement ToXml() {

            XElement xml = new XElement(
                SitemapConstants.XNamespace + "url",
                new XElement(SitemapConstants.XNamespace + "loc", Url),
                new XElement(SitemapConstants.XNamespace + "lastmod", LastModified.ToString("yyyy-MM-dd"))
            );
            
            if (ChangeFrequency > 0) xml.Add(new XElement(SitemapConstants.Properties.ChangeFrequency, ChangeFrequency.ToLower()));
            if (PagePriority != null) xml.Add(new XElement(SitemapConstants.Properties.Priority, PagePriority.Value.ToString("N1", CultureInfo.InvariantCulture)));

            return xml;

        }

    }

}