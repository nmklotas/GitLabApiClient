using Newtonsoft.Json;

namespace GitLabApiClient.Models.Releases.Responses
{
    public sealed class Asset
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("sources")]
        public Source[] Sources { get; set; }

        [JsonProperty("links")]
        public Link[] Links { get; set; }
    }
}
