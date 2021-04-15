using Newtonsoft.Json;

namespace GitLabApiClient.Models.Markdown.Response
{
    public sealed class Markdown
    {
        [JsonProperty("html")]
        public string Html { get; set; }
    }
}
