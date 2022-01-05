using Limbo.Umbraco.Seo.Sitemaps;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Limbo.Umbraco.Seo.Composers {

    public class SeoComposer : IUserComposer {

        public void Compose(Composition composition) {
            composition.Register<ISitemapHelper, SitemapHelper>();
        }

    }

}