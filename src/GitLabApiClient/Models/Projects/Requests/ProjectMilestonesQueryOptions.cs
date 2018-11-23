using System.Collections.Generic;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient.Models.Projects.Requests
{
    /// <summary>
    /// Options for project milestone listing
    /// </summary>
    public sealed class ProjectMilestonesQueryOptions
    {
        internal ProjectMilestonesQueryOptions() { }

        /// <summary>
        /// Return only the milestones having the given iid.
        /// </summary>
        public IList<int> MilestoneIds { get; set; } = new List<int>();
        
        /// <summary>
        /// Return only active or closed milestones.
        /// </summary>
        public MilestoneState State { get; set; }

        /// <summary>
        /// Return only milestones with a title or description matching the provided string.
        /// </summary>
        public string Search { get; set; }
    }
}