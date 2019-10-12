using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Pipelines.Responses
{
    public class PipelineUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("avatar_url")]
        public Uri AvatarUrl { get; set; }

        [JsonProperty("web_url")]
        public Uri WebUrl { get; set; }
    }
}
