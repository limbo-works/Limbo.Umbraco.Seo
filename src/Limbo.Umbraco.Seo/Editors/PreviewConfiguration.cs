using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors;

/// <summary>
/// Class representing the configuration of <see cref="PreviewPropertyEditor"/>.
/// </summary>
public class PreviewConfiguration {

    /// <summary>
    /// Gets or sets an array of the title properties on the page.
    /// </summary>
    [ConfigurationField("title")]
    public string? TitleProperties { get; set; }

    /// <summary>
    /// Gets or sets an array of the description properties on the page.
    /// </summary>
    [ConfigurationField("description")]
    public string? DescriptionProperties { get; set; }

    /// <summary>
    /// Gets or sets whether asterisks in the title properties should be removed.
    /// </summary>
    [ConfigurationField("removeAsterisks")]
    public bool RemoveAsterisks { get; set; }

}