using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Responses
{
    public sealed class ExportStatusLinks
    {
        [JsonProperty("api_url")]
        public string ApiUrl { get; set; }

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }
    }
}
