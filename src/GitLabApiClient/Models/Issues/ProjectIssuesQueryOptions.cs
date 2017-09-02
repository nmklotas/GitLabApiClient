using System;
using GitLabApiClient.Utilities;

namespace GitLabApiClient.Models.Issues
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
        /// <param name="projectId">The ID or URL-encoded path of the project owned by the authenticated user</param>
        internal ProjectIssuesQueryOptions(string projectId)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            ProjectId = projectId;
        }

        /// <summary>
        /// The ID or URL-encoded path of the project owned by the authenticated user
        /// </summary>
        public string ProjectId { get; }

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
