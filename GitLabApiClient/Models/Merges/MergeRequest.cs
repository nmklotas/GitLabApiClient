using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Merges
{
    public class MergeRequest : ModifiableObject
    {
        [JsonProperty("labels")]
        public List<string> Labels { get; } = new List<string>();

        [JsonProperty("source_branch")]
        public string SourceBranch { get; set; }

        [JsonProperty("downvotes")]
        public long Downvotes { get; set; }

        [JsonProperty("author")]
        public Assignee Author { get; set; }

        [JsonProperty("assignee")]
        public Assignee Assignee { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("force_remove_source_branch")]
        public bool ForceRemoveSourceBranch { get; set; }

        [JsonProperty("milestone")]
        public Milestone Milestone { get; set; }

        [JsonProperty("merge_status")]
        public string MergeStatus { get; set; }

        [JsonProperty("merge_commit_sha")]
        public object MergeCommitSha { get; set; }

        [JsonProperty("merge_when_pipeline_succeeds")]
        public bool MergeWhenPipelineSucceeds { get; set; }

        [JsonProperty("sha")]
        public string Sha { get; set; }

        [JsonProperty("project_id")]
        public long ProjectId { get; set; }

        [JsonProperty("should_remove_source_branch")]
        public bool ShouldRemoveSourceBranch { get; set; }

        [JsonProperty("target_project_id")]
        public long TargetProjectId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("source_project_id")]
        public long SourceProjectId { get; set; }

        [JsonProperty("target_branch")]
        public string TargetBranch { get; set; }

        [JsonProperty("upvotes")]
        public long Upvotes { get; set; }

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("user_notes_count")]
        public long UserNotesCount { get; set; }

        [JsonProperty("work_in_progress")]
        public bool WorkInProgress { get; set; }
    }
}
