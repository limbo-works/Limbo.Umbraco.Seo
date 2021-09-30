using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {

    public interface ISitemapResult {
        
        Exception Exception { get; }
        
        List<ISitemapItem> Items { get; }

        XDocument ToXml();

    }

}