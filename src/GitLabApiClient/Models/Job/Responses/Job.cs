using System;
using GitLabApiClient.Models.Commits.Responses;
using GitLabApiClient.Models.Pipelines.Responses;
using GitLabApiClient.Models.Runners.Responses;
using GitLabApiClient.Models.Users.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Job.Responses
{
    public sealed class Job
    {
        [JsonProperty("allow_failure")]
        public bool AllowFailure { get; set; }

        [JsonProperty("artifacts_expire_at")]
        public DateTime ArtifactsExpireAt { get; set; }

        [JsonProperty("commit")]
        public Commit Commit { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("finished_at")]
        public DateTime? FinishedAt { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pipeline")]
        public Pipeline Pipeline { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("runner")]
        public Runner Runner { get; set; }

        [JsonProperty("stage")]
        public string Stage { get; set; }

        [JsonProperty("started_at")]
        public DateTime? StartedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }
    }
}
