import {
    LIMBO_SEO_PREVIEW_ALIAS,
    LIMBO_SEO_PREVIEW_UI_ALIAS,
    LIMBO_SEO_SITEMAP_CHANGE_FREQUENCY_ALIAS,
    LIMBO_SEO_SITEMAP_CHANGE_FREQUENCY_UI_ALIAS,
    LIMBO_SEO_SITEMAP_PRIORITY_ALIAS,
    LIMBO_SEO_SITEMAP_PRIORITY_UI_ALIAS
} from './constants.js';

/**
 * Every extension this package contributes to the backoffice.
 *
 * Each property editor is declared twice: once as a `propertyEditorSchema`, which mirrors the C#
 * `DataEditor` and owns the data type configuration, and once as a `propertyEditorUi`, which owns
 * the element an editor actually interacts with. The two must use *different* aliases - the
 * extension registry keys on alias alone, so a schema and a UI sharing one means the second
 * registration is dropped with an "already registered" error and the editor never shows up.
 *
 * The `settings.properties` aliases below must match the `[ConfigurationField]` aliases on the
 * matching C# configuration class.
 */
export const manifests: Array<UmbExtensionManifest> = [

    // Localization

    {
        type: 'localization',
        alias: 'Limbo.Umbraco.Seo.Localization.En',
        name: 'Limbo SEO English',
        meta: { culture: 'en' },
        js: () => import('./lang/en.js')
    },

    // SEO preview

    {
        type: 'propertyEditorSchema',
        alias: LIMBO_SEO_PREVIEW_ALIAS,
        name: 'Limbo SEO Preview',
        meta: {
            defaultPropertyEditorUiAlias: LIMBO_SEO_PREVIEW_UI_ALIAS,
            settings: {
                properties: [
                    {
                        alias: 'title',
                        label: 'Title properties',
                        description: "Specify a comma separated list of properties that should be used for determining the page's SEO title.",
                        propertyEditorUiAlias: 'Umb.PropertyEditorUi.TextBox',
                        weight: 10
                    },
                    {
                        alias: 'description',
                        label: 'Description properties',
                        description: "Specify a comma separated list of properties that should be used for determining the page's SEO description.",
                        propertyEditorUiAlias: 'Umb.PropertyEditorUi.TextBox',
                        weight: 20
                    },
                    {
                        alias: 'removeAsteriscs',
                        label: 'Remove asteriscs?',
                        description: 'Feature used by Limbo. Should probably be documented.',
                        propertyEditorUiAlias: 'Umb.PropertyEditorUi.Toggle',
                        weight: 30
                    }
                ]
            }
        }
    },
    {
        type: 'propertyEditorUi',
        alias: LIMBO_SEO_PREVIEW_UI_ALIAS,
        name: 'Limbo SEO Preview',
        element: () => import('./property-editors/preview.element.js'),
        meta: {
            label: 'Limbo SEO Preview',
            icon: 'icon-chart',
            group: 'Limbo',
            propertyEditorSchemaAlias: LIMBO_SEO_PREVIEW_ALIAS
        }
    },

    // Sitemap change frequency

    {
        type: 'propertyEditorSchema',
        alias: LIMBO_SEO_SITEMAP_CHANGE_FREQUENCY_ALIAS,
        name: 'Limbo Sitemap Change Frequency',
        // No settings: SitemapFrequencyEditor deliberately doesn't expose a configuration editor,
        // matching the Umbraco 13 version of this package.
        meta: {
            defaultPropertyEditorUiAlias: LIMBO_SEO_SITEMAP_CHANGE_FREQUENCY_UI_ALIAS
        }
    },
    {
        type: 'propertyEditorUi',
        alias: LIMBO_SEO_SITEMAP_CHANGE_FREQUENCY_UI_ALIAS,
        name: 'Limbo Sitemap Change Frequency',
        element: () => import('./property-editors/sitemap-change-frequency.element.js'),
        meta: {
            label: 'Limbo Sitemap Change Frequency',
            icon: 'icon-timer',
            group: 'Limbo',
            propertyEditorSchemaAlias: LIMBO_SEO_SITEMAP_CHANGE_FREQUENCY_ALIAS
        }
    },

    // Sitemap priority

    {
        type: 'propertyEditorSchema',
        alias: LIMBO_SEO_SITEMAP_PRIORITY_ALIAS,
        name: 'Limbo Sitemap Priority',
        meta: {
            defaultPropertyEditorUiAlias: LIMBO_SEO_SITEMAP_PRIORITY_UI_ALIAS
        }
    },
    {
        type: 'propertyEditorUi',
        alias: LIMBO_SEO_SITEMAP_PRIORITY_UI_ALIAS,
        name: 'Limbo Sitemap Priority',
        element: () => import('./property-editors/sitemap-priority.element.js'),
        meta: {
            label: 'Limbo Sitemap Priority',
            icon: 'icon-caps-lock',
            group: 'Limbo',
            propertyEditorSchemaAlias: LIMBO_SEO_SITEMAP_PRIORITY_ALIAS
        }
    }

];
