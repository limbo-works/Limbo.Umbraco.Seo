using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {
    
    public static class SitemapExtensions {

        public static ISitemapResult BuildSitemap(this ISitemapHelper helper) {
            return helper.BuildSitemap(new HttpContextWrapper(HttpContext.Current));
        }

        public static ISitemapResult BuildSitemap(this ISitemapHelper helper, HttpContext context) {
            return helper.BuildSitemap(new HttpContextWrapper(context ?? HttpContext.Current));
        }

        public static HttpResponseMessage AsResponseMessage(this ISitemapResult result) {
            
            StringBuilder sb = new StringBuilder();

            XmlWriterSettings xws = new XmlWriterSettings {
                OmitXmlDeclaration = false,
                Indent = true
            };
            
            using (XmlWriter xw = XmlWriter.Create(sb, xws)) {  
                result.ToXml().WriteTo(xw);  
            }  
            
            return new HttpResponseMessage {
                Content = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml")
            };

        }

        public static void WriteTo(this ISitemapResult result, Stream stream) {
            
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            // Convert the result to an instance of "XDocument"
            XDocument document = result.ToXml();
            
            // Write the XML to "stream"
            document.Save(stream, SaveOptions.None);

        }

        public static void WriteTo(this ISitemapResult result, HttpResponse response) {
            response.Clear();
            response.ContentType = "application/xml";
            result.WriteTo(response.OutputStream);
            response.End();
        }

        public static void WriteTo(this ISitemapResult result, HttpResponseBase response) {
            response.Clear();
            response.ContentType = "application/xml";
            result.WriteTo(response.OutputStream);
            response.End();
        }

    }

}