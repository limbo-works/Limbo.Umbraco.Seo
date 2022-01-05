using Microsoft.AspNetCore.Http;

namespace Limbo.Umbraco.Seo.Sitemaps {

    public interface ISitemapHelper {

        ISitemapResult BuildSitemap(HttpContext context);

    }

}