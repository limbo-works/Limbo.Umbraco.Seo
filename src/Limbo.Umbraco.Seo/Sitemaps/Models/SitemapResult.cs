using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Limbo.Umbraco.Seo.Sitemaps.Models;

/// <summary>
/// Class representing a sitemap result.
/// </summary>
public class SitemapResult : ISitemapResult {

    #region Properties

    /// <summary>
    /// Gets the status of the sitemap build operation.
    /// </summary>
    public SitemapBuildStatus Status { get; }

    /// <summary>
    /// Gets an instance of <see cref="Exception"/> if building the sitemap was unsuccessful.
    /// </summary>
    public Exception? Exception { get; }

    /// <summary>
    /// Gets a list of the sitemap items.
    /// </summary>
    public IReadOnlyList<ISitemapItem>? Items { get; }

    /// <summary>
    /// Gets whether the building the sitemap was successful.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Items))]
    public bool IsSuccessful => Items is not null;

    /// <summary>
    /// Gets an error message that will be returned to the "user".
    /// </summary>
    public string? Error { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new unsuccessful result based on the specified <paramref name="exception"/>.
    /// </summary>
    /// <param name="exception">An exception with details about why building the sitemap failed.</param>
    public SitemapResult(Exception exception) {
        Exception = exception;
        Status = SitemapBuildStatus.Error;
    }

    /// <summary>
    /// Initializes a new successful result based on the specified list of sitemap <paramref name="items"/>.
    /// </summary>
    /// <param name="items"></param>
    public SitemapResult(List<ISitemapItem> items) {
        Items = items;
        Status = SitemapBuildStatus.Success;
    }

    /// <summary>
    /// Initializes a new sitemap result with the specified <paramref name="status"/> and <paramref name="error"/> message.
    /// </summary>
    /// <param name="status">The status of the result.</param>
    /// <param name="error">An error message than will be returned in the sitemap.</param>
    public SitemapResult(SitemapBuildStatus status, string? error) {
        Status = status;
        Error = error;
    }

    #endregion

    #region Static methods

    /// <summary>
    /// Returns successful result with the specified list of sitemap <paramref name="items"/>.
    /// </summary>
    /// <param name="items">The items making up the sitemap.</param>
    /// <returns>An instance of <see cref="SitemapResult"/>.</returns>
    public static SitemapResult Success(List<ISitemapItem> items) {
        return new SitemapResult(items);
    }

    /// <summary>
    /// Returns an unauthorized result with an optional <paramref name="error"/> message.
    /// </summary>
    /// <param name="error">The error message to be returned, if any.</param>
    /// <returns>An instance of <see cref="SitemapResult"/>.</returns>
    public static SitemapResult Unauthorized(string? error = null) {
        return new SitemapResult(SitemapBuildStatus.Unauthorized, error ?? "Unauthorized");
    }

    /// <summary>
    /// Returns a not found result with an optional <paramref name="error"/> message.
    /// </summary>
    /// <param name="error">The error message to be returned, if any.</param>
    /// <returns>An instance of <see cref="SitemapResult"/>.</returns>
    public static SitemapResult NotFound(string? error = null) {
        return new SitemapResult(SitemapBuildStatus.Unauthorized, error ?? "Not Found");
    }

    #endregion

}