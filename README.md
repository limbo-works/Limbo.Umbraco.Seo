<table>
  <thead>
    <tr>
      <td align="left">
        :warning:
      </td>
      <td align="left" width="100%">
          <strong>NOTICE</strong>
      </td>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td colspan="2">
          The Umbraco 8 package has been retired and is no longer in active development.
      </td>
    </tr>
  </tbody>
</table>

# Limbo SEO

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md)
[![NuGet](https://img.shields.io/nuget/vpre/Limbo.Umbraco.Seo.svg)](https://www.nuget.org/packages/Limbo.Umbraco.Seo)
[![NuGet](https://img.shields.io/nuget/dt/Limbo.Umbraco.Seo.svg)](https://www.nuget.org/packages/Limbo.Umbraco.Seo)
[![Our Umbraco](https://img.shields.io/badge/our-umbraco-%233544B1)](https://our.umbraco.com/packages/developer-tools/limbo-seo/)


**Limbo SEO** is a package for helping improving the SEO experience in the Umbraco backoffice. The package currently includes:


**Property Editors**  

- SEO Preview - eg. like the page will be shown on Google
- Sitemap Frequency Picker - let's editors set the sitemap update frequency of a given page
- Sitemap Page Priority - let's editors specify the page priority of a given page

**Other**  

- Logic for easily building sitemaps - extendable via the `ISitemapHelper` interface and the `SitemapHelper` class
- more...



<br /><br /><br />

## Installation

The Umbraco 8 version of this package is only available via NuGet. To install the package, you can use either .NET CLI:

```
dotnet add package Limbo.Umbraco.Seo --version 1.0.0
```

or the NuGet Package Manager:

```
Install-Package Limbo.Umbraco.Seo -Version 1.0.0
```

**Umbraco 9**  
For the Umbraco 9 version of this package, see the [**v2/main**](https://github.com/limbo-works/Limbo.Umbraco.Seo/tree/v2/main) branch instead.

**Umbraco 10-12**  
For the Umbraco 10 version of this package, see the [**v3/main**](https://github.com/limbo-works/Limbo.Umbraco.Seo/tree/v3/main) branch instead.




<br /><br /><br />

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

Under the hood, the XML sitemap generation is handled by the `ISitemapHelper` interface. The default implementation of the interface is `SitemapHelper`, but implementation can be overridden using dependency injection.
