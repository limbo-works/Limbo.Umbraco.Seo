using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using Umbraco.Cms.Web.Common.Controllers;
#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Sitemaps {

    public class SitemapController : UmbracoApiController {

        private readonly ISitemapHelper _sitemapHelper;

        public SitemapController(ISitemapHelper sitemapHelper) {
            _sitemapHelper = sitemapHelper;
        }

        [HttpGet]
        public ActionResult XmlSitemap() {

            // Generate a new sitemap
            ISitemapResult sitemap = _sitemapHelper.BuildSitemap(HttpContext);

            // Generate the XML for the sitemap
            StringBuilder builder = new();
            using (TextWriter writer = new StringWriter(builder)) {
                sitemap.ToXml().Save(writer);
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