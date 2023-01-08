using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {

    [DataEditor(EditorAlias, EditorType.PropertyValue, "Limbo Sitemap Change Frequency", EditorView,
        ValueType = ValueTypes.String,
        Group = "Limbo",
        Icon = "icon-timer color-limbo")]
    public class SitemapFrequencyEditor : DataEditor {

        #region Constants

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        public const string EditorAlias = "Limbo.Umbraco.Seo.SitemapChangeFrequency";

        public const string EditorView = "/App_Plugins/Limbo.Umbraco.Seo/Views/SitemapChangeFrequency.html";

        #endregion

        #region Constructors

        public SitemapFrequencyEditor(IDataValueEditorFactory dataValueEditorFactory) : base(dataValueEditorFactory) { }

        #endregion

    }

}