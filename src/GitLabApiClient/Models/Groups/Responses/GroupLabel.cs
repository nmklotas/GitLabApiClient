using Newtonsoft.Json;

namespace GitLabApiClient.Models.Groups.Responses
{
    public sealed class GroupLabel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
