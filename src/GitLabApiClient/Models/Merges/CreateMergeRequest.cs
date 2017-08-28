using GitLabApiClient.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Merges
{
    public class CreateMergeRequest
    {
        public CreateMergeRequest(int projectId, string sourceBranch, string targetBranch, string title)
        {
            Guard.NotEmpty(sourceBranch,nameof(sourceBranch));
            Guard.NotEmpty(targetBranch, nameof(targetBranch));
            Guard.NotEmpty(title,nameof(title));
            SourceBranch = sourceBranch;
            TargetBranch = targetBranch;
            Title = title;
            ProjectId = projectId;
        }

        [JsonProperty("id")]
        public int ProjectId { get; }

        [JsonProperty("source_branch")]
        public string SourceBranch { get; }

        [JsonProperty("target_branch")]
        public string TargetBranch { get; }

        [JsonProperty("title")]
        public string Title { get; }

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
