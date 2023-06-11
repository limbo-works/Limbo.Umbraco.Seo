using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using Umbraco.Cms.Web.Common.Controllers;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Sitemaps {

    public class SitemapController : UmbracoApiController {

        private readonly ILogger<SitemapController> _logger;
        private readonly ISitemapHelper _sitemapHelper;

        public SitemapController(ILogger<SitemapController> logger, ISitemapHelper sitemapHelper) {
            _logger = logger;
            _sitemapHelper = sitemapHelper;
        }

        [HttpGet]
        public ActionResult XmlSitemap() {

            // Generate a new sitemap
            ISitemapResult sitemap = _sitemapHelper.BuildSitemap(HttpContext);

            // Write to the log if building the sitemap failed
            if (sitemap.Exception is not null) _logger.LogError(sitemap.Exception, "Failed building sitemap for {Domain}.", HttpContext.Request.Host);

            // Generate the XML for the sitemap
            StringBuilder builder = new();
            using (TextWriter writer = new StringWriter(builder)) {
                _sitemapHelper.ToXmlDocument(sitemap).Save(writer);
            }

            // Return a content result with the XML
            return new ContentResult {
                ContentType = "application/xml",
                Content = builder.ToString(),
                StatusCode = 200
            };

        }

    }

}