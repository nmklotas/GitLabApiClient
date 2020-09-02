using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Runners.Responses
{
    public sealed class RunnerDetails
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("architecture")]
        public string Architecture { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddresses { get; set; }

        [JsonProperty("is_shared")]
        public bool IsShared { get; set; }

        [JsonProperty("contacted_at")]
        public string ContactedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("projects")]
        public List<RunnerProject> Projects { get; } = new List<RunnerProject>();

        [JsonProperty("revision")]
        public string Revision { get; set; }

        [JsonProperty("tag_list")]
        public List<string> TagList { get; } = new List<string>();

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("access_level")]
        public string AccessLevel { get; set; }

        [JsonProperty("maximum_timeout")]
        public int MaximumTimeout { get; set; }
    }

}
