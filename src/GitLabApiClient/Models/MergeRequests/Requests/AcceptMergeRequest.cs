using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequests.Requests
{
    /// <summary>
    /// Used to accept merge requests.
    /// </summary>
    public sealed class AcceptMergeRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMergeRequest"/> class.
        /// </summary>
        /// <param name="projectId">The ID or URL-encoded path of the project owned by the authenticated user.</param>
        /// <param name="mergeRequestId">Internal ID of merge request.</param>
        public AcceptMergeRequest(string projectId, int mergeRequestId)
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
        /// Internal ID of merge request.
        /// </summary>
        [JsonProperty("merge_request_iid")]
        public int MergeRequestId { get; }

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
