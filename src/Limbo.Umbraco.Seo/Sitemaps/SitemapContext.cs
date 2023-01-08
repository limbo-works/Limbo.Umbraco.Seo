using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Class representing a sitemap context.
    /// </summary>
    public class SitemapContext : ISitemapContext {

        #region Properties

        /// <summary>
        /// Gets a reference to the current <see cref="HttpContext"/>.
        /// </summary>
        public HttpContext HttpContext { get; }

        /// <summary>
        /// Gets the URI of the current request.
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        /// Gets a list of all domains configured in Umbraco.
        /// </summary>
        public IReadOnlyList<IDomain> Domains { get; }

        /// <summary>
        /// Gets or sets the domain associated with the current request.
        /// </summary>
        public IDomain? Domain { get; set; }

        /// <summary>
        /// Gets or sets a reference to the <see cref="IPublishedContent"/> representing the root node.
        /// </summary>
        public IPublishedContent? RootNode { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new sitemap context.
        /// </summary>
        /// <param name="context">A reference to the current <see cref="HttpContext"/>.</param>
        /// <param name="uri">An instance of <see cref="Uri"/> representing the requested URL.</param>
        /// <param name="domains">A list of all domains added to Umbraco.</param>
        /// <param name="domain">A reference to th current <see cref="IDomain"/>, if any.</param>
        public SitemapContext(HttpContext context, Uri uri, IReadOnlyList<IDomain> domains, IDomain? domain) {
            HttpContext = context;
            Uri = uri;
            Domains = domains;
            Domain = domain;
        }

        #endregion

    }

}