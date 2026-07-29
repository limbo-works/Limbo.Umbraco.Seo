using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors.Sitemaps;

// [CHANGE: Umbraco 17 upgrade - IEditorConfigurationParser was removed in v14] Related: Editors/PreviewConfigurationEditor.cs, Editors/Sitemaps/SitemapFrequencyEditor.cs
public class SitemapFrequencyConfigurationEditor : ConfigurationEditor<SitemapFrequencyConfiguration> {

    public SitemapFrequencyConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) { }

}
