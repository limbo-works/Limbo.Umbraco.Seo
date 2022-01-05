using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {

    public class SitemapResult : ISitemapResult {

        public Exception Exception { get; }

        public List<ISitemapItem> Items { get; }

        public SitemapResult(Exception exception) {
            Exception = exception;
        }

        public SitemapResult(List<ISitemapItem> items) {
            Items = items;
        }

        public XDocument ToXml() {

            XElement root;

            if (Exception != null || Items == null) {

                // Initialize a new <e> element as root
                root = new XElement(SitemapConstants.XNamespace + "e", "Error");

            } else {

                // Initialize a new <urlset> element as root
                root = new XElement(SitemapConstants.XNamespace + "urlset");

                // Add an <url> element for each item
                foreach (ISitemapItem item in Items) root.Add(item.ToXml());

            }

            return new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                root
            );

        }

    }

}