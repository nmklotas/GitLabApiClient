using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequests.Requests
{
    /// <summary>
    /// Used to accept merge requests.
    /// </summary>
    public sealed class AcceptMergeRequest
    {
        /// <summary>
        /// Custom merge commit message.
        /// </summary>
        [JsonProperty("merge_commit_message")]
        public string MergeCommitMessage { get; set; }

        /// <summary>
        /// If set removes the source branch.
        /// </summary>
        [JsonProperty("should_remove_source_branch")]
        public bool? RemoveSourceBranch { get; set; }

        /// <summary>
        /// If set merge request is merged when the pipeline succeeds.
        /// </summary>
        [JsonProperty("merge_when_pipeline_succeeds")]
        public bool? MergeWhenPipelineSucceeds { get; set; }

        /// <summary>
        /// If set, then this SHA must match the HEAD of the source branch, otherwise the merge will fail.
        /// </summary>
        [JsonProperty("sha")]
        public string Sha { get; set; }
    }
}
