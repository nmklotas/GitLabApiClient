using System.Collections.Generic;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Pipelines.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Pipelines.Requests
{
    public class CreatePipelineRequest
    {
        public CreatePipelineRequest(string projectId, string reference, IList<PipelineVariable> variables = null)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.NotEmpty(reference, nameof(reference));

            ProjectId = projectId;
            Reference = reference;

            Variables = variables ?? new List<PipelineVariable>();
        }

        /// <summary>
        ///     The ID or URL-encoded path of the project owned by the authenticated user
        /// </summary>
        [JsonProperty("id")]
        public string ProjectId { get; set; }

        /// <summary>
        ///     Reference to commit
        /// </summary>
        [JsonProperty("ref")]
        public string Reference { get; set; }

        /// <summary>
        ///     An array containing the variables available in the pipeline, matching the structure [{ 'key' => 'UPLOAD_TO_S3',
        ///     'variable_type' => 'file', 'value' => 'true' }]
        /// </summary>
        [JsonProperty("variables")]
        public IList<PipelineVariable> Variables { get; set; }
    }
}
