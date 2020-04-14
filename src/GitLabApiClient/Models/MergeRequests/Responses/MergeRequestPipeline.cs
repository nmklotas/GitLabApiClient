using System;
using GitLabApiClient.Models.Pipelines;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequests.Responses
{
    public class MergeRequestPipeline
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("status")]
        public PipelineStatus Status { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("sha")]
        public string Sha { get; set; }

        [JsonProperty("web_url")]
        public Uri WebUrl { get; set; }
    }
}
