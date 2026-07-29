# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

**Limbo.Umbraco.Seo** — a NuGet package for Umbraco CMS adding SEO features: XML sitemap generation, `robots.txt` / `security.txt` serving, and backoffice property editors (SEO preview, sitemap change frequency, sitemap priority). One C# class library (`Microsoft.NET.Sdk.Razor`, net10.0) plus a Vite/TypeScript backoffice client. No test project.

## Commands

```sh
# C# build
dotnet build src/Limbo.Umbraco.Seo

# Backoffice client (only needed after changing anything under Client/src)
cd src/Limbo.Umbraco.Seo/Client && npm ci && npm run build   # tsc --noEmit + vite build

# Build + pack release NuGet into releases/nuget (release.bat does client + this)
dotnet build src/Limbo.Umbraco.Seo --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget
```

`debug.bat` / `release.bat` are Windows wrappers that run the client build first, then pack. Debug builds get a `buildYYYYMMDDHHmm` version suffix automatically.

## Branching

Branches are versioned per Umbraco major. `v17/dev` targets Umbraco 17 / .NET 10 and is the current line; `v13/main` is still the repo's default branch and the usual PR target for v13 work. Older majors (`v10/main`, `v2/main`, `v1/main`) are EOL. Package version lives in `<VersionPrefix>` in the `.csproj`.

## Architecture

Everything server-side is wired in `Composers/SeoComposer.cs`: it registers services with `AddUnique` (so consuming sites can replace any of them) and inserts `SeoMiddleware` into the Umbraco pre-pipeline.

**Request flow:** `Middleware/SeoMiddleware.cs` intercepts `/robots.txt`, `/security.txt`, and `/sitemap.xml` (paths in `Constants/SeoUrls.cs`) before Umbraco routing, ensures an Umbraco context, delegates to the matching service, and writes the response itself. Everything else passes through.

**Site resolution:** `Sites/ISiteAccessor` maps an `HttpContext` to an `ISite` (the root content node for the request's domain) by matching against `IUmbracoContext.Domains`, the published domain cache — not `IDomainService`, which is obsolete and hits the database per request. All three services depend on it; robots/security values are read as properties off the site node using aliases in `Constants/SeoProperties.cs`.

**Feature verticals:** `Sitemaps/`, `RobotsTxt/`, `SecurityTxt/` each contain `Models/` (interface + implementation pairs, e.g. `ISitemapResult`/`SitemapResult`) and `Services/` (`I*Service`/`*Service`). Service methods are `virtual` by design — the documented extension model is subclassing (e.g. override `SitemapService.CreateItem`, `IgnoreNode`, `IgnoreChildren`) and re-registering via DI. Result models carry status/exception rather than throwing; the middleware logs the exception and maps status to HTTP code.

**Property editors** are split across the two languages, as Umbraco 14+ requires:

- C# in `Editors/` owns the *schema*: a `DataEditor` subclass carrying the alias and value type, plus (for the preview editor) a `ConfigurationEditor` and a POCO whose `[ConfigurationField]` aliases are the storage contract.
- TypeScript in `Client/src/` owns the *UI*: `manifests.ts` declares a `propertyEditorSchema` and a `propertyEditorUi` per editor, and the Lit elements under `Client/src/property-editors/` render them.

Two invariants to preserve when touching either side: the UI alias is deliberately **identical** to the schema alias (Umbraco's v13→v14 data type migration copies the old editor alias into `EditorUiAlias`, so matching them avoids a custom migration), and `settings.properties` aliases in `manifests.ts` must match the `[ConfigurationField]` aliases in C#. `SitemapFrequencyEditor` intentionally exposes *no* configuration editor even though `SitemapFrequencyConfiguration` exists — `UseNullable` is not honoured by the value converter, so wiring it up would surface a dead setting.

**Client build:** Vite emits an ES bundle from `Client/src/manifests.ts` into `wwwroot/` with hash-free filenames; `Client/public/umbraco-package.json` is copied alongside it and registers the bundle. `StaticWebAssetBasePath` serves `wwwroot` as `App_Plugins/Limbo.Umbraco.Seo`. **The build output in `wwwroot/` is committed**, so `dotnet build`/`pack` works without Node installed — rebuild and commit it whenever `Client/src` changes. `Client/**` is excluded from the csproj so `node_modules` never reaches the NuGet package.

## Code style

Per `src/.editorconfig`: opening braces on the same line (K&R, including classes/namespaces), 4-space indent, CRLF, no final newline, `var` discouraged (explicit types), file-scoped namespaces. Public members carry XML doc comments; `#pragma warning disable CS1591` where they're intentionally omitted. The TypeScript follows the same 4-space, same-line-brace shape.
