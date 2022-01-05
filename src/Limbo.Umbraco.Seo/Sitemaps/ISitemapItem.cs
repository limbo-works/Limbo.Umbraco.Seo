using System;
using System.Xml.Linq;
using Limbo.Umbraco.Seo.Models.Sitemaps;

namespace Limbo.Umbraco.Seo.Sitemaps {

    public interface ISitemapItem {

        string Url { get; }

        DateTime LastModified { get; }

        SitemapChangeFrequency ChangeFrequency { get; }

        float? PagePriority { get; }

        XElement ToXml();

    }

}