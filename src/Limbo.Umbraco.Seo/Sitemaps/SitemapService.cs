using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Limbo.Umbraco.Seo.Extensions;
using Limbo.Umbraco.Seo.Models.Sitemaps;
using Limbo.Umbraco.Seo.Sites;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Skybrud.Essentials.Strings.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

// ReSharper disable ConvertIfStatementToReturnStatement

namespace Limbo.Umbraco.Seo.Sitemaps;

/// <summary>
/// Class representing the default implementation of the XML sitemap helper used by this package.
/// </summary>
public class SitemapService : ISitemapService {

    private readonly ILogger<SitemapService> _logger;
    private readonly ISiteAccessor _siteAccessor;

    /// <summary>
    /// Initializes a new instance based on the specified DI dependencies.
    /// </summary>
    /// <param name="logger">The current logger.</param>
    /// <param name="siteAccessor">The current site accessor.</param>
    public SitemapService(ILogger<SitemapService> logger, ISiteAccessor siteAccessor) {
        _logger = logger;
        _siteAccessor = siteAccessor;
    }

    /// <summary>
    /// Returns an instance of <see cref="ISitemapResult"/> for the specified HTTP <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>An instance of <see cref="ISitemapResult"/>.</returns>
    public virtual ISitemapResult BuildSitemap(HttpContext context) {

        try {

            if (!_siteAccessor.TryGetSite(context, out ISite? site)) {
                throw new SitemapException("Failed determining site from HTTP context.");
            }

            return BuildSitemap(new SitemapContext(context, site));

        } catch (Exception ex) {

            return new SitemapResult(ex);

        }

    }

    /// <summary>
    /// Returns an instance of <see cref="ISitemapResult"/> for the specified sitemap <paramref name="context"/>.
    /// </summary>
    /// <param name="context">A reference to the current sitemap context.</param>
    /// <returns>An instance of <see cref="ISitemapResult"/>.</returns>
    protected virtual ISitemapResult BuildSitemap(ISitemapContext context) {

        try {

            List<ISitemapItem> items = new();

            // Start recursively building the sitemap
            BuildSitemap(context, items, context.Site.Content);

            return new SitemapResult(items);

        } catch (Exception ex) {

            return new SitemapResult(ex);

        }

    }

    /// <summary>
    /// Returns whether the specified content <paramref name="node"/> should be ignored from the sitemap.
    /// </summary>
    /// <param name="context">A reference to the current sitemap context.</param>
    /// <param name="node">The content node.</param>
    /// <returns><see langword="true"/> if <paramref name="node"/> should be ignored; otherwise, <see langword="false"/>.</returns>
    protected virtual bool IgnoreNode(ISitemapContext context, IPublishedContent node) {

        // Skip formRender
        if (node.ContentType.Alias == "formRender") return true;

        if (node.TemplateId <= 0) return true;
        if (node.Value<bool>(SitemapConstants.Properties.HideFromSitemap)) return true;

        return false;

    }

    /// <summary>
    /// Returns whether children under the specified content <paramref name="node"/> should be ignored from the sitemap.
    /// </summary>
    /// <param name="context">A reference to the current sitemap context.</param>
    /// <param name="node">The content node.</param>
    /// <returns><see langword="true"/> if children under <paramref name="node"/> should be ignored; otherwise, <see langword="false"/>.</returns>
    protected virtual bool IgnoreChildren(ISitemapContext context, IPublishedContent node) {

        // Skip formRender
        if (node.ContentType.Alias == "formRender") return true;

        return false;

    }

    /// <summary>
    /// Recursively builds the sitemap based on the specified sitemap <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="items">A list of all the sitemap items.</param>
    /// <param name="node">The current content node.</param>
    protected virtual void BuildSitemap(ISitemapContext context, List<ISitemapItem> items, IPublishedContent node) {

        if (!IgnoreNode(context, node)) items.Add(CreateItem(context, node));

        if (IgnoreChildren(context, node)) return;

#pragma warning disable CS0168

        try {

            // Run the same for all children
            foreach (IPublishedContent child in node.ChildrenForAllCultures!) BuildSitemap(context, items, child);

        } catch (Exception ex) {

#if DEBUG
            _logger.LogError(ex, $"Failed generating sitemap for node with ID {node.Id}");
#else
                // ignore
#endif

        }

#pragma warning restore CS0168

    }

    /// <summary>
    /// Creates and returns a new <see cref="SitemapItem"/> instance based on the specified HTTP context <paramref name="context"/> and content node <paramref name="node"/>.
    /// </summary>
    /// <param name="context">A reference to the current sitemap context.</param>
    /// <param name="node">An instance of <see cref="IPublishedContent"/> representing the node.</param>
    /// <returns>An instance of <see cref="SitemapItem"/>.</returns>
    protected virtual ISitemapItem CreateItem(ISitemapContext context, IPublishedContent node) {

        // Get the absolute of the node
        string absoluteUrl = node.Url(mode: UrlMode.Absolute);

        // Initialize a new item
        SitemapItem item = new(absoluteUrl) {
            LastModified = node.UpdateDate // TODO: Should this be UTC?
        };

        if (node.TryGetSitemapChangeFrequency(out SitemapChangeFrequency frequency)) {
            item.ChangeFrequency = frequency;
        }

        if (node.TryGetSitemapPriority(out float priority)) {
            item.PagePriority = priority;
        }

        return item;

    }


    /// <summary>
    /// Returns an <see cref="XDocument"/> representing the specified <paramref name="sitemap"/>.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    /// <returns>An instance of <see cref="XDocument"/>.</returns>
    public XDocument ToXmlDocument(ISitemapResult sitemap) {
        XElement root = ToXmlElement(sitemap);
        return new XDocument(new XDeclaration("1.0", "utf-8", null), root);
    }

    /// <summary>
    /// Returns an <see cref="XElement"/> representing the specified <paramref name="sitemap"/>.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    /// <returns>An instance of <see cref="XElement"/>.</returns>
    public XElement ToXmlElement(ISitemapResult sitemap) {

        XElement root;

        if (sitemap.Exception != null || sitemap.Items == null) {

            // Initialize a new <e> element as root
            root = new XElement(SitemapConstants.XNamespace + "e", "Error");

        } else {

            // Initialize a new <urlset> element as root
            root = new XElement(SitemapConstants.XNamespace + "urlset");

            // Add an <url> element for each item
            foreach (ISitemapItem item in sitemap.Items) root.Add(ToXmlElement(item));

        }

        return root;

    }

    /// <summary>
    /// Returns an <see cref="XElement"/> representing the specified sitemap <paramref name="item"/>.
    /// </summary>
    /// <param name="item">The sitemap item.</param>
    /// <returns>An instance of <see cref="XElement"/>.</returns>
    public XElement ToXmlElement(ISitemapItem item) {

        XElement xml = new(
            SitemapConstants.XNamespace + SitemapConstants.Xml.Location,
            new XElement(SitemapConstants.XNamespace + SitemapConstants.Xml.Url, item.Url),
            new XElement(SitemapConstants.XNamespace + SitemapConstants.Xml.LastModified, item.LastModified.ToString("yyyy-MM-dd"))
        );

        if (item.ChangeFrequency > 0) xml.Add(new XElement(SitemapConstants.XNamespace + SitemapConstants.Xml.ChangeFrequency, item.ChangeFrequency.ToLower()));
        if (item.PagePriority != null) xml.Add(new XElement(SitemapConstants.XNamespace + SitemapConstants.Xml.Priority, item.PagePriority.Value.ToString("N1", CultureInfo.InvariantCulture)));

        return xml;

    }

}