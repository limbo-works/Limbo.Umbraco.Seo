using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Limbo.Umbraco.Seo.Sites;

/// <summary>
/// Service for getting the current site.
/// </summary>
public class SiteAccessor : ISiteAccessor {

    private readonly IDomainService _domainService;
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;

    #region Constructors

    /// <summary>
    /// Initializes a new instance based on the specified parameters.
    /// </summary>
    /// <param name="domainService">An instance of <see cref="IDomainService"/>.</param>
    /// <param name="umbracoContextAccessor">An instance of <see cref="IUmbracoContextAccessor"/>.</param>
    public SiteAccessor(IDomainService domainService, IUmbracoContextAccessor umbracoContextAccessor) {
        _domainService = domainService;
        _umbracoContextAccessor = umbracoContextAccessor;
    }

    #endregion

    /// <summary>
    /// Returns the site associated with the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>An instance of <see cref="ISite"/> if successful; otherwise, <see langword="null"/>.</returns>
    public virtual ISite? GetSite(HttpContext context) {
        return TryGetSite(context, out var result) ? result : null;
    }

    /// <summary>
    /// Attempts to get the site associated with the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="ISite"/> if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="null"/>.</returns>
    public virtual bool TryGetSite(HttpContext context, [NotNullWhen(true)] out ISite? result) {

        result = null;

        // Determine the current URL/URL
        Uri url = GetUri(context.Request);

        // Get a list of all domains configured in Umbraco
        IReadOnlyList<IDomain> domains = _domainService.GetAll(false).ToArray();

        // Get the first matching domain (or null if no matches were found)
        IDomain? domain = domains.FirstOrDefault(d => IsMatch(url, d));
        if (domain?.RootContentId is null) return false;

        // Get the root node of the matched domain
        IPublishedContent? content = _umbracoContextAccessor.GetRequiredUmbracoContext().Content?.GetById(domain.RootContentId.Value);
        if (content is null) return false;

        // Return whether a domain was found
        result = new Site(content);
        return true;

    }

    /// <summary>
    /// Returns  whether <paramref name="url"/> and <paramref name="domain"/>.
    /// </summary>
    /// <param name="url">The current URL.</param>
    /// <param name="domain">The domain to check.</param>
    /// <returns><see langword="true"/> if <paramref name="url"/> and <paramref name="domain"/> matches; otherwise, <see langword="false"/>.</returns>
    protected virtual bool IsMatch(Uri url, IDomain domain) {
        if (domain.DomainName.StartsWith($"{url.Scheme}://{url.Authority}")) return true;
        if (domain.DomainName.StartsWith($"{url.Scheme}://{url.Host}")) return true;
        if (domain.DomainName.Contains(url.Authority)) return true;
        if (domain.DomainName.Contains(url.Host)) return true;
        return false;
    }

    private static Uri GetUri(HttpRequest request) {
        // TODO: Can we move this to one of our other packages?
        return new UriBuilder {
            Scheme = request.Scheme,
            Host = request.Host.Host,
            Port = request.Host.Port ?? (request.Scheme == "https" ? 80 : 443),
            Path = request.Path,
            Query = request.QueryString.ToUriComponent()
        }.Uri;
    }

}