using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors.Sitemaps;

[DataEditor(EditorAlias, ValueType = ValueTypes.String)]
public class SitemapFrequencyPropertyEditor : DataEditor {

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
    /// <remarks>Must differ from <see cref="EditorAlias"/> - see <see cref="PreviewPropertyEditor.EditorUiAlias"/>.</remarks>
    public const string EditorUiAlias = "Limbo.Umbraco.Seo.PropertyEditorUi.SitemapChangeFrequency";

    #endregion

    #region Constructors

    // NOTE: As in previous versions of this package, no configuration editor is wired up here.
    // "SitemapFrequencyConfiguration.UseNullable" isn't honoured by SitemapFrequencyValueConverter,
    // so exposing it on the data type would only offer editors a setting that does nothing.
    public SitemapFrequencyPropertyEditor(IDataValueEditorFactory dataValueEditorFactory) : base(dataValueEditorFactory) { }

    #endregion

}