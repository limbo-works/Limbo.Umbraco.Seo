using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Skybrud.Essentials.AspNetCore;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Limbo.Umbraco.Seo.Sites;

/// <summary>
/// Service for getting the current site.
/// </summary>
// [CHANGE: Umbraco 17 upgrade - IDomainService.GetAll is obsolete; resolve domains from the published domain cache instead] Related: Sitemaps/Services/SitemapService.cs, Composers/SeoComposer.cs
public class SiteAccessor : ISiteAccessor {

    private readonly IUmbracoContextAccessor _umbracoContextAccessor;

    #region Constructors

    /// <summary>
    /// Initializes a new instance based on the specified parameters.
    /// </summary>
    /// <param name="umbracoContextAccessor">An instance of <see cref="IUmbracoContextAccessor"/>.</param>
    public SiteAccessor(IUmbracoContextAccessor umbracoContextAccessor) {
        _umbracoContextAccessor = umbracoContextAccessor;
    }

    #endregion

    /// <summary>
    /// Returns the site associated with the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>An instance of <see cref="ISite"/> if successful; otherwise, <see langword="null"/>.</returns>
    public virtual ISite? GetSite(HttpContext context) {
        return TryGetSite(context, out ISite? result) ? result : null;
    }

    /// <summary>
    /// Attempts to get the site associated with the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="ISite"/> if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="null"/>.</returns>
    public virtual bool TryGetSite(HttpContext context, [NotNullWhen(true)] out ISite? result) {

        result = null;

        // Determine the current URL
        Uri url = context.Request.GetUri();

        // [CHANGE: "GetRequiredUmbracoContext" throws when no context is available, turning a "no site
        // for this request" case into an unhandled 500 - the try-pattern keeps the documented
        // false/null return] Related: SeoPackage.cs, Editors/Sitemaps/SitemapPriorityValueConverter.cs
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext)) return false;

        // Get a list of all domains from the published domain cache. Unlike "IDomainService", this
        // doesn't hit the database on each request, and it is the supported API from Umbraco 14.
        IReadOnlyList<Domain> domains = umbracoContext.Domains?.GetAll(false).ToArray() ?? [];

        // Get the first matching domain (or null if no matches were found)
        Domain? domain = domains.FirstOrDefault(d => IsMatch(url, d));
        if (domain is null) return false;

        // Get the root node of the matched domain
        IPublishedContent? content = umbracoContext.Content?.GetById(domain.ContentId);
        if (content is null) return false;

        // Return whether a domain was found
        result = new Site(content);
        return true;

    }

    /// <summary>
    /// Returns whether <paramref name="url"/> and <paramref name="domain"/> matches.
    /// </summary>
    /// <param name="url">The current URL.</param>
    /// <param name="domain">The domain to check.</param>
    /// <returns><see langword="true"/> if <paramref name="url"/> and <paramref name="domain"/> matches; otherwise, <see langword="false"/>.</returns>
    protected virtual bool IsMatch(Uri url, Domain domain) {
        if (domain.Name.StartsWith($"{url.Scheme}://{url.Authority}")) return true;
        if (domain.Name.StartsWith($"{url.Scheme}://{url.Host}")) return true;
        if (domain.Name.Contains(url.Authority)) return true;
        if (domain.Name.Contains(url.Host)) return true;
        return false;
    }

}
