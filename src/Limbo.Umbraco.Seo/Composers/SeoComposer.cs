using Limbo.Umbraco.Seo.Manifests;
using Limbo.Umbraco.Seo.Robots;
using Limbo.Umbraco.Seo.Sitemaps;
using Limbo.Umbraco.Seo.Sites;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using Umbraco.Extensions;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Composers;

public class SeoComposer : IComposer {

    public void Compose(IUmbracoBuilder builder) {

        builder.Services.AddUnique<ISiteAccessor, SiteAccessor>();
        builder.Services.AddUnique<IRobotsService, RobotsService>();
        builder.Services.AddUnique<ISitemapService, SitemapService>();

        builder.ManifestFilters().Append<SeoManifestFilter>();

        builder.Services.Configure<UmbracoPipelineOptions>(options => {
            options.AddFilter(new UmbracoPipelineFilter(
                "LimboSeo",
                applicationBuilder => {
                    applicationBuilder.UseMiddleware<RobotsMiddleware>();
                },
                _ => { },
                _ => { }
            ));
        });

    }

}