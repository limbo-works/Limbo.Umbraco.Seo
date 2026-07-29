# Upgrading Limbo SEO from Umbraco 13 to Umbraco 17

This document recaps the work done on the `v17/dev` branch to move **Limbo.Umbraco.Seo** from Umbraco 13 (`net8.0`, AngularJS backoffice) to Umbraco 17 (`net10.0`, Lit/TypeScript backoffice).

The package version was bumped from `13.1.1` to `17.0.0`.

Release builds ship as a prerelease: `<VersionSuffix>` is set to `alpha000` for the `Release` configuration, so `dotnet pack -c Release` produces `Limbo.Umbraco.Seo.17.0.0-alpha000.nupkg`. This mirrors the existing `Debug` conditional, which stamps a `buildYYYYMMDDHHmm` suffix. Remove that line in `Limbo.Umbraco.Seo.csproj` — or bump the suffix — when Umbraco 17 support is ready to go out as a stable `17.0.0`.

Note that `Client/public/umbraco-package.json` carries a plain `"version": "17.0.0"`. It's a static file that can't follow the build configuration, and it only feeds the backoffice's package listing, so it tracks the base version rather than the prerelease suffix.

---

## Summary

The upgrade splits into two fairly independent halves:

1. **Server side (C#)** — a handful of APIs removed between v14 and v17 had to be replaced. The public shape of the package's own services (`ISitemapService`, `IRobotsTxtService`, `ISecurityTxtService`, `ISiteAccessor`) is unchanged, apart from one constructor.
2. **Backoffice (client)** — a full rewrite. Umbraco 14 replaced the AngularJS backoffice with a web-component backoffice, so the three AngularJS controllers and views were rewritten as Lit elements in a new TypeScript project.

Both `dotnet build` and the client's `tsc --noEmit && vite build` complete with no errors and no warnings, and `dotnet pack` produces a package containing the compiled client assets.

---

## Dependencies and target framework

| | Before | After |
|---|---|---|
| Target framework | `net8.0` | `net10.0` |
| `Umbraco.Cms.Core` | `[13.0.0,13.999)` | `[17.0.0,17.999)` |
| `Umbraco.Cms.Web.Website` | `[13.0.0,13.999)` | `[17.0.0,17.999)` |
| `Umbraco.Cms.Web.BackOffice` | `[13.0.0,13.999)` | *(removed)* |
| `Skybrud.Essentials` | `1.1.67` | `1.1.68` |
| `Skybrud.Essentials.AspNetCore` | `1.0.2` | `1.0.2` |

Umbraco 17 requires .NET 10.

`Umbraco.Cms.Web.BackOffice` has no version past 13 — the package was dissolved when the backoffice was rewritten in v14, and its server-side successor is `Umbraco.Cms.Api.Management`. Limbo SEO doesn't register any management API controllers (its only backoffice contributions are property editors, which live in `Umbraco.Cms.Core`), so the reference was dropped rather than replaced. This also trims the package's dependency surface.

---

## Server-side changes

### `IManifestFilter` removed

`Manifests/SeoManifestFilter.cs` was **deleted**, along with the `builder.ManifestFilters().Append<SeoManifestFilter>()` call in `Composers/SeoComposer.cs`.

Umbraco 14 removed the `IManifestFilter` / `PackageManifest` / `BundleOptions` API used to register backoffice scripts and stylesheets from C#. Packages now ship a `umbraco-package.json` file in their static web assets instead; see [Backoffice client](#backoffice-client) below.

### Property editors: the schema / UI split

Umbraco 14 split each property editor in two: a **schema** (server-side, owns the stored value and the data type configuration) and a **UI** (client-side, owns what the editor actually sees). The C# side lost everything that was purely presentational.

```csharp
// Before
[DataEditor(EditorAlias, EditorType.PropertyValue, EditorName, EditorView,
    ValueType = ValueTypes.String,
    Group = "Limbo",
    Icon = "icon-chart color-limbo")]

// After
[DataEditor(EditorAlias, ValueType = ValueTypes.String)]
```

`EditorType`, the display name, the view path, `Group` and `Icon` are all gone from the attribute; name, icon and group now live in the `propertyEditorUi` manifest. The `EditorView` constants (pointing at `/App_Plugins/.../Views/*.html`) were replaced with `EditorUiAlias` constants.

**Alias compatibility.** Each editor's UI alias is deliberately set to the *same string* as its schema alias:

```csharp
public const string EditorAlias   = "Limbo.Umbraco.Seo.Preview";
public const string EditorUiAlias = EditorAlias;
```

When a site is upgraded from Umbraco 13, Umbraco's data type migration copies the old editor alias into the new `EditorUiAlias` column. Reusing the same string means existing data types keep resolving to the right UI with no custom migration and no manual edits to the `umbracoDataType` table.

The property value converters still match on `propertyType.EditorAlias`, which is correct here because the schema alias is unchanged.

### Configuration editors

`IEditorConfigurationParser` was removed in v14, so the base constructor is now `ConfigurationEditor<T>(IIOHelper)`:

```csharp
// Before
public PreviewConfigurationEditor(IIOHelper ioHelper, IEditorConfigurationParser parser)
    : base(ioHelper, parser) { }

// After
public PreviewConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) { }
```

`[ConfigurationField]` also lost its presentational arguments. Label, description and editor moved into the manifest, leaving only the alias — which is the storage contract:

```csharp
// Before
[ConfigurationField("title", "Title properties", view: "textstring", Description = "...")]

// After
[ConfigurationField("title")]
```

The aliases in `Client/src/manifests.ts` under `meta.settings.properties` must stay in sync with these.

### `ChildrenForAllCultures` removed

`IPublishedContent.ChildrenForAllCultures` no longer exists. In `SitemapService.BuildSitemap`, passing `"*"` as the culture is the replacement:

```csharp
// Before
foreach (IPublishedContent child in node.ChildrenForAllCultures) ...

// After
foreach (IPublishedContent child in node.Children("*")) ...
```

### Domain resolution moved off `IDomainService`

`SiteAccessor` previously resolved the current site by calling `IDomainService.GetAll(false)`, which is now marked obsolete. It also queried the database on **every** request to `/sitemap.xml`, `/robots.txt` and `/security.txt`.

It now reads from the published domain cache exposed on the Umbraco context:

```csharp
IReadOnlyList<Domain> domains = umbracoContext.Domains?.GetAll(false).ToArray() ?? [];
Domain? domain = domains.FirstOrDefault(d => IsMatch(url, d));
IPublishedContent? content = umbracoContext.Content?.GetById(domain.ContentId);
```

This removes the obsolete-API warning, avoids a database round trip per request, and drops the `IDomainService` dependency.

> **Breaking change for consumers.** `SiteAccessor`'s constructor changed from `(IDomainService, IUmbracoContextAccessor)` to `(IUmbracoContextAccessor)`, and the protected `IsMatch` overload now takes `Umbraco.Cms.Core.Routing.Domain` instead of `Umbraco.Cms.Core.Models.IDomain`. Anyone who subclassed `SiteAccessor` will need to adjust. The `ISiteAccessor` interface itself is unchanged.

### Deliberately *not* changed

`SitemapFrequencyEditor` still does not wire up a configuration editor, even though `SitemapFrequencyConfiguration` and `SitemapFrequencyConfigurationEditor` exist. That was already the case in v13: `UseNullable` is not honoured by `SitemapFrequencyValueConverter`, so activating it during the upgrade would have surfaced a data type setting that does nothing. The classes were kept (removing them would be a separate breaking change) and the situation is now documented in a comment on the editor.

---

## Backoffice client

The AngularJS assets were deleted:

```
wwwroot/Scripts/Controllers/Preview.js
wwwroot/Scripts/Controllers/SitemapChangeFrequency.js
wwwroot/Scripts/Controllers/SitemapPriority.js
wwwroot/Views/Preview.html
wwwroot/Views/SitemapChangeFrequency.html
wwwroot/Views/SitemapPriority.html
wwwroot/Styles/*.less, Styles.css
compilerconfig.json, compilerconfig.json.defaults
```

The LESS files and the Web Compiler config went with them: the new backoffice uses shadow DOM, so component styles live inside each element rather than in a global stylesheet.

In their place there is a Vite/TypeScript project:

```
src/Limbo.Umbraco.Seo/Client/
├── package.json, tsconfig.json, vite.config.ts
├── public/umbraco-package.json          → copied to wwwroot, registers the bundle
└── src/
    ├── manifests.ts                     → bundle entry; all extension manifests
    ├── constants.ts                     → aliases and option lists shared by the elements
    ├── lang/en.ts                       → localization for the change frequency labels
    └── property-editors/
        ├── preview.element.ts
        ├── sitemap-change-frequency.element.ts
        └── sitemap-priority.element.ts
```

`umbraco-package.json` registers a single `bundle` extension pointing at the built entry file, and the bundle exports every manifest:

```json
{
  "extensions": [
    {
      "type": "bundle",
      "alias": "Limbo.Umbraco.Seo.Bundle",
      "name": "Limbo SEO Bundle",
      "js": "/App_Plugins/Limbo.Umbraco.Seo/limbo-seo.js"
    }
  ]
}
```

Each editor is declared as a `propertyEditorSchema` (matching the C# `DataEditor`, and carrying the data type settings) plus a `propertyEditorUi` (carrying the label, icon, group and element).

### The three elements

**Sitemap change frequency** and **sitemap priority** are straightforward button lists built from `uui-button`, dispatching `UmbChangeEvent` on selection and honouring the `readonly` property. Both keep their v13 stored values, so no content migration is needed. The priority element additionally normalises its incoming value to a number, because the schema stores a decimal while content saved by older versions of the package may still hold a string such as `"0.5"` — which is exactly what `SitemapPriorityValueConverter` already tolerates on the server.

**The preview element** needed the most thought. The old AngularJS controller reached directly into `editorState.current.variants[0].tabs[*].properties[*]` and `$scope.$watch`-ed the paths it found, with a `// TODO: Add support for variants` noting that it always used the first variant. The Lit version consumes `UMB_PROPERTY_DATASET_CONTEXT` and observes `propertyValueByAlias(alias)` for each configured alias, plus the dataset's `name`. That is both the supported API and a behavioural improvement: the dataset context is scoped to the variant the editor is actually working on, so the preview now follows the right culture instead of always showing the first one.

The configuration (`title`, `description`, `removeAsteriscs`), the fallback alias chains (`seoTitle`/`title` and `seoMetaDescription`/`teaser`/`introTeaser`), the fall back to the node name, the asterisk stripping, and the 60/160 character truncation all behave as before.

Localization moved from `wwwroot/Lang/*.xml` to a `localization` manifest (`lang/en.ts`), keeping the existing `limboSeo_frequency_*` keys.

### Findings from the extension review

The new client code was reviewed against the Umbraco 17 extension conventions. Four things came out of it that are worth recording, because they are behaviours the AngularJS originals got away with and the new backoffice does not.

**Unsaved values are `undefined`, not the default.** `umb-property` assigns `element.value = undefined` for a property that has never been saved. Both button lists initially treated that as "the default is selected", which was wrong in two ways for the priority editor: the UI claimed a value the server didn't have (`SitemapPriorityValueConverter` maps a missing value to `null`, not to `0.5`), and because clicking the already-"selected" 0.5 button was a no-op, **0.5 could never actually be saved**. The value and the rendered selection are now separate: `#value` stays `undefined` until an editor picks something, and a private `#selected` getter supplies the default for display only. The change frequency editor had the milder version of the same problem — `null` matched none of the buttons, so nothing rendered as selected — and now normalises incoming values to `''`.

**Mandatory support.** `UmbPropertyEditorUiElement` declares `mandatory` and `mandatoryMessage`, and `umb-property` assigns both, but neither button list handled them, so marking either property mandatory produced no client-side feedback at all — the save just failed server-side. Both now build on `UmbFormControlMixin` and register a `valueMissing` validator. The priority editor's validator checks `value === undefined` rather than falsiness, since `0` is a legitimate priority.

**Stale observers in the preview.** The preview subscribes to one observable per configured alias. Editing a data type's alias list while a content node was open left the old subscriptions running and their values lingering in the cache, so a removed alias could still feed the preview. Subscriptions are now tracked and dropped when they fall out of the configuration.

**A comment that was simply wrong.** Both value setters carried a comment claiming Lit does not wrap custom accessors and that the setter must call `requestUpdate` itself. That was true of Lit 1, but `ReactiveElement.getPropertyDescriptor` has wrapped user-defined accessors since Lit 2 — verified against the installed `@lit/reactive-element` 2.1.2. The redundant calls and the misleading comment are gone.

Smaller fixes: `role="group"` on both button lists so the buttons are exposed as one control; a getter for the preview's `config` property, which was write-only and so broke the `UmbPropertyEditorUiElement` contract; and an ellipsis on the preview's truncation, which previously hard-cut mid-word.

One review suggestion was **not** taken: the preview's hardcoded `#ffffff` / `#1a0dab` / `#545454` stay as they are. Those are Google's colours, and the entire point of the editor is to show how the page will look in a search result — which is a light surface whatever theme the backoffice is in. It is now styled as a card and carries a comment saying the choice is deliberate, so it doesn't read as an oversight.

### Build output

Vite emits an ES bundle into `src/Limbo.Umbraco.Seo/wwwroot`, which the Razor SDK serves as static web assets under `/App_Plugins/Limbo.Umbraco.Seo` (via the existing `StaticWebAssetBasePath`). Everything under `@umbraco-cms/*` is left external — the backoffice supplies those modules at runtime through its import map — so the bundle stays a few kilobytes.

Two deliberate choices here:

- **Filenames carry no content hash.** The output is committed to the repository so that `dotnet build` and `dotnet pack` work on a machine without Node installed. Hashed names would create a new set of files on every rebuild.
- **`Client/**` is excluded from the `.csproj`** (`Content`, `None`, `Compile`, `EmbeddedResource`), so `node_modules` and the TypeScript sources never end up in the NuGet package.

The consequence is that **the client must be rebuilt and committed whenever anything under `Client/src` changes**:

```sh
cd src/Limbo.Umbraco.Seo/Client
npm ci
npm run build
```

`debug.bat` and `release.bat` now run this step before packing, so the release flow can't ship a stale bundle. `debug.bat` also had its local feed path updated from `c:/nuget/Umbraco13` to `c:/nuget/Umbraco17`.

---

## Other files touched

- **`README.md`** — installation section retargeted at 17.0.0, `v13/main` added to the version list, and the stale "Usage" section rewritten. It previously described a WebAPI controller at `/umbraco/api/Sitemap/XmlSitemap` plus an IIS rewrite rule; the package has served `/sitemap.xml`, `/robots.txt` and `/security.txt` from middleware since v13. A short "Development" section covering the client build was added.
- **`CLAUDE.md`** — repository guidance updated for the new layout.

---

## Verification performed

- `dotnet build src/Limbo.Umbraco.Seo` — succeeds, no compiler warnings.
- `dotnet build ... /t:pack` — succeeds; the resulting `.nupkg` contains `lib/net10.0/Limbo.Umbraco.Seo.dll` and all six client assets plus `umbraco-package.json` under `staticwebassets/`.
- `npx tsc --noEmit` — clean. This is meaningful validation rather than a formality: the Umbraco manifest types are strongly typed, so a wrong `type`, a missing `meta` field or a bad settings shape in `manifests.ts` fails the type check.
- `npm run build` — clean.
- An extension review against the Umbraco 17 conventions; see [Findings from the extension review](#findings-from-the-extension-review). All High and Medium findings are fixed.

### Not verified

The package has **not** been run inside a live Umbraco 17 instance. The following are the areas most worth exercising manually before release:

- The three property editors rendering and saving in the backoffice, including on a variant (culture-aware) document. Worth covering specifically: saving priority `0.5` and priority `0.0`, an unsaved property of each type, and a property marked mandatory — these are the paths the review changed, and they are validated by the compiler but not by a running backoffice.
- Upgrading a site that already has v13 data types, to confirm the `EditorUiAlias` migration lands on the aliases this package registers.
- `/sitemap.xml`, `/robots.txt` and `/security.txt` on a multi-site install, to confirm the switch to the published domain cache resolves the same sites as `IDomainService` did.


## Output from claude code review session
Fixed (7):
1. csproj:45 — Umbraco range [17.0.0,17.9.9) blocked 17.10+ → 17.999
2. release.bat/debug.bat — no errorlevel check after npm; a failed tsc/vite packed a stale (or emptied) bundle → added exit /b 1
3. SiteAccessor.cs — GetRequiredUmbracoContext() moved to top of TryGetSite, could throw instead of returning false → TryGetUmbracoContext
4. Sitemap priority — Lit editor now submits a JSON number, but converter/extensions only handled float/string → added double/decimal (priority would silently vanish from sitemap)
5. preview.element.ts — unguarded await in alias loop; stale variant could win the observe race → generation counter
6. SeoPackage.cs:52 — docs URL still v13
7. umbraco-package.json — dangling $schema path

Left alone (flagged):
- Manifest version hardcoded 17.0.0 in 2 files (was auto from assembly via deleted SeoManifestFilter) — needs MSBuild rewrite, your call
- .js.map sourcemaps now ship in nupkg (~48 KB) — behaviour change, possibly intended
- SitemapService.IgnoreNode:96 — TemplateId <= 0 false when null; pre-existing

Post-fix: build 0 errors/0 warnings, tsc --noEmit clean, pack emits 13 static assets.