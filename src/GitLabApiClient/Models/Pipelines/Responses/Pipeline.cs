using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Pipelines.Responses
{
    public class Pipeline
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

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
