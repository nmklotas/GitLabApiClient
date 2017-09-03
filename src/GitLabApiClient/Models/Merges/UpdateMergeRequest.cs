using Newtonsoft.Json;

namespace GitLabApiClient.Models.Merges
{
    public sealed class UpdateMergeRequest
    {
        public UpdateMergeRequest(int projectId, int mergeRequestId)
        {
            ProjectId = projectId;
            MergeRequestId = mergeRequestId;
        }

        [JsonProperty("id")]
        public int ProjectId { get; }

        [JsonProperty("merge_request_iid")]
        public int MergeRequestId { get; }

        [JsonProperty("target_branch")]
        public string TargetBranch { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("assignee_id")]
        public int? AssigneeId { get; set; }

        [JsonProperty("milestone_id")]
        public int? MilestoneId { get; set; }

        [JsonProperty("labels")]
        public string Labels { get; set; }

        [JsonProperty("state_event")]
        public string State { get; set; }

        [JsonProperty("remove_source_branch")]
        public bool RemoveSourceBranch { get; set; }
    }
}
