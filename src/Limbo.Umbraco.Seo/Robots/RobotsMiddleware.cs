using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Limbo.Umbraco.Seo.Robots;

#pragma warning disable CS1591

public class RobotsMiddleware {

    private readonly RequestDelegate _next;
    private readonly ILogger<RobotsMiddleware> _logger;
    private readonly IUmbracoContextFactory _umbracoContextFactory;
    private readonly IRobotsService _robotsService;

    public RobotsMiddleware(RequestDelegate next, ILogger<RobotsMiddleware> logger, IUmbracoContextFactory umbracoContextFactory, IRobotsService robotsService) {
        _next = next;
        _logger = logger;
        _umbracoContextFactory = umbracoContextFactory;
        _robotsService = robotsService;
    }

    public async Task InvokeAsync(HttpContext context) {

        string path = context.Request.Path;

        // Ignore all /umbraco/ requests
        if (!"/robots.txt".InvariantEquals(path)) {
            await _next(context);
            return;
        }

        // Make sure we have an Umbraco context
        using UmbracoContextReference reference = _umbracoContextFactory.EnsureUmbracoContext();

        // Generate a new robots result
        IRobotsResult result = _robotsService.GetRobots(context);

        // Write to the log if building the robots value failed
        if (result.Exception is not null) {
            _logger.LogError(result.Exception, "Failed building robots.txt for {Domain}.", context.Request.Host);
        }

        // Return a content result with the XML
        context.Response.StatusCode = (int) result.StatusCode;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync(result.Value ?? string.Empty);

    }

}