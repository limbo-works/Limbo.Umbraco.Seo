using System.Threading.Tasks;
using Limbo.Umbraco.Seo.Constants;
using Limbo.Umbraco.Seo.Security.Models;
using Limbo.Umbraco.Seo.Security.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            case SeoUrls.Security:
                await HandleSecurityTxt(context);
                return;

            default:
                await _next(context);
                return;

        }

    }

    protected virtual async Task HandleSecurityTxt(HttpContext context) {

        // Make sure we have an Umbraco context
        using UmbracoContextReference reference = _umbracoContextFactory.EnsureUmbracoContext();

        // Generate a new security result
        ISecurityResult result = context.RequestServices.GetRequiredService<ISecurityService>().GetSecurity(context);

        // Write to the log if building the security value failed
        if (result.Exception is not null) {
            _logger.LogError(result.Exception, "Failed building security.txt for {Domain}.", context.Request.Host);
        }

        // Return a content result with the XML
        context.Response.StatusCode = (int) result.StatusCode;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync(result.Value ?? string.Empty);

    }

}