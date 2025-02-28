using Limbo.Umbraco.Seo.Robots.Models;
using Limbo.Umbraco.Seo.Sites;
using Microsoft.AspNetCore.Http;

namespace Limbo.Umbraco.Seo.Robots.Services;

/// <summary>
/// Interface descring a service for generating <c>robots.txt</c> files.
/// </summary>
public interface IRobotsService {

    /// <summary>
    /// Returns the <c>robots.txt</c> for the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>An instance of <see cref="IRobotsResult"/>.</returns>
    IRobotsResult GetRobots(HttpContext context);

    /// <summary>
    /// Returns the <c>robots.txt</c> for the specified <paramref name="site"/>.
    /// </summary>
    /// <param name="site">The site.</param>
    /// <returns>An instance of <see cref="IRobotsResult"/>.</returns>
    IRobotsResult GetRobots(ISite site);

}