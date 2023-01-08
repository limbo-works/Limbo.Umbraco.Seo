using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {

    public class SitemapFrequencyConfigurationEditor : ConfigurationEditor<SitemapFrequencyConfiguration> {

        public SitemapFrequencyConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) { }

    }

}