using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Commits.Responses
{
    public sealed class Author
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }
    }
}
