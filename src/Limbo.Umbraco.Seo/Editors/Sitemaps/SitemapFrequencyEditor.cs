using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {

    [DataEditor(EditorAlias, EditorType.PropertyValue, "Limbo Sitemap Frequency", EditorView, ValueType = ValueTypes.String, Group = "Limbo", Icon = "icon-checkbox color-limbo")]
    public class SitemapFrequencyEditor : DataEditor {

        #region Constants

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        public const string EditorAlias = "Limbo.Seo.PageFrequency";

        public const string EditorView = "/App_Plugins/Limbo.Seo/Views/Editors/PageFrequency.html";

        #endregion

        #region Constructors

        public SitemapFrequencyEditor(IDataValueEditorFactory dataValueEditorFactory) : base(dataValueEditorFactory) { }

        #endregion

    }


}