using Newtonsoft.Json;

namespace GitLabApiClient.Models.Merges
{
    public class EditMergeRequest
    {
        [JsonProperty("id")]
        public long ProjectId { get; set; }

        [JsonProperty("merge_request_iid")]
        public long MergeRequestId { get; set; }

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
