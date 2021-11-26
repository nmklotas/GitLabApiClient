using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models
{
    public sealed class Assignee
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }
    }
}
