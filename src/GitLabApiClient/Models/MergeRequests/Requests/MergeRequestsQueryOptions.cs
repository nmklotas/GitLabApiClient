using System;
using System.Collections.Generic;

namespace GitLabApiClient.Models.MergeRequests.Requests
{
    /// <summary>
    /// Options for merge requests listing
    /// </summary>
    public class MergeRequestsQueryOptions
    {
        internal MergeRequestsQueryOptions() { }

        /// <summary>
        /// Return all merge requests or just those that are opened, closed, or merged.
        /// </summary>
        public QueryMergeRequestState State { get; set; }

        /// <summary>
        /// Return requests ordered by CreatedAt or UpdatedAt fields. 
        /// Default is CreatedAt.
        /// </summary>
        public MergeRequestsOrder Order { get; set; }

        /// <summary>
        /// Return requests sorted in ascending or descending order. Default is descending.
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// The milestone title.
        /// </summary>
        public string MilestoneTitle { get; set; }

        /// <summary>
        /// If set, returns the iid, URL, title, description, and basic state of merge request.
        /// </summary>
        public bool Simple { get; set; }

        /// <summary>
        /// Return merge requests matching a list of labels.
        /// </summary>
        public IList<string> Labels { get; set; } = new List<string>();

        /// <summary>
        /// Return issues created after the given time (inclusive).
        /// </summary>
        public DateTime? CreatedAfter { get; set; }

        /// <summary>
        /// Return issues created before the given time (inclusive).
        /// </summary>
        public DateTime? CreatedBefore { get; set; }

        /// <summary>
        /// Return merge requests for the given scope.
        /// Defaults to merge requests created by anyone.
        /// </summary>
        public Scope Scope { get; set; }

        /// <summary>
        /// Return merge requests created by the given user id.
        /// </summary>
        public int? AuthorId { get; set; }

        /// <summary>
        /// Returns merge requests assigned to the given user id.
        /// </summary>
        public int? AssigneeId { get; set; }
    }
}
