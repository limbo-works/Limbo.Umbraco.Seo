using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Limbo.Umbraco.Seo.Sites;

/// <summary>
/// Interface describing a service for getting the current site.
/// </summary>
public interface ISiteAccessor {

    /// <summary>
    /// Returns the site associated with the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>An instance of <see cref="ISite"/> if successful; otherwise, <see langword="null"/>.</returns>
    ISite? GetSite(HttpContext context);

    /// <summary>
    /// Attempts to get the site associated with the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="ISite"/> if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="null"/>.</returns>
    bool TryGetSite(HttpContext context, [NotNullWhen(true)] out ISite? result);

}