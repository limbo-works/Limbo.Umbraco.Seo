using System.Web;

namespace Limbo.Umbraco.Seo.Sitemaps {
    
    public interface ISitemapHelper {
        
        ISitemapResult BuildSitemap(HttpContextBase context);

    }

}