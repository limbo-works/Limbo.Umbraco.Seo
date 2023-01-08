using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Interface describing a sitemap result.
    /// </summary>
    public interface ISitemapResult {

        /// <summary>
        /// Gets an instance of <see cref="Exception"/> if building the sitemap was unsuccessful.
        /// </summary>
        Exception? Exception { get; }

        /// <summary>
        /// Gets a list of the sitemap items.
        /// </summary>
        List<ISitemapItem>? Items { get; }

        /// <summary>
        /// Gets whether the building the sitemap was successful.
        /// </summary>
        [MemberNotNullWhen(true, "Items")]
        bool IsSuccesful { get; }

        /// <summary>
        /// Returns an instance of <see cref="XElement"/> representing the sitemap result.
        /// </summary>
        /// <returns>An instance of <see cref="XElement"/>.</returns>
        XDocument ToXml();

    }

}