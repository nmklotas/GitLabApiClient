using Newtonsoft.Json;

namespace GitLabApiClient.Models.Releases.Responses
{
    public sealed class Source
    {
        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
