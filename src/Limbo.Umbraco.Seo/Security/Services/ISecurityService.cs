using Limbo.Umbraco.Seo.Robots;
using Limbo.Umbraco.Seo.Security.Models;
using Limbo.Umbraco.Seo.Sites;
using Microsoft.AspNetCore.Http;

namespace Limbo.Umbraco.Seo.Security.Services;

/// <summary>
/// Interface descring a service for generating <c>security.txt</c> files.
/// </summary>
public interface ISecurityService {

    /// <summary>
    /// Returns the <c>security.txt</c> for the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>An instance of <see cref="ISecurityResult"/>.</returns>
    ISecurityResult GetSecurity(HttpContext context);

    /// <summary>
    /// Returns the <c>security.txt</c> for the specified <paramref name="site"/>.
    /// </summary>
    /// <param name="site">The site.</param>
    /// <returns>An instance of <see cref="ISecurityResult"/>.</returns>
    ISecurityResult GetSecurity(ISite site);

}