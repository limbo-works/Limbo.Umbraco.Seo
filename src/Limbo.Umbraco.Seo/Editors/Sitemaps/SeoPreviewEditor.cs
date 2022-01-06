using Umbraco.Core.Logging;
using Umbraco.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {
    
    [DataEditor(EditorAlias, EditorType.PropertyValue, EditorName, EditorView,
        ValueType = ValueTypes.String,
        Group = "Limbo",
        Icon = "icon-chart color-limbo")]
    public class SeoPreviewEditor : DataEditor {

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

        public SeoPreviewEditor(ILogger logger) : base(logger) { }

        #endregion

    }


}