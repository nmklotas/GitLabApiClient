using System;
using System.Net;

namespace GitLabApiClient
{
    /// <inheritdoc />
    /// <summary>
    /// Exception thrown when GitLab returns non success status code.
    /// </summary>
    public sealed class GitLabException : Exception
    {
        public GitLabException(HttpStatusCode statusCode)
            => HttpStatusCode = statusCode;

        public GitLabException(HttpStatusCode statusCode, string message) : base(message)
            => HttpStatusCode = statusCode;

        public GitLabException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
            => HttpStatusCode = statusCode;

        /// <summary>
        /// Http status code of GitLab response.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }
    }
}
