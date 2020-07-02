using System;

namespace GitLabApiClient.Models.Issues.Requests
{
    /// <inheritdoc />
    /// <summary>
    /// Options for project issues listing
    /// </summary>
    [Obsolete("Use IssuesQueryOptions instead")]
    public sealed class ProjectIssuesQueryOptions : IssuesQueryOptions
    {
        /// <summary>
        /// Initializes a new instance of ProjectIssuesQueryOptions
        /// </summary>
        internal ProjectIssuesQueryOptions()
        {
        }
    }
}
