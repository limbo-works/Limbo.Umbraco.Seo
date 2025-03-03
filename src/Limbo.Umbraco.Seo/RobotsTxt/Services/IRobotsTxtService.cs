using Limbo.Umbraco.Seo.RobotsTxt.Models;
using Limbo.Umbraco.Seo.Sites;
using Microsoft.AspNetCore.Http;

namespace Limbo.Umbraco.Seo.RobotsTxt.Services;

/// <summary>
/// Interface descring a service for generating <c>robots.txt</c> files.
/// </summary>
public interface IRobotsTxtService {

    /// <summary>
    /// Returns the <c>robots.txt</c> for the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>An instance of <see cref="IRobotsTxtResult"/>.</returns>
    IRobotsTxtResult GetRobots(HttpContext context);

    /// <summary>
    /// Returns the <c>robots.txt</c> for the specified <paramref name="site"/>.
    /// </summary>
    /// <param name="site">The site.</param>
    /// <returns>An instance of <see cref="IRobotsTxtResult"/>.</returns>
    IRobotsTxtResult GetRobots(ISite site);

}