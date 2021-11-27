using Limbo.Umbraco.Seo.Sitemaps;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;

namespace Limbo.Umbraco.Seo.Composers {
    
    public class SeoComposer : IComposer {

        public void Compose(IUmbracoBuilder builder) {
            builder.Services.AddUnique<ISitemapHelper, SitemapHelper>();
        }
    }

}