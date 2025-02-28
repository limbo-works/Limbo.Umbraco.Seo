using System;
using System.Net;

namespace Limbo.Umbraco.Seo.Robots.Models;

/// <summary>
/// Interface describing a robots result.
/// </summary>
public interface IRobotsResult {

    /// <summary>
    /// Gets the status code of the result.
    /// </summary>
    HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets whether the result was successful (typically the case if <see cref="StatusCode"/>¨is <see cref="HttpStatusCode.OK"/>).
    /// </summary>
    bool Success { get; }

    /// <summary>
    /// Gets an instance of <see cref="Exception"/> if building the robots value was unsuccessful.
    /// </summary>
    Exception? Exception { get; }

    /// <summary>
    /// Gets the value for the <c>robots.txt</c> file.
    /// </summary>
    string? Value { get; }

}