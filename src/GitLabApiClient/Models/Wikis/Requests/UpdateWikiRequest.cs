using Newtonsoft.Json;

namespace GitLabApiClient.Models.Wikis.Requests
{
    public sealed class UpdateWikiRequest
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
