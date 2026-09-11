using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors.Sitemaps;

/// <summary>
/// Class representing the configuration of <see cref="SitemapFrequencyPropertyEditor"/>.
/// </summary>
public class SitemapFrequencyConfiguration {

    /// <summary>
    /// Gets or sets whether the returned property value should be <see langword="null"/> if no value has been saved.
    /// </summary>
    [ConfigurationField("nullable")]
    public bool Nullable { get; set; }

}