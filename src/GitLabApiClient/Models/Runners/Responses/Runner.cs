using Newtonsoft.Json;

namespace GitLabApiClient.Models.Runners.Responses
{
    public sealed class Runner
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("is_shared")]
        public bool IsShared { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddresses { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
