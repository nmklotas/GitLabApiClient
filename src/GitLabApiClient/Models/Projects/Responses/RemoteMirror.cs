using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Responses
{
    public class RemoteMirror
    {
        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enabled { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }

        [JsonProperty("auth_method", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthMethod { get; set; }

        [JsonProperty("last_error", NullValueHandling = NullValueHandling.Ignore)]
        public object LastError { get; set; }

        [JsonProperty("last_successful_update_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastSuccessfulUpdateAt { get; set; }

        [JsonProperty("last_update_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdateAt { get; set; }

        [JsonProperty("last_update_started_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdateStartedAt { get; set; }

        [JsonProperty("only_protected_branches", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OnlyProtectedBranches { get; set; }

        [JsonProperty("keep_divergent_refs", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KeepDivergentRefs { get; set; }

        [JsonProperty("update_status", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdateStatus { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
    }
}
