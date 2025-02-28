using System.Collections.Generic;
using Umbraco.Cms.Core.Manifest;

namespace Limbo.Umbraco.Seo.Manifests;

/// <inheritdoc />
public class SeoManifestFilter : IManifestFilter {

    /// <inheritdoc />
    public void Filter(List<PackageManifest> manifests) {

        // Initialize a new manifest filter for this package
        PackageManifest manifest = new() {
            AllowPackageTelemetry = true,
            PackageId = SeoPackage.Alias,
            PackageName = SeoPackage.Name,
            Version = SeoPackage.InformationalVersion,
            BundleOptions = BundleOptions.Independent,
            Scripts = [
                $"/App_Plugins/{SeoPackage.Alias}/Scripts/Controllers/Preview.js",
                $"/App_Plugins/{SeoPackage.Alias}/Scripts/Controllers/SitemapChangeFrequency.js",
                $"/App_Plugins/{SeoPackage.Alias}/Scripts/Controllers/SitemapPriority.js"
            ],
            Stylesheets = [
                $"/App_Plugins/{SeoPackage.Alias}/Styles/Styles.css"
            ]
        };

        // Append the manifest
        manifests.Add(manifest);

    }

}