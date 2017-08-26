using Newtonsoft.Json;

namespace GitLabApiClient.Models.Merges
{
    public class CreateMergeRequest
    {
        [JsonProperty("id")]
        public int ProjectId { get; set; }

        [JsonProperty("source_branch")]
        public string SourceBranch { get; set; }

        [JsonProperty("target_branch")]
        public string TargetBranch { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("labels")]
        public string Labels { get; set; }

        [JsonProperty("milestone_id")]
        public int? MilestoneId { get; set; }

        [JsonProperty("assignee_id")]
        public int? AssigneeId { get; set; }

        [JsonProperty("target_project_id")]
        public int? TargetProjectId { get; set; }

        [JsonProperty("remove_source_branch")]
        public bool RemoveSourceBranch { get; set; }
    }
}
