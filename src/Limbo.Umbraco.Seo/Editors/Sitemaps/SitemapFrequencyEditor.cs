using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors.Sitemaps;

// [CHANGE: Umbraco 17 upgrade - property editor schema/UI split] Related: Editors/PreviewEditor.cs, Editors/Sitemaps/SitemapPriorityEditor.cs, Client/src/manifests.ts
[DataEditor(EditorAlias, ValueType = ValueTypes.String)]
public class SitemapFrequencyEditor : DataEditor {

    #region Constants

    /// <summary>
    /// Gets the name of the editor.
    /// </summary>
    public const string EditorName = "Limbo Sitemap Change Frequency";

    /// <summary>
    /// Gets the alias of the editor.
    /// </summary>
    public const string EditorAlias = "Limbo.Umbraco.Seo.SitemapChangeFrequency";

    /// <summary>
    /// Gets the alias of the client side property editor UI of this editor.
    /// </summary>
    public const string EditorUiAlias = EditorAlias;

    #endregion

    #region Constructors

    // NOTE: As in previous versions of this package, no configuration editor is wired up here.
    // "SitemapFrequencyConfiguration.UseNullable" isn't honoured by SitemapFrequencyValueConverter,
    // so exposing it on the data type would only offer editors a setting that does nothing.
    public SitemapFrequencyEditor(IDataValueEditorFactory dataValueEditorFactory) : base(dataValueEditorFactory) { }

    #endregion

}
