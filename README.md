# Limbo SEO

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/limbo-works/Limbo.Umbraco.Seo/blob/v13/main/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/vpre/Limbo.Umbraco.Seo.svg)](https://www.nuget.org/packages/Limbo.Umbraco.Seo)
[![NuGet](https://img.shields.io/nuget/dt/Limbo.Umbraco.Seo.svg)](https://www.nuget.org/packages/Limbo.Umbraco.Seo)
[![Our Umbraco](https://img.shields.io/badge/our-umbraco-%233544B1)](https://our.umbraco.com/packages/developer-tools/limbo-seo/)
[![Umbraco Marketplace](https://img.shields.io/badge/umbraco-marketplace-%233544B1)](https://marketplace.umbraco.com/package/limbo.umbraco.seo)
[![Limbo.Umbraco.Seo at packages.limbo.works](https://img.shields.io/badge/limbo-packages-blue)](https://packages.limbo.works/limbo.umbraco.seo/)


**Limbo SEO** is a package for helping improving the SEO experience in the Umbraco backoffice. While still under development, the package currently includes:

 

**Property Editors**  

- SEO Preview - eg. like the page will be shown on Google
- Sitemap Frequency Picker - let's editors set the sitemap update frequency of a given page
- Sitemap Page Priority - let's editors specify the page priority of a given page

**Other**  

- Logic for easily building sitemaps - extendable via the `ISitemapHelper` interface and the `SitemapHelper` class
- More to come 😎














<br /><br />

## Installation

### Umbraco 17

The Umbraco 17 version of this package is only available via [**NuGet**](https://www.nuget.org/packages/Limbo.Umbraco.Seo/17.0.0-alpha000), and is currently released as a **prerelease**. To install the package, you can use either .NET CLI:

```
dotnet add package Limbo.Umbraco.Seo --version 17.0.0-alpha000
```

or the NuGet Package Manager:

```
Install-Package Limbo.Umbraco.Seo -Version 17.0.0-alpha000
```

Since this is a prerelease, an explicit version (or `--prerelease`) is required — NuGet won't resolve it otherwise.

### Other versions of Umbraco

- [**`v13/main`**](https://github.com/limbo-works/Limbo.Umbraco.Seo/tree/v13/main) Umbraco 13
- ~~[**`v10/main`**](https://github.com/limbo-works/Limbo.Umbraco.Seo/tree/v10/main) Umbraco 10, 11 and 12~~ <sub title="Umbraco 10, 11 and 12 have reached end-of-life"><sup>(EOL)</sup></sub>
- ~~[**`v2/main`**](https://github.com/limbo-works/Limbo.Umbraco.Seo/tree/v2/main) Umbraco 9~~ <sub title="Umbraco 9 has reached end-of-life"><sup>(EOL)</sup></sub>
- ~~[**`v1/main`**](https://github.com/limbo-works/Limbo.Umbraco.Seo/tree/v1/main) Umbraco 8~~ <sub title="Umbraco 8 has reached end-of-life"><sup>(EOL)</sup></sub>





<br /><br />

## Screenshots

![image](https://user-images.githubusercontent.com/3634580/148427849-7ac515ad-de78-49bc-8312-6782fb9fdf55.png)




<br /><br />

## Usage

### Sitemap, robots.txt and security.txt

The package registers a middleware that serves three files for the site matching the inbound domain:

| URL | Source |
|-----|--------|
| `/sitemap.xml` | Built by walking the content tree below the site root |
| `/robots.txt` | The `robotsTxt` property on the site root |
| `/security.txt` | The `securityTxt` property on the site root |

Under the hood these are handled by the `ISitemapService`, `IRobotsTxtService` and `ISecurityTxtService` interfaces. The default implementations are `SitemapService`, `RobotsTxtService` and `SecurityTxtService`, and each can be replaced through dependency injection. `SitemapService` in particular exposes virtual `IgnoreNode`, `IgnoreChildren` and `CreateItem` methods, so subclassing it is usually enough to customise which pages end up in the sitemap.

<br /><br />

## Development

The C# project builds on its own:

```
dotnet build src/Limbo.Umbraco.Seo
```

The backoffice extensions are a Vite/TypeScript project under `src/Limbo.Umbraco.Seo/Client`. Its build output is written to `src/Limbo.Umbraco.Seo/wwwroot` and is committed to the repository, so you only need to rebuild it after changing anything under `Client/src`:

```
cd src/Limbo.Umbraco.Seo/Client
npm ci
npm run build
```
