using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors.Sitemaps;

/// <summary>
/// Class representing the configuration of <see cref="SitemapFrequencyEditor"/>.
/// </summary>
/// <remarks>The label and editor of the configuration field below are declared in the
/// <c>propertyEditorSchema</c> manifest in <c>Client/src/manifests.ts</c>.</remarks>
public class SitemapFrequencyConfiguration {

    /// <summary>
    /// Gets or sets whether the returned property value should be <see langword="null"/> if no value has been saved.
    /// </summary>
    [ConfigurationField("nullable")]
    public bool UseNullable { get; set; }

}
