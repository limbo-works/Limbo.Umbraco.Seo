using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;

namespace Limbo.Umbraco.Seo.Sitemaps {

    public class SitemapController : UmbracoApiController {

        private readonly ISitemapHelper _sitemapHelper;

        public SitemapController(ISitemapHelper sitemapHelper) {
            _sitemapHelper = sitemapHelper;
        }

        [HttpGet]
        public HttpResponseMessage XmlSitemap() {
            return _sitemapHelper.BuildSitemap(HttpContext).AsResponseMessage();
        }

    }

}