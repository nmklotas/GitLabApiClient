using System;

namespace GitLabApiClient.Models.Projects.Requests
{
    /// <summary>
    /// Options for projects listing
    /// </summary>
    public sealed class ProjectQueryOptions
    {
        internal ProjectQueryOptions() { }

        /// <summary>
        /// The ID or username of the user
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Limit by archived status
        /// </summary>
        public bool Archived { get; set; }

        /// <summary>
        /// Limit by visibility. Default is Public.
        /// </summary>
        public QueryProjectVisibilityLevel Visibility { get; set; }

        /// <summary>
        /// Specifies project order. Default is Creation time.
        /// </summary>
        public ProjectsOrder Order { get; set; }

        /// <summary>
        /// Specifies project sort order. Default is desending.
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// Return list of projects matching the search criteria
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Return only the ID, URL, name, and path of each project
        /// </summary>
        public bool Simple { get; set; }

        /// <summary>
        /// Limit by projects owned by the current user
        /// </summary>
        public bool Owned { get; set; }

        /// <summary>
        /// Limit by projects that the current user is a member of
        /// </summary>
        public bool IsMemberOf { get; set; }

        /// <summary>
        /// Limit by projects starred by the current user
        /// </summary>
        public bool Starred { get; set; }

        /// <summary>
        /// Include project statistics
        /// </summary>
        public bool IncludeStatistics { get; set; }

        /// <summary>
        /// Limit by enabled issues feature
        /// </summary>
        public bool WithIssuesEnabled { get; set; }

        /// <summary>
        /// Limit by enabled merge requests feature
        /// </summary>
        public bool WithMergeRequestsEnabled { get; set; }

        /// <summary>
        /// Limit by Last Activity After specified datetime
        /// Note: You would need GitLab 12.10 or later
        /// </summary>
        public DateTime LastActivityAfter { get; set; }

        /// <summary>
        /// Limit by Last Activity Before specified datetime
        /// Note: You would need GitLab 12.10 or later
        /// </summary>
        public DateTime LastActivityBefore { get; set; }
    }
}
