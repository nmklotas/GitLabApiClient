using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Pipelines.Responses
{
    public class PipelineDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sha")]
        public string Sha { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("status")]
        public PipelineStatus Status { get; set; }

        [JsonProperty("web_url")]
        public Uri WebUrl { get; set; }

        [JsonProperty("before_sha")]
        public string BeforeSha { get; set; }

        [JsonProperty("tag")]
        public bool Tag { get; set; }

        [JsonProperty("yaml_errors")]
        public string YamlErrors { get; set; }

        [JsonProperty("user")]
        public PipelineUser User { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("started_at")]
        public DateTime? StartedAt { get; set; }

        [JsonProperty("finished_at")]
        public DateTime? FinishedAt { get; set; }

        [JsonProperty("committed_at")]
        public DateTime? CommittedAt { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("coverage")]
        public string Coverage { get; set; }
    }
}
