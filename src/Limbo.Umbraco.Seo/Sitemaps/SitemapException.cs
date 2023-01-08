using System;
using System.Net;

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Exception class thrown if building a sitemap fails
    /// </summary>
    public class SitemapException : Exception {

        /// <summary>
        /// Gets the HTTP status code associated with the exception.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public SitemapException(string message) : base(message) {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="statusCode"/> and <paramref name="message"/>.
        /// </summary>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="message">The exception message.</param>
        public SitemapException(HttpStatusCode statusCode, string message) : base(message) {
            StatusCode = statusCode;
        }

    }

}