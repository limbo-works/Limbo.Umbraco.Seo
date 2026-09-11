using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors.Sitemaps;

[DataEditor(EditorAlias, ValueType = ValueTypes.Decimal)]
public class SitemapPriorityPropertyEditor : DataEditor {

    #region Constants

    /// <summary>
    /// Gets the name of the editor.
    /// </summary>
    public const string EditorName = "Limbo Sitemap Priority";

    /// <summary>
    /// Gets the alias of the editor.
    /// </summary>
    public const string EditorAlias = "Limbo.Umbraco.Seo.SitemapPriority";

    /// <summary>
    /// Gets the alias of the client side property editor UI of this editor.
    /// </summary>
    /// <remarks>Must differ from <see cref="EditorAlias"/> - see <see cref="PreviewPropertyEditor.EditorUiAlias"/>.</remarks>
    public const string EditorUiAlias = "Limbo.Umbraco.Seo.PropertyEditorUi.SitemapPriority";

    #endregion

    #region Constructors

    public SitemapPriorityPropertyEditor(IDataValueEditorFactory dataValueEditorFactory) : base(dataValueEditorFactory) { }

    #endregion

}