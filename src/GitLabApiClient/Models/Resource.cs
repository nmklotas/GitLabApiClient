using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models
{
    public class Resource
    {
        internal Resource() { }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("resource_type")]
        public string ResourceType { get; set; }

        [JsonProperty("resource_id")]
        public string ResourceId { get; set; }
    }
}
