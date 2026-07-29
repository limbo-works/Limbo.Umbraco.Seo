using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors;

/// <summary>
/// Class representing the configuration of <see cref="PreviewEditor"/>.
/// </summary>
/// <remarks>From Umbraco 14 and onwards, the label, description and editor of each configuration
/// field are declared in the <c>propertyEditorSchema</c> manifest rather than in C#. The aliases
/// below must stay in sync with <c>settings.properties</c> in <c>Client/src/manifests.ts</c>.</remarks>
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
    /// Gets or sets whether asteriscs in the title properties should be removed.
    /// </summary>
    [ConfigurationField("removeAsteriscs")]
    public bool RemoveAsteriscs { get; set; }

}
