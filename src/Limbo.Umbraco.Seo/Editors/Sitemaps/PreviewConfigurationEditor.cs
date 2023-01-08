using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {

    public class PreviewConfigurationEditor : ConfigurationEditor<PreviewConfiguration> {

        public PreviewConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) { }

    }

}