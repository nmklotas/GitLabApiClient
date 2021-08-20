using Newtonsoft.Json;

namespace GitLabApiClient.Models.Trees.Responses
{
    public sealed class Tree
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }
    }
}
