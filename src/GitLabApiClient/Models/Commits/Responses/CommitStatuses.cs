using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Commits.Responses
{
    public sealed class CommitStatuses
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("sha")]
        public string Sha { get; set; }
        [JsonProperty("ref")]
        public string Ref { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("started_at")]
        public DateTime StartedAt { get; set; }
        [JsonProperty("finished_at")]
        public DateTime FinishedAt { get; set; }
        [JsonProperty("allow_failure")]
        public bool AllowFailure { get; set; }
        [JsonProperty("coverage")]
        public float Coverage { get; set; }
        [JsonProperty("author")]
        public Author Author { get; set; }

    }
}
