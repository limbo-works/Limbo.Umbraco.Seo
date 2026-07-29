using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors;

// [CHANGE: Umbraco 17 upgrade - IEditorConfigurationParser was removed in v14] Related: Editors/Sitemaps/SitemapFrequencyConfigurationEditor.cs, Editors/PreviewEditor.cs
public class PreviewConfigurationEditor : ConfigurationEditor<PreviewConfiguration> {

    public PreviewConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) { }

}
