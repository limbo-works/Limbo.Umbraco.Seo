using Limbo.Umbraco.Seo.Options;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Limbo.Umbraco.Seo.Notifications {
    public class ServerVariablesNotificationHandler : INotificationHandler<ServerVariablesParsingNotification> 
    {
        private readonly IOptions<SEOPropertyAliasOptions> _options;

        public ServerVariablesNotificationHandler(IOptions<SEOPropertyAliasOptions> options) {
            _options = options;
        }

        public void Handle(ServerVariablesParsingNotification notification) {
            notification.ServerVariables.Add("LimboUmbracoSEO", new {
                titlePropertyAlias = _options.Value.TitlePropertyAlias ?? "title",
                seoTitlePropertyAlias = _options.Value.SeoTitlePropertyAlias ?? "seoTitle",
                teaserPropertyAlias = _options.Value.TeaserPropertyAlias ?? "teaser",
                seoMetaDescriptionPropertyAlias = _options.Value.SeoMetaDescriptionPropertyAlias ?? "seoMetaDescription"
            });
        }
    }
}