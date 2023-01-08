using System.Collections.Generic;
using Skybrud.Essentials.Strings.Extensions;
using Umbraco.Cms.Core.Manifest;

namespace Limbo.Umbraco.Seo.Manifests {

    /// <inheritdoc />
    public class SeoManifestFilter : IManifestFilter {

        /// <inheritdoc />
        public void Filter(List<PackageManifest> manifests) {
            manifests.Add(new PackageManifest {
                PackageName = SeoPackage.Alias.ToKebabCase(),
                BundleOptions = BundleOptions.Independent,
                Scripts = new[] {
                    $"/App_Plugins/{SeoPackage.Alias}/Scripts/Controllers/Preview.js",
                    $"/App_Plugins/{SeoPackage.Alias}/Scripts/Controllers/SitemapChangeFrequency.js",
                    $"/App_Plugins/{SeoPackage.Alias}/Scripts/Controllers/SitemapPriority.js"
                },
                Stylesheets = new[] {
                    $"/App_Plugins/{SeoPackage.Alias}/Styles/Styles.css"
                }
            });
        }

    }

}