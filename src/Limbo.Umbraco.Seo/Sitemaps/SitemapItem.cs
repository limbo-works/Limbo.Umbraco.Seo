using System;
using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {

    public class SitemapItem : ISitemapItem {

        public string Url { get; set; }

        public DateTime LastModified { get; set; }

        public string ChangeFrequency { get; set; }

        public string PagePriority { get; set; }

        public XElement ToXml() {

            XElement xml = new XElement(
                SitemapConstants.XNamespace + "url",
                new XElement(SitemapConstants.XNamespace + "loc", Url),
                new XElement(SitemapConstants.XNamespace + "lastmod", LastModified.ToString("yyyy-MM-dd"))
            );
            
            if (!string.IsNullOrWhiteSpace(ChangeFrequency)) xml.Add(new XElement(SitemapConstants.Properties.ChangeFrequency, ChangeFrequency));
            if (!string.IsNullOrWhiteSpace(PagePriority)) xml.Add(new XElement(SitemapConstants.Properties.Priority, PagePriority));

            return xml;

        }

    }

}