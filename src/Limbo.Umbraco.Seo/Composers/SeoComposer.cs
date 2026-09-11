using Limbo.Umbraco.Seo.Manifests;
using Limbo.Umbraco.Seo.Middleware;
using Limbo.Umbraco.Seo.RobotsTxt.Services;
using Limbo.Umbraco.Seo.SecurityTxt.Services;
using Limbo.Umbraco.Seo.Sitemaps.Services;
using Limbo.Umbraco.Seo.Sites;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skybrud.Essentials.Umbraco.Composing;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using Umbraco.Extensions;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Composers;

public class SeoComposer : IComposer {

    public void Compose(IUmbracoBuilder builder) {

        builder.AddPackageManifestReader<SeoPackageManifestReader>();

        builder.Services.AddUnique<ISiteAccessor, SiteAccessor>();
        builder.Services.AddUnique<IRobotsTxtService, RobotsTxtService>();
        builder.Services.AddUnique<ISecurityTxtService, SecurityTxtService>();
        builder.Services.AddUnique<ISitemapService, SitemapService>();

        builder.Services.Configure<UmbracoPipelineOptions>(options => {
            options.AddFilter(new UmbracoPipelineFilter("LimboSeo", prePipeline: applicationBuilder => {
                applicationBuilder.UseMiddleware<SeoMiddleware>();
            }));
        });

    }

}