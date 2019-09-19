using System;
using GitLabApiClient.Internal.Utilities;

namespace GitLabApiClient.Models.Issues.Requests
{
    /// <inheritdoc />
    /// <summary>
    /// Options for project issues listing
    /// </summary>
    public sealed class ProjectIssuesQueryOptions : IssuesQueryOptions
    {
        /// <summary>
        /// Initializes a new instance of ProjectIssuesQueryOptions
        /// </summary>
        internal ProjectIssuesQueryOptions()
        {
        }

        /// <summary>
        /// Return issues created after the given time (inclusive)
        /// </summary>
        public DateTime? CreatedAfter { get; set; }

        /// <summary>
        /// Return issues created before the given time (inclusive)
        /// </summary>
        public DateTime? CreatedBefore { get; set; }
    }
}
