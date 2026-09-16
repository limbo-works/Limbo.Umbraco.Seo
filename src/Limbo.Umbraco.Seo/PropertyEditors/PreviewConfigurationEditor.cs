using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.PropertyEditors;

public class PreviewConfigurationEditor : ConfigurationEditor<PreviewConfiguration> {

    public PreviewConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) { }

}