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

#pragma warning disable 168

namespace Limbo.Umbraco.Seo.Sitemaps {

    public class SitemapHelper : ISitemapHelper {

        private readonly ILogger<SitemapHelper> _logger;
        private readonly IDomainService _domainService;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public SitemapHelper(ILogger<SitemapHelper> logger, IDomainService domainService, IUmbracoContextAccessor umbracoContextAccessor) {
            _logger = logger;
            _domainService = domainService;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public virtual ISitemapResult BuildSitemap(HttpContext context) {

            Uri url = GetUri(context.Request);

            IDomain domain = _domainService
                .GetAll(false)
                .FirstOrDefault(d => d.DomainName.StartsWith($"{url.Scheme}://{url.Authority}") ||
                                     d.DomainName.StartsWith($"{url.Scheme}://{url.Host}") ||
                                     d.DomainName.Contains(url.Authority) ||
                                     d.DomainName.Contains(url.Host));

            return BuildSitemap(context, domain);

        }

        protected virtual ISitemapResult BuildSitemap(HttpContext context, IDomain domain) {

            try {

                if (domain == null) throw new SitemapException(HttpStatusCode.NotFound, "Unable to find domain.");
                if (domain.RootContentId == null || domain.RootContentId <= 0) throw new SitemapException(HttpStatusCode.NotFound, "Domain does not specify a root ID.");

                List<ISitemapItem> items = new List<ISitemapItem>();

                IPublishedContent root = _umbracoContextAccessor.GetRequiredUmbracoContext().Content.GetById(domain.RootContentId.Value);

                BuildSitemap(context, items, root);

                return new SitemapResult(items);

            } catch (Exception ex) {

                return new SitemapResult(ex);

            }

        }

        protected virtual bool IgnoreNode(IPublishedContent node) {

            // Skip formRender
            if (node.ContentType.Alias == "formRender") return true;

            if (node.TemplateId <= 0) return true;
            if (node.Value<bool>(SitemapConstants.Properties.HideFromSitemap)) return true;

            return false;

        }

        protected virtual bool IgnoreChildren(IPublishedContent node) {

            // Skip formRender
            if (node.ContentType.Alias == "formRender") return true;

            return false;

        }

        protected virtual void BuildSitemap(HttpContext context, List<ISitemapItem> items, IPublishedContent node) {

            if (!IgnoreNode(node)) {

                items.Add(CreateItem(context, node));

            }

            if (IgnoreChildren(node)) return;

            try {

                // Run the same for all children
                foreach (IPublishedContent child in node.ChildrenForAllCultures) BuildSitemap(context, items, child);

            } catch (Exception ex) {

#if DEBUG
                _logger.LogError(ex, $"Failed generating sitemap for node with ID {node.Id}");
#endif

            }

        }

        protected virtual SitemapItem CreateItem(HttpContext context, IPublishedContent node) {

            Uri uri = GetUri(context.Request);

            string absoluteUrl = uri.Scheme + Uri.SchemeDelimiter + uri.Host + node.Url();

            // TODO: Isn't this mroe correct?
            absoluteUrl = node.Url(mode: UrlMode.Absolute);


            SitemapItem item = new SitemapItem {
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