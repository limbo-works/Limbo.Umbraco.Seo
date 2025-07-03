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

### Umbraco 13

The Umbraco 13 version of this package is only available via [**NuGet**](https://www.nuget.org/packages/Limbo.Umbraco.Seo/13.0.1). To install the package, you can use either .NET CLI:

```
dotnet add package Limbo.Umbraco.Seo --version 13.0.1
```

or the NuGet Package Manager:

```
Install-Package Limbo.Umbraco.Seo -Version 13.0.1
```

### Other versions of Umbraco

- ~~[**`v10/main`**](https://github.com/limbo-works/Limbo.Umbraco.Seo/tree/v10/main) Umbraco 10, 11 and 12~~ <sub title="Umbraco 10, 11 and 12 have reached end-of-life"><sup>(EOL)</sup></sub>
- ~~[**`v2/main`**](https://github.com/limbo-works/Limbo.Umbraco.Seo/tree/v2/main) Umbraco 9~~ <sub title="Umbraco 9 has reached end-of-life"><sup>(EOL)</sup></sub>
- ~~[**`v1/main`**](https://github.com/limbo-works/Limbo.Umbraco.Seo/tree/v1/main) Umbraco 8~~ <sub title="Umbraco 8 has reached end-of-life"><sup>(EOL)</sup></sub>





<br /><br />

## Screenshots

![image](https://user-images.githubusercontent.com/3634580/148427849-7ac515ad-de78-49bc-8312-6782fb9fdf55.png)




<br /><br />

## Usage

### Sitemap

The package contains a WebAPI controller, which by default can be accessed at `/umbraco/api/Sitemap/XmlSitemap`. The controller outputs the XML sitemap for the site matching the inbound domain, or an error if the domain isn't recognized.

To make the sitemap appear at `/sitemap.xml` instead, you can add the following IIS rewrite rule:

```xml
<rule name="sitemap" stopProcessing="true" patternSyntax="ExactMatch">
  <match url="sitemap.xml" />
  <action type="Rewrite" url="/umbraco/api/sitemap/XmlSitemap" appendQueryString="false" redirectType="Found" statusCode="200" />
</rule>
```

Under the hood, the XML sitemap generation is handled by the `ISitemapService` interface. The default implementation of the interface is `SitemapService`, but implementation can be overridden using dependency injection.
