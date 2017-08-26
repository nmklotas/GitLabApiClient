using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects
{
    public class Owner
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}