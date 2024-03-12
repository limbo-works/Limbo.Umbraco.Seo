using System;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Limbo.Umbraco.Seo.Sites;

/// <summary>
/// Interface describing a site.
/// </summary>
public interface ISite {

    /// <summary>
    /// Gets the numeric ID of the site.
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Gets the GUID key of the site.
    /// </summary>
    Guid Key { get; }

    /// <summary>
    /// Gets the name of the site.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the content type of the site node.
    /// </summary>
    string ContentTypeAlias { get; }

    /// <summary>
    /// Gets a reference to the <see cref="IPublishedContent"/> representing the site node.
    /// </summary>
    IPublishedContent Content { get; }

}