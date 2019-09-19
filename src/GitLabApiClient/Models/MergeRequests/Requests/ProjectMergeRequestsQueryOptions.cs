using System.Collections.Generic;

namespace GitLabApiClient.Models.MergeRequests.Requests
{
    /// <inheritdoc />
    /// <summary>
    /// Options for project merge requests listing.
    /// </summary>
    public sealed class ProjectMergeRequestsQueryOptions : MergeRequestsQueryOptions
    {
        /// <summary>
        /// Initializes a new instance of ProjectMergeRequestsQueryOptions.
        /// </summary>
        /// <param name="projectId">The ID of a project.</param>
        internal ProjectMergeRequestsQueryOptions()
        {
        }

        /// <summary>
        /// Return the request having the given ids.
        /// </summary>
        public IList<int> MergeRequestsIds { get; set; } = new List<int>();
    }
}
