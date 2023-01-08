using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {

    [DataEditor(EditorAlias, EditorType.PropertyValue, EditorName, EditorView,
        ValueType = ValueTypes.String,
        Group = "Limbo",
        Icon = "icon-chart color-limbo")]
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
        /// Gets the URL of the view of the editor.
        /// </summary>
        public const string EditorView = "/App_Plugins/Limbo.Umbraco.Seo/Views/Preview.html";

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

}