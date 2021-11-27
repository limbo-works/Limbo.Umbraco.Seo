using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Limbo.Umbraco.Seo.Extensions;
using Limbo.Umbraco.Seo.Models.Sitemaps;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Limbo.Umbraco.Seo.Sitemaps {
    
    public class SitemapHelper : ISitemapHelper {
        
        private readonly ILogger _logger;
        private readonly IDomainService _domainService;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public SitemapHelper(ILogger logger, IDomainService domainService, IUmbracoContextAccessor umbracoContextAccessor) {
            _logger = logger;
            _domainService = domainService;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public virtual ISitemapResult BuildSitemap(HttpContextBase context) {

            Uri url = context.Request.Url;

            IDomain domain = _domainService
                .GetAll(false)
                .FirstOrDefault(d => d.DomainName.StartsWith($"{url.Scheme}://{url.Authority}") ||
                                     d.DomainName.StartsWith($"{url.Scheme}://{url.Host}") ||
                                     d.DomainName.Contains(url.Authority) ||
                                     d.DomainName.Contains(url.Host));

            return BuildSitemap(context, domain);

        }

        protected virtual ISitemapResult BuildSitemap(HttpContextBase context, IDomain domain) {

            try {

                if (domain == null) throw new SitemapException(HttpStatusCode.NotFound, "Unable to find domain.");
                if (domain.RootContentId == null || domain.RootContentId <= 0) throw new SitemapException(HttpStatusCode.NotFound, "Domain does not specify a root ID.");

                List<ISitemapItem> items = new List<ISitemapItem>();

                IPublishedContent root = _umbracoContextAccessor.UmbracoContext.Content.GetById(domain.RootContentId.Value);

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

        protected virtual void BuildSitemap(HttpContextBase context, List<ISitemapItem> items, IPublishedContent node) {

            if (!IgnoreNode(node)) {

                items.Add(CreateItem(context, node));

            }

            if (IgnoreChildren(node)) return;

            try {

                // Run the same for all children
                foreach (IPublishedContent child in node.ChildrenForAllCultures) BuildSitemap(context, items, child);

            } catch (Exception ex) {

#if DEBUG
                _logger.Error<SitemapHelper>(ex, $"Failed generating sitemap for node with ID {node.Id}");
#endif

            }

        }

        protected virtual SitemapItem CreateItem(HttpContextBase context, IPublishedContent node) {

            Uri uri = context.Request.Url;
            
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

    }

}