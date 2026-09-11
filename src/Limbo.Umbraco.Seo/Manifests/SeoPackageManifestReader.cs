using System.Collections.Generic;
using System.Threading.Tasks;
using Limbo.Umbraco.Seo.Constants;
using Skybrud.Essentials.Umbraco.Constants;
using Skybrud.Essentials.Umbraco.Manifests.Extensions;
using Skybrud.Essentials.Umbraco.Manifests.Extensions.Localization;
using Skybrud.Essentials.Umbraco.Manifests.Extensions.PropertyEditors;
using Umbraco.Cms.Core.Manifest;
using Umbraco.Cms.Infrastructure.Manifest;

using static Limbo.Umbraco.Seo.SeoPackage;

// ReSharper disable StringLiteralTypo

namespace Limbo.Umbraco.Seo.Manifests;

public class SeoPackageManifestReader : IPackageManifestReader {

    public async Task<IEnumerable<PackageManifest>> ReadPackageManifestsAsync() {

        PackageManifest manifest = new() {
            Id = Alias,
            Name = Name,
            AllowTelemetry = true,
            Version = InformationalVersion,
            Extensions = [
                ..GetLocalizationExtensions(),
                ..GetPreviewExtensions(),
                ..GetSitemapChangeFrequencyExtensions(),
                ..GetSitemapPriorityExtensions()
            ],
            Importmap = new PackageManifestImportmap {
                Imports = new Dictionary<string, string> {
                    {"@limbo/seo/constants", $"/App_Plugins/{Alias}/Constants.js"}
                }
            }
        };

        return await Task.FromResult(new List<PackageManifest> { manifest });

    }

    private static IEnumerable<IExtension> GetLocalizationExtensions() {

        yield return new LocalizationExtension {
            Alias = $"{Alias}.Localization.EnUs",
            Name = $"{Name}: English (US)",
            Meta = new LocalizationMeta {
                Culture = "en",
                Localizations = new LocalizationDictionary {
                    ["limboSeo"] = new Dictionary<string, string> {
                        ["frequency_unspecified"] = "Unspecified",
                        ["frequency_always"] = "Always",
                        ["frequency_hourly"] = "Hourly",
                        ["frequency_daily"] = "Daily",
                        ["frequency_weekly"] = "Weekly",
                        ["frequency_monthly"] = "Monthly",
                        ["frequency_yearly"] = "Yearly",
                        ["frequency_never"] = "Never"
                    }
                }
            }
        };

        yield return new LocalizationExtension {
            Alias = $"{Alias}.Localization.DaDk",
            Name = $"{Name}: Danish (DK)",
            Meta = new LocalizationMeta {
                Culture = "da",
                Localizations = new LocalizationDictionary {
                    ["limboSeo"] = new Dictionary<string, string> {
                        ["frequency_unspecified"] = "Ikke angivet",
                        ["frequency_always"] = "Altid",
                        ["frequency_hourly"] = "Timevis",
                        ["frequency_daily"] = "Dagligt",
                        ["frequency_weekly"] = "Ugentligt",
                        ["frequency_monthly"] = "Månedligt",
                        ["frequency_yearly"] = "Årligt",
                        ["frequency_never"] = "Aldrig"
                    }
                }
            }
        };

    }

    private static IEnumerable<IExtension> GetPreviewExtensions() {

        yield return new PropertyEditorSchemaExtension {
            Alias = SeoPropertyEditorSchemaAliases.Preview,
            Name = $"{Name}: Preview Property Editor Schema",
            Meta = new PropertyEditorSchemaMeta {
                DefaultPropertyEditorUiAlias = SeoPropertyEditorUiAliases.Preview,
                Settings = new PropertyEditorSettings {
                    Properties = [
                        new PropertyEditorSettingsProperty {
                            Alias = "title",
                            Label = "Title properties",
                            Description = "Specify a comma separated list of properties that should be used for determining the page's SEO title.",
                            PropertyEditorUiAlias = UmbracoPropertyEditorUiAliases.TextBox
                        },
                        new PropertyEditorSettingsProperty {
                            Alias = "title",
                            Label = "Description properties",
                            Description = "Specify a comma separated list of properties that should be used for determining the page's SEO description.",
                            PropertyEditorUiAlias = UmbracoPropertyEditorUiAliases.TextBox
                        },
                        new PropertyEditorSettingsProperty {
                            Alias = "removeAsteriscs",
                            Label = "Remove asteriscs?",
                            Description = "Feature used by Limbo. Should probably be documented.",
                            PropertyEditorUiAlias = UmbracoPropertyEditorUiAliases.Toggle
                        }
                    ],
                    DefaultData = [
                        new PropertyEditorSettingsDefaultData { Alias = "title", Value = "title" },
                        new PropertyEditorSettingsDefaultData { Alias = "description", Value = "teaser" }
                    ]
                }
            }
        };

        yield return new PropertyEditorUiExtension {
            Alias = SeoPropertyEditorUiAliases.Preview,
            Name = $"{Name}: Preview Property Editor UI",
            Element = $"/App_Plugins/{Alias}/Elements/Preview.js",
            Meta = new PropertyEditorUiMeta {
                Label = "Limbo SEO Preview",
                Icon = "icon-chart",
                Group = "Limbo",
                PropertyEditorSchemaAlias = SeoPropertyEditorSchemaAliases.Preview
            }
        };

    }

    private static IEnumerable<IExtension> GetSitemapChangeFrequencyExtensions() {

        yield return new PropertyEditorSchemaExtension {
            Alias = SeoPropertyEditorSchemaAliases.SitemapChangeFrequency,
            Name = $"{Name}: Sitemap Change Frequency Property Editor Schema",
            Meta = new PropertyEditorSchemaMeta {
                DefaultPropertyEditorUiAlias = SeoPropertyEditorUiAliases.SitemapChangeFrequency
            }
        };

        yield return new PropertyEditorUiExtension {
            Alias = SeoPropertyEditorUiAliases.SitemapChangeFrequency,
            Name = $"{Name}: Sitemap Change Frequency Property Editor UI",
            Element = $"/App_Plugins/{Alias}/Elements/SitemapChangeFrequency.js",
            Meta = new PropertyEditorUiMeta {
                Label = "Limbo Sitemap Change Frequency",
                Icon = "icon-timer",
                Group = "Limbo",
                PropertyEditorSchemaAlias = SeoPropertyEditorSchemaAliases.SitemapChangeFrequency
            }
        };

    }

    private static IEnumerable<IExtension> GetSitemapPriorityExtensions() {

        yield return new PropertyEditorSchemaExtension {
            Alias = SeoPropertyEditorSchemaAliases.SitemapPriority,
            Name = $"{Name}: Sitemap Priority Property Editor Schema",
            Meta = new PropertyEditorSchemaMeta {
                DefaultPropertyEditorUiAlias = SeoPropertyEditorUiAliases.SitemapPriority
            }
        };

        yield return new PropertyEditorUiExtension {
            Alias = SeoPropertyEditorUiAliases.SitemapPriority,
            Name = $"{Name}: Sitemap Priority Property Editor UI",
            Element = $"/App_Plugins/{Alias}/Elements/SitemapPriority.js",
            Meta = new PropertyEditorUiMeta {
                Label = "Limbo Sitemap Priority",
                Icon = "icon-timer",
                Group = "Limbo",
                PropertyEditorSchemaAlias = SeoPropertyEditorSchemaAliases.SitemapPriority
            }
        };

    }

}