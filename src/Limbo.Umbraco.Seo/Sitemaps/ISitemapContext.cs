using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Interface a sitemap context.
    /// </summary>
    public interface ISitemapContext {

        /// <summary>
        /// Gets a reference to the current <see cref="HttpContext"/>.
        /// </summary>
        HttpContext HttpContext { get; }

        /// <summary>
        /// Gets the URI of the current request.
        /// </summary>
        Uri Uri { get; }

        /// <summary>
        /// Gets a list of all domains configured in Umbraco.
        /// </summary>
        IReadOnlyList<IDomain> Domains { get; }

        /// <summary>
        /// Gets or sets the domain associated with the current request.
        /// </summary>
        IDomain Domain { get; set; }

        /// <summary>
        /// Gets or sets a reference to the <see cref="IPublishedContent"/> representing the root node.
        /// </summary>
        IPublishedContent RootNode { get; set; }

    }

}