using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors {

    /// <summary>
    /// Class representing the configuration of <see cref="PreviewEditor"/>.
    /// </summary>
    public class PreviewConfiguration {

        /// <summary>
        /// Gets or sets an array of the title properties on the page.
        /// </summary>
        [ConfigurationField("title", "Title properties", view: "textstring", Description = "Specify a comma separated list of properties that should be used for determining the page's SEO title.")]
        public string? TitleProperties { get; set; }

        /// <summary>
        /// Gets or sets an array of the description properties on the page.
        /// </summary>
        [ConfigurationField("description", "Description properties", view: "textstring", Description = "Specify a comma separated list of properties that should be used for determining the page's SEO description.")]
        public string? DescriptionProperties { get; set; }

        /// <summary>
        /// Gets or sets whether asteriscs in the title properties should be removed.
        /// </summary>
        [ConfigurationField("removeAsteriscs", "Remove asteriscs?", "boolean", Description = "Feature used by Limbo. Should probably be documented.")]
        public bool RemoveAsteriscs { get; set; }

    }

}