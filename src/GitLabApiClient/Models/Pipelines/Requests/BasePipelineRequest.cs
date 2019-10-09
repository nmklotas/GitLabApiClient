using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Pipelines.Requests
{
    public abstract class BasePipelineRequest
    {
        protected BasePipelineRequest(string projectId, int pipelineId)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.IsTrue(pipelineId > 0, nameof(pipelineId));

            ProjectId = projectId;
            PipelineId = pipelineId;
        }

        /// <summary>
        ///     The ID or URL-encoded path of the project owned by the authenticated user
        /// </summary>
        [JsonProperty("id")]
        public string ProjectId { get; }

        /// <summary>
        ///     The ID of a pipeline
        /// </summary>
        [JsonProperty("pipeline_id")]
        public int PipelineId { get; }
    }
}
