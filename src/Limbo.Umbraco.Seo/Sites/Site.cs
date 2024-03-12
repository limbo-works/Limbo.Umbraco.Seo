using System;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Limbo.Umbraco.Seo.Sites;

/// <summary>
/// Class representing describing a site.
/// </summary>
public class Site : ISite {

    /// <summary>
    /// Gets the numeric ID of the site.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the GUID key of the site.
    /// </summary>
    public Guid Key { get; }

    /// <summary>
    /// Gets the name of the site.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the content type of the site node.
    /// </summary>
    public string ContentTypeAlias { get; }

    /// <summary>
    /// Gets a reference to the <see cref="IPublishedContent"/> representing the site node.
    /// </summary>
    public IPublishedContent Content { get; }

    /// <summary>
    /// Gets a reference to the <see cref="IPublishedContent"/> representing the site node.
    /// </summary>
    /// <param name="content">An instance of <see cref="IPublishedContent"/> representing the site node.</param>
    public Site(IPublishedContent content) {
        Id = content.Id;
        Key = content.Key;
        Name = content.Name!;
        ContentTypeAlias = content.ContentType.Alias;
        Content = content;
    }

}