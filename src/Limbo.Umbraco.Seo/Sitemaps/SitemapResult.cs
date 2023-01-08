using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Class representing a sitemap result.
    /// </summary>
    public class SitemapResult : ISitemapResult {

        #region Properties

        /// <summary>
        /// Gets an instance of <see cref="Exception"/> if building the sitemap was unsuccessful.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Gets a list of the sitemap items.
        /// </summary>
        public List<ISitemapItem> Items { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new unsuccessful result based on the specified <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">An exception with details about why building the sitemap failed.</param>
        public SitemapResult(Exception exception) {
            Exception = exception;
        }

        /// <summary>
        /// Initializes a new successful result based on the specified list of sitemap <paramref name="items"/>.
        /// </summary>
        /// <param name="items"></param>
        public SitemapResult(List<ISitemapItem> items) {
            Items = items;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns an instance of <see cref="XElement"/> representing the sitemap result.
        /// </summary>
        /// <returns>An instance of <see cref="XElement"/>.</returns>
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

        #endregion

    }

}