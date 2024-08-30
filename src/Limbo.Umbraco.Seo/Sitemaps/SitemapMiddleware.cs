using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Limbo.Umbraco.Seo.Sitemaps;

#pragma warning disable CS1591

public class SitemapMiddleware {

    private readonly RequestDelegate _next;
    private readonly ILogger<SitemapMiddleware> _logger;
    private readonly IUmbracoContextFactory _umbracoContextFactory;
    private readonly ISitemapService _sitemapService;

    public SitemapMiddleware(RequestDelegate next, ILogger<SitemapMiddleware> logger, IUmbracoContextFactory umbracoContextFactory, ISitemapService sitemapService) {
        _next = next;
        _logger = logger;
        _umbracoContextFactory = umbracoContextFactory;
        _sitemapService = sitemapService;
    }

    public async Task InvokeAsync(HttpContext context) {

        string path = context.Request.Path;

        // Ignore all /umbraco/ requests
        if (!"/sitemap.xml".InvariantEquals(path)) {
            await _next(context);
            return;
        }

        // Make sure we have an Umbraco context
        using UmbracoContextReference reference = _umbracoContextFactory.EnsureUmbracoContext();

        // Generate a new sitemap
        ISitemapResult sitemap = _sitemapService.BuildSitemap(context);

        // Write to the log if building the sitemap failed
        if (sitemap.Exception is not null) _logger.LogError(sitemap.Exception, "Failed building sitemap for {Domain}.", context.Request.Host);

        // Generate the XML for the sitemap
        StringBuilder builder = new();
        await using (TextWriter writer = new StringWriter(builder)) {
            _sitemapService.ToXmlDocument(sitemap).Save(writer);
        }

        // Return a content result with the XML
        context.Response.StatusCode = (int) HttpStatusCode.OK;
        context.Response.ContentType = "application/xml";
        await context.Response.WriteAsync(builder.ToString());

    }

}