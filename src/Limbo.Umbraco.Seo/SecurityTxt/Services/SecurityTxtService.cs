using System.Net;
using Limbo.Umbraco.Seo.Constants;
using Limbo.Umbraco.Seo.SecurityTxt.Models;
using Limbo.Umbraco.Seo.Sites;
using Microsoft.AspNetCore.Http;
using Umbraco.Extensions;

namespace Limbo.Umbraco.Seo.SecurityTxt.Services;

/// <summary>
/// Service for generating <c>security.txt</c> files.
/// </summary>
public class SecurityTxtService : ISecurityTxtService {

    private readonly ISiteAccessor _siteAccessor;

    /// <summary>
    /// Initializes a new instance based on the specified dependencies.
    /// </summary>
    /// <param name="siteAccessor">An instance of <see cref="ISiteAccessor"/>.</param>
    public SecurityTxtService(ISiteAccessor siteAccessor) {
        _siteAccessor = siteAccessor;
    }

    /// <summary>
    /// Returns the <c>robots.txt</c> for the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>An instance of <see cref="ISecurityTxtResult"/>.</returns>
    public virtual ISecurityTxtResult GetSecurity(HttpContext context) {
        return !_siteAccessor.TryGetSite(context, out ISite? site) ? new SecurityTxtResult(HttpStatusCode.NotFound) : GetSecurity(site);
    }

    /// <summary>
    /// Returns the <c>robots.txt</c> for the specified <paramref name="site"/>.
    /// </summary>
    /// <param name="site">The site.</param>
    /// <returns>An instance of <see cref="ISecurityTxtResult"/>.</returns>
    public virtual ISecurityTxtResult GetSecurity(ISite site) {

        // Get the robots value
        string robots = site.Content.Value<string>(SeoProperties.RobotsTxt) ?? string.Empty;

        // Return the result
        return new SecurityTxtResult(robots);

    }

}