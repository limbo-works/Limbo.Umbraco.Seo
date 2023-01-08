using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Limbo.Umbraco.Seo.Extensions;
using Limbo.Umbraco.Seo.Models.Sitemaps;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

// ReSharper disable ConvertIfStatementToReturnStatement

namespace Limbo.Umbraco.Seo.Sitemaps {

    /// <summary>
    /// Class representing the default implementation of the XML sitemap helper used by this package.
    /// </summary>
    public class SitemapHelper : ISitemapHelper {

        private readonly ILogger<SitemapHelper> _logger;
        private readonly IDomainService _domainService;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        /// <summary>
        /// Initializes a instanced based on the specified DI dependencies.
        /// </summary>
        /// <param name="logger">The current logger.</param>
        /// <param name="domainService">The current domain service.</param>
        /// <param name="umbracoContextAccessor">The current Umbraco context accessor.</param>
        public SitemapHelper(ILogger<SitemapHelper> logger, IDomainService domainService, IUmbracoContextAccessor umbracoContextAccessor) {
            _logger = logger;
            _domainService = domainService;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        /// <summary>
        /// Returns an instance of <see cref="ISitemapResult"/> for the specified HTTP <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>An instance of <see cref="ISitemapResult"/>.</returns>
        public virtual ISitemapResult BuildSitemap(HttpContext context) {

            // Determine the current URL/URL
            Uri url = GetUri(context.Request);

            // Get a list of all domains configured in Umbraco
            IReadOnlyList<IDomain> domains = _domainService.GetAll(false).ToArray();

            // Get the first matching domain (or null if no matches were found)
            IDomain domain = domains.FirstOrDefault(d => IsMatch(url, d));

            // Continue building the sitemap
            return BuildSitemap(new SitemapContext(context, url, domains, domain));

        }

        /// <summary>
        /// Returns  whether <paramref name="url"/> and <paramref name="domain"/>.
        /// </summary>
        /// <param name="url">The current URL.</param>
        /// <param name="domain">The domain to check.</param>
        /// <returns><see langword="true"/> if <paramref name="url"/> and <paramref name="domain"/> matches; otherwise, <see langword="false"/>.</returns>
        protected virtual bool IsMatch(Uri url, IDomain domain) {
            if (domain.DomainName.StartsWith($"{url.Scheme}://{url.Authority}")) return true;
            if (domain.DomainName.StartsWith($"{url.Scheme}://{url.Host}")) return true;
            if (domain.DomainName.Contains(url.Authority)) return true;
            if (domain.DomainName.Contains(url.Host)) return true;
            return false;
        }

        /// <summary>
        /// Attempts to find the root node of the specified sitemap <paramref name="context"/>. if a root node isn't
        /// found, an exception of type of <see cref="SitemapException"/> is thrown.
        /// </summary>
        /// <param name="context">A reference to the current sitemap context.</param>
        /// <returns>An instance </returns>
        protected virtual IPublishedContent FindRootNode(ISitemapContext context) {

            if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext umbracoContext)) {
                throw new SitemapException("Failed getting reference to the current Umbraco context.");
            }

            IPublishedContent rootNode;

            if (context.Domain is null) {

                // If one or more domains are configured in Umbraco, we should trigger an exception if a matching domain isn't found
                if (context.Domains.Count > 0) throw new SitemapException(HttpStatusCode.NotFound, "Unable to find domain.");

                // If no domains are configured in Umbraco, we fall back to the first root node in the content cache
                rootNode = umbracoContext.Content?.GetAtRoot().FirstOrDefault();
                if (rootNode is null) throw new SitemapException("Failed determining root node from request.");
                return rootNode;

            }

            // If the matched domain doesn't specify a valid root node ID, we trigger an exception as well
            if (context.Domain.RootContentId is null or <= 0) throw new SitemapException(HttpStatusCode.NotFound, "Domain does not specify a root ID.");

            // Loop up the root node from the domain
            rootNode = umbracoContext.Content.GetById(context.Domain.RootContentId.Value);
            return rootNode ?? throw new SitemapException("Failed determining root node from request.");

        }

        /// <summary>
        /// Returns an instance of <see cref="ISitemapResult"/> for the specified sitemap <paramref name="context"/>.
        /// </summary>
        /// <param name="context">A reference to the current sitemap context.</param>
        /// <returns>An instance of <see cref="ISitemapResult"/>.</returns>
        protected virtual ISitemapResult BuildSitemap(ISitemapContext context) {

            try {

                // Attemp to find the root node
                context.RootNode = FindRootNode(context);

                List<ISitemapItem> items = new();

                // Start recursively building the sitemap
                BuildSitemap(context, items, context.RootNode);

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

            try {

                // Run the same for all children
                foreach (IPublishedContent child in node.ChildrenForAllCultures) BuildSitemap(context, items, child);

            } catch (Exception ex) {

#if DEBUG
                _logger.LogError(ex, $"Failed generating sitemap for node with ID {node.Id}");
#endif

            }

        }

        /// <summary>
        /// Creates and returns a new <see cref="SitemapItem"/> instance based on the specified HTTP context <paramref name="context"/> and content node <paramref name="node"/>.
        /// </summary>
        /// <param name="context">A reference to the current sitemap context.</param>
        /// <param name="node">An instance of <see cref="IPublishedContent"/> representing the node.</param>
        /// <returns>An instance of <see cref="SitemapItem"/>.</returns>
        protected virtual SitemapItem CreateItem(ISitemapContext context, IPublishedContent node) {

            // Get the absolute of the node
            string absoluteUrl = node.Url(mode: UrlMode.Absolute);

            // Initialize a new item
            SitemapItem item = new() {
                Url = absoluteUrl,
                LastModified = node.UpdateDate, // TODO: Should this be UTC?
            };

            if (node.TryGetSitemapChangeFrequency(out SitemapChangeFrequency frequency)) {
                item.ChangeFrequency = frequency;
            }

            if (node.TryGetSitemapPriority(out float priority)) {
                item.PagePriority = priority;
            }

            return item;

        }

        private static Uri GetUri(HttpRequest request) {
            // TODO: Can we move this to one of our other packages?
            return new UriBuilder {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port ?? (request.Scheme == "https" ? 80 : 443),
                Path = request.Path,
                Query = request.QueryString.ToUriComponent()
            }.Uri;
        }

    }

}