using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Static class with various exrension methods related to building sitemaps
    /// </summary>
    public static class SitemapExtensions {

        /// <summary>
        /// Returns a new <see cref="HttpResponseMessage"/> wrapping the specified <paramref name="result"/>.
        /// </summary>
        /// <param name="result">The <see cref="ISitemapResult"/> to be wrapped.</param>
        /// <returns>An instance of <see cref="HttpResponseMessage"/>.</returns>
        public static HttpResponseMessage AsResponseMessage(this ISitemapResult result) {

            StringBuilder sb = new();

            XmlWriterSettings xws = new() {
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

        /// <summary>
        /// Writes the specified <paramref name="result"/> to <paramref name="stream"/>.
        /// </summary>
        /// <param name="result">The <see cref="ISitemapResult"/> to write.</param>
        /// <param name="stream">The stream to write to.</param>
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