#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Limbo.Umbraco.Seo.Sitemaps.Models;

/// <summary>
/// Enum class indicating the status of a sitemap build operation.
/// </summary>
public enum SitemapBuildStatus {
    Success,
    Unauthorized,
    NotFound,
    Error
}