using System;
using System.Net;

namespace Limbo.Umbraco.Seo.Robots;

/// <summary>
/// Class describing a robots result.
/// </summary>
public class RobotsResult : IRobotsResult {

    /// <summary>
    /// Gets the status code of the result.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets whether the result was successful (typically the case if <see cref="StatusCode"/>¨is <see cref="HttpStatusCode.OK"/>).
    /// </summary>
    public bool Success => StatusCode == HttpStatusCode.OK;

    /// <summary>
    /// Gets an instance of <see cref="Exception"/> if building the robots value was unsuccessful.
    /// </summary>
    public Exception? Exception { get; }

    /// <summary>
    /// Gets the value for the <c>robots.txt</c> file.
    /// </summary>
    public string? Value { get; }

    /// <summary>
    /// Initializes a new result with the specified <paramref name="robots"/> value. Since a value is specified, the
    /// status is assumed to be <see cref="HttpStatusCode.OK"/>.
    /// </summary>
    /// <param name="robots">The <c>robots.txt</c> value.</param>
    public RobotsResult(string robots) {
        StatusCode = HttpStatusCode.OK;
        Value = robots;
    }

    /// <summary>
    /// Initializes a new result based on the specified <paramref name="statusCode"/>. This method should generally be
    /// used to represent failed results where a <c>robots.txt</c> value couldn't be determined.
    /// </summary>
    /// <param name="statusCode">The status code.</param>
    public RobotsResult(HttpStatusCode statusCode) {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Initializes a new result based on the specified <paramref name="statusCode"/> and <paramref name="exception"/>.
    /// This method should generally be used to represent failed results where a <c>robots.txt</c> value couldn't be
    /// determined.
    /// </summary>
    /// <param name="statusCode">The status code.</param>
    /// <param name="exception">The exception, if any.</param>
    public RobotsResult(HttpStatusCode statusCode, Exception exception) {
        StatusCode = statusCode;
        Exception = exception;
    }

}