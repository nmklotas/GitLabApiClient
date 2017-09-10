using System.Collections.Generic;
using GitLabApiClient.Http.Serialization;
using GitLabApiClient.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.MergeRequests.Requests
{
    /// <summary>
    /// Used to update merge request.
    /// </summary>
    public sealed class UpdateMergeRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMergeRequest"/> class.
        /// </summary>
        /// <param name="projectId">The ID or URL-encoded path of the project owned by the authenticated user.</param>
        /// <param name="mergeRequestId">The ID of the merge request.</param>
        public UpdateMergeRequest(string projectId, int mergeRequestId)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            ProjectId = projectId;
            MergeRequestId = mergeRequestId;
        }

        /// <summary>
        /// The ID or URL-encoded path of the project owned by the authenticated user.
        /// </summary>
        [JsonProperty("id")]
        public string ProjectId { get; }

        /// <summary>
        /// The ID of the merge request.
        /// </summary>
        [JsonProperty("merge_request_iid")]
        public int MergeRequestId { get; }

        /// <summary>
        /// The target branch.
        /// </summary>
        [JsonProperty("target_branch")]
        public string TargetBranch { get; set; }

        /// <summary>
        /// Title of merge request.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Description of merge request.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Assignee user ID.
        /// </summary>
        [JsonProperty("assignee_id")]
        public int? AssigneeId { get; set; }

        /// <summary>
        /// The ID of a milestone.
        /// </summary>
        [JsonProperty("milestone_id")]
        public int? MilestoneId { get; set; }

        /// <summary>
        /// Labels names for merge request.
        /// </summary>
        [JsonProperty("labels")]
        [JsonConverter(typeof(CollectionToCommaSeparatedValuesConverter))]
        public IList<string> Labels { get; set; } = new List<string>();

        [JsonProperty("state_event")]
        public RequestedMergeRequestState? State { get; set; }

        /// <summary>
        /// Flag indicating if a merge request should remove the source branch when merging.
        /// </summary>
        [JsonProperty("remove_source_branch")]
        public bool? RemoveSourceBranch { get; set; }
    }
}
