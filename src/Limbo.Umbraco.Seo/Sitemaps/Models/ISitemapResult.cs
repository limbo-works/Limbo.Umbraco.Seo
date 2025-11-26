using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Limbo.Umbraco.Seo.Sitemaps.Models;

/// <summary>
/// Interface describing a sitemap result.
/// </summary>
public interface ISitemapResult {

    /// <summary>
    /// Gets the status of the sitemap build operation.
    /// </summary>
    public SitemapBuildStatus Status { get; }

    /// <summary>
    /// Gets an instance of <see cref="Exception"/> if building the sitemap was unsuccessful.
    /// </summary>
    Exception? Exception { get; }

    /// <summary>
    /// Gets a list of the sitemap items.
    /// </summary>
    IReadOnlyList<ISitemapItem>? Items { get; }

    /// <summary>
    /// Gets whether the building the sitemap was successful.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Items))]
    bool IsSuccessful { get; }

    /// <summary>
    /// Gets an error message that will be returned to the "user".
    /// </summary>
    public string? Error { get; }

}