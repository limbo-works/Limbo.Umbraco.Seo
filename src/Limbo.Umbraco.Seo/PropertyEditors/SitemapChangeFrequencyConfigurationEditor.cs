using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.PropertyEditors;

public class SitemapChangeFrequencyConfigurationEditor : ConfigurationEditor<SitemapChangeFrequencyConfiguration> {

    public SitemapChangeFrequencyConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) { }

}