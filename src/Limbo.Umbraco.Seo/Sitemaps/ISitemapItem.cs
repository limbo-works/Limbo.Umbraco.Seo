using System;
using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {
    
    public interface ISitemapItem {

        string Url { get; }

        DateTime LastModified { get; }

        string ChangeFrequency { get; }

        string PagePriority { get; }

        XElement ToXml();

    }

}