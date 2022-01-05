using System.Net.Http;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace Limbo.Umbraco.Seo.Sitemaps {

    public class SitemapController : UmbracoApiController {

        private readonly ISitemapHelper _sitemapHelper;

        public SitemapController(ISitemapHelper sitemapHelper) {
            _sitemapHelper = sitemapHelper;
        }

        [HttpGet]
        public HttpResponseMessage XmlSitemap() {
            return _sitemapHelper.BuildSitemap().AsResponseMessage();
        }

    }

}