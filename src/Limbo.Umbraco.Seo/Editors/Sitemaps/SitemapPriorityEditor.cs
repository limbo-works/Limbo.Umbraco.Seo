using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors.Sitemaps;

// [CHANGE: Umbraco 17 upgrade - property editor schema/UI split] Related: Editors/PreviewEditor.cs, Editors/Sitemaps/SitemapFrequencyEditor.cs, Client/src/manifests.ts
[DataEditor(EditorAlias, ValueType = ValueTypes.Decimal)]
public class SitemapPriorityEditor : DataEditor {

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
    public const string EditorUiAlias = EditorAlias;

    #endregion

    #region Constructors

    public SitemapPriorityEditor(IDataValueEditorFactory dataValueEditorFactory) : base(dataValueEditorFactory) { }

    #endregion

}
