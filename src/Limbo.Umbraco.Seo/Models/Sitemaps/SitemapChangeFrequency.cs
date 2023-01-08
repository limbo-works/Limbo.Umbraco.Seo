#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Models.Sitemaps {

    /// <summary>
    /// Enum class representing the change frequency of a page.
    /// </summary>
    public enum SitemapChangeFrequency {

        Unspecified,

        Always,

        Hourly,

        Daily,

        Weekly,

        Monthly,

        Yearly,

        Never

    }

}