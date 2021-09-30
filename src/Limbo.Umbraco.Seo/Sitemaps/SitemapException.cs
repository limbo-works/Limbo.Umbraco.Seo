using System;
using System.Net;

namespace Limbo.Umbraco.Seo.Sitemaps {
    
    public class SitemapException : Exception {
        
        public HttpStatusCode StatusCode { get; }

        public SitemapException(string message) : base(message) { }
        
        public SitemapException(HttpStatusCode statusCode, string message) : base(message) {
            StatusCode = statusCode;
        }

    }

}