using Umbraco.Core.Logging;
using Umbraco.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {

    [DataEditor(EditorAlias, EditorType.PropertyValue, "Limbo Sitemap Priority", EditorView, ValueType = ValueTypes.Decimal, Group = "Limbo", Icon = "icon-checkbox color-limbo")]
    public class SitemapPriorityEditor : DataEditor {

        #region Constants

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        public const string EditorAlias = "Limbo.Seo.PagePriority";

        public const string EditorView = "/App_Plugins/Limbo.Seo/Views/Editors/PagePriority.html";

        #endregion

        #region Constructors

        public SitemapPriorityEditor(ILogger logger) : base(logger) { }

        #endregion

    }


}