using Limbo.Umbraco.Seo.Notifications;
using Limbo.Umbraco.Seo.Options;
using Limbo.Umbraco.Seo.Sitemaps;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;

namespace Limbo.Umbraco.Seo.Composers {

    public class SeoComposer : IComposer {

        public void Compose(IUmbracoBuilder builder) {
            builder.Services.AddUnique<ISitemapHelper, SitemapHelper>();
            builder.Services.Configure<SEOPropertyAliasOptions>(builder.Config.GetSection(SEOPropertyAliasOptions.SectionName));
            builder.AddNotificationHandler<ServerVariablesParsingNotification, ServerVariablesNotificationHandler>();
        }
    }

}