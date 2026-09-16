using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.PropertyEditors;

[DataEditor(EditorAlias, ValueType = EditorValueType)]
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

    /// <summary>
    /// Gets the icon of the editor.
    /// </summary>
    public const string EditorIcon = "icon-timer";

    /// <summary>
    /// Gets the value type of the editor.
    /// </summary>
    public const string EditorValueType = ValueTypes.Decimal;

    #endregion

    #region Constructors

    public SitemapPriorityPropertyEditor(IDataValueEditorFactory dataValueEditorFactory) : base(dataValueEditorFactory) { }

    #endregion

}