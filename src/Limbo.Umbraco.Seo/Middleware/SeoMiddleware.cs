using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Limbo.Umbraco.Seo.Constants;
using Limbo.Umbraco.Seo.RobotsTxt.Models;
using Limbo.Umbraco.Seo.RobotsTxt.Services;
using Limbo.Umbraco.Seo.SecurityTxt.Models;
using Limbo.Umbraco.Seo.SecurityTxt.Services;
using Limbo.Umbraco.Seo.Sitemaps.Models;
using Limbo.Umbraco.Seo.Sitemaps.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skybrud.Essentials.Text;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Web;

namespace Limbo.Umbraco.Seo.Middleware;

#pragma warning disable CS1591

/// <summary>
/// Middleware used for exposing <c>/security.txt</c> files.
/// </summary>
public class SeoMiddleware {

    private readonly RequestDelegate _next;
    private readonly ILogger<SeoMiddleware> _logger;
    private readonly IUmbracoContextFactory _umbracoContextFactory;

    public SeoMiddleware(RequestDelegate next, ILogger<SeoMiddleware> logger, IUmbracoContextFactory umbracoContextFactory) {
        _next = next;
        _logger = logger;
        _umbracoContextFactory = umbracoContextFactory;
    }

    public async Task InvokeAsync(HttpContext context) {

        string path = context.Request.Path.ToString().ToLowerInvariant();

        switch (path) {

            case SeoUrls.RobotsTxt:
                await HandleRobotsTxt(context);
                return;

            case SeoUrls.SecurityTxt:
                await HandleSecurityTxt(context);
                return;

            case SeoUrls.Sitemap:
                await HandleSitemapXml(context);
                return;

            default:
                await _next(context);
                return;

        }

    }

    protected virtual async Task HandleRobotsTxt(HttpContext context) {

        // Make sure we have an Umbraco context
        using UmbracoContextReference reference = _umbracoContextFactory.EnsureUmbracoContext();

        // Generate a new robots result
        IRobotsTxtResult result = context.RequestServices.GetRequiredService<IRobotsTxtService>().GetRobotsTxt(context);

        // Write to the log if building the security value failed
        if (result.Exception is not null) {
            _logger.LogError(result.Exception, "Failed generating robots.txt for {Domain}.", context.Request.Host);
        }

        // Write the result to the response
        await WritePlain(context, result.StatusCode, result.Value);

    }

    protected virtual async Task HandleSecurityTxt(HttpContext context) {

        // Make sure we have an Umbraco context
        using UmbracoContextReference reference = _umbracoContextFactory.EnsureUmbracoContext();

        // Generate a new security result
        ISecurityTxtResult result = context.RequestServices.GetRequiredService<ISecurityTxtService>().GetSecurityTxt(context);

        // Write to the log if building the security value failed
        if (result.Exception is not null) {
            _logger.LogError(result.Exception, "Failed generating security.txt for {Domain}.", context.Request.Host);
        }

        // Write the result to the response
        await WritePlain(context, result.StatusCode, result.Value);

    }

    protected virtual async Task HandleSitemapXml(HttpContext context) {

        // Get a reference to the current sitemap service
        ISitemapService sitemapService = context.RequestServices.GetRequiredService<ISitemapService>();

        // Make sure we have an Umbraco context
        using UmbracoContextReference reference = _umbracoContextFactory.EnsureUmbracoContext();

        // Generate a new sitemap
        ISitemapResult sitemap = sitemapService.BuildSitemap(context);

        // Write to the log if building the sitemap failed
        if (sitemap.Exception is not null) _logger.LogError(sitemap.Exception, "Failed building sitemap for {Domain}.", context.Request.Host);

        // Generate the XML for the sitemap
        StringBuilder builder = new();
        await using (TextWriter writer = new StringWriterWithEncoding(builder, Encoding.UTF8)) {
            sitemapService.ToXmlDocument(sitemap).Save(writer);
        }

        // Return a content result with the XML
        context.Response.StatusCode = (int) HttpStatusCode.OK;
        context.Response.ContentType = "application/xml";
        await context.Response.WriteAsync(builder.ToString());

    }

    protected virtual async Task WritePlain(HttpContext context, HttpStatusCode statusCode, string? value) {

        context.Response.StatusCode = (int) statusCode;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync(value ?? string.Empty);

    }

}