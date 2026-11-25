#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Limbo.Umbraco.Seo.Constants;

/// <summary>
/// Static class with constants for various URLs used in the package.
/// </summary>
public static class SeoUrls {

    public const string RobotsTxt = "/robots.txt";

    /// <summary>
    /// Gets the canonical URL of the <c>security.txt</c> file, as defined by
    /// <see href="https://www.rfc-editor.org/rfc/rfc9116#section-3">RFC 9116</see>.
    /// </summary>
    public const string SecurityTxt = "/.well-known/security.txt";

    /// <summary>
    /// Gets the legacy top-level URL of the <c>security.txt</c> file. Requests to this URL are redirected to
    /// <see cref="SecurityTxt"/>.
    /// </summary>
    public const string SecurityTxtLegacy = "/security.txt";

    public const string Sitemap = "/sitemap.xml";

}