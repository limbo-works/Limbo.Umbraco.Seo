using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {

    public static class SitemapExtensions {

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

        //public static void WriteTo(this ISitemapResult result, HttpResponse response) {
        //    response.Clear();
        //    response.ContentType = "application/xml";
        //    result.WriteTo(response.OutputStream);
        //    response.End();
        //}

    }

}