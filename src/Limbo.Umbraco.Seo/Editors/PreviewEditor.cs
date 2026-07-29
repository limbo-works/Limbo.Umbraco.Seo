using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors;

// [CHANGE: Umbraco 17 upgrade - property editor schema/UI split] Related: Editors/Sitemaps/SitemapFrequencyEditor.cs, Editors/Sitemaps/SitemapPriorityEditor.cs, Composers/SeoComposer.cs, Client/src/manifests.ts
// The name, icon, group and view of the editor are no longer declared here. From Umbraco 14 and
// onwards they belong to the "propertyEditorUi" manifest in "Client/src/manifests.ts".
[DataEditor(EditorAlias, ValueType = ValueTypes.String)]
public class PreviewEditor : DataEditor {

    private readonly IIOHelper _ioHelper;

    #region Constants

    /// <summary>
    /// Gets the name of the editor.
    /// </summary>
    public const string EditorName = "Limbo SEO Preview";

    /// <summary>
    /// Gets the alias of the editor.
    /// </summary>
    public const string EditorAlias = "Limbo.Umbraco.Seo.Preview";

    /// <summary>
    /// Gets the alias of the client side property editor UI of this editor.
    /// </summary>
    /// <remarks>The UI alias must differ from <see cref="EditorAlias"/>: the backoffice extension
    /// registry requires aliases to be unique across all extension types, so a schema and its UI
    /// sharing one alias means the UI is never registered. Data types carried over from Umbraco 13
    /// are pointed at this alias by
    /// <see cref="Migrations.UpdatePropertyEditorUiAliases"/>.</remarks>
    public const string EditorUiAlias = "Limbo.Umbraco.Seo.PropertyEditorUi.Preview";

    #endregion

    #region Constructors

    public PreviewEditor(IDataValueEditorFactory dataValueEditorFactory, IIOHelper ioHelper) : base(dataValueEditorFactory) {
        _ioHelper = ioHelper;
    }

    #endregion

    #region member methods

    protected override IConfigurationEditor CreateConfigurationEditor() {
        return new PreviewConfigurationEditor(_ioHelper);
    }

    #endregion

}
