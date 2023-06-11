using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Class representing a sitemap result.
    /// </summary>
    public class SitemapResult : ISitemapResult {

        #region Properties

        /// <summary>
        /// Gets an instance of <see cref="Exception"/> if building the sitemap was unsuccessful.
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        /// Gets a list of the sitemap items.
        /// </summary>
        public List<ISitemapItem>? Items { get; }

        /// <summary>
        /// Gets whether the building the sitemap was successful.
        /// </summary>
        [MemberNotNullWhen(true, "Items")]
        public bool IsSuccesful => Items is not null;

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

    }

}