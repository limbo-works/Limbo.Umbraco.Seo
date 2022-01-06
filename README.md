# Limbo SEO

## Installation

The Umbraco 9 version of this package is only available via NuGet. To install the package, you can use either .NET CLI:

```
dotnet add package Limbo.Umbraco.Seo --version 2.0.0-alpha001
```

or the older NuGet Package Manager:

```
Install-Package Limbo.Umbraco.Seo -Version 2.0.0-alpha001
```

**Umbraco 8**  
For the Umbraco 8 version of this package, see the [**v1/latest**](https://github.com/limbo-works/Limbo.Umbraco.Seo/tree/v1/main) branch instead.

## Screenshots

![image](https://user-images.githubusercontent.com/3634580/148427849-7ac515ad-de78-49bc-8312-6782fb9fdf55.png)

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
