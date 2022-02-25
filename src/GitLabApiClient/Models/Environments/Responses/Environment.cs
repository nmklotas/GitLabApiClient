using System;
using GitLabApiClient.Models.Environments.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Environments.Responses
{
    public class Environment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("external_url")]
        public Uri ExternalUrl { get; set; }

        [JsonProperty("state")]
        public EnvironmentState State { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
