using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {
    
    public class SitemapFrequencyConfiguration {

        [ConfigurationField("nullable", "Use nullable?", "boolean")]
        public bool UseNullable { get; set; }

    }

}