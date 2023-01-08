using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {

    [DataEditor(EditorAlias, EditorType.PropertyValue, "Limbo Sitemap Priority", EditorView,
        ValueType = ValueTypes.Decimal,
        Group = "Limbo",
        Icon = "icon-caps-lock color-limbo")]
    public class SitemapPriorityEditor : DataEditor {

        #region Constants

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        public const string EditorAlias = "Limbo.Umbraco.Seo.SitemapPriority";

        public const string EditorView = "/App_Plugins/Limbo.Umbraco.Seo/Views/SitemapPriority.html";

        #endregion

        #region Constructors

        public SitemapPriorityEditor(IDataValueEditorFactory dataValueEditorFactory) : base(dataValueEditorFactory) { }

        #endregion

    }


}