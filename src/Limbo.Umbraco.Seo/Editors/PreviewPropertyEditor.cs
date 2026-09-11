using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors;

[DataEditor(EditorAlias, ValueType = ValueTypes.String)]
public class PreviewPropertyEditor : DataEditor {

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

    public PreviewPropertyEditor(IDataValueEditorFactory dataValueEditorFactory, IIOHelper ioHelper) : base(dataValueEditorFactory) {
        _ioHelper = ioHelper;
    }

    #endregion

    #region member methods

    protected override IConfigurationEditor CreateConfigurationEditor() {
        return new PreviewConfigurationEditor(_ioHelper);
    }

    #endregion

}