using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Responses
{
    public sealed class Links
    {
        [JsonProperty("members")]
        public string Members { get; set; }

        [JsonProperty("issues")]
        public string Issues { get; set; }

        [JsonProperty("events")]
        public string Events { get; set; }

        [JsonProperty("labels")]
        public string Labels { get; set; }

        [JsonProperty("repo_branches")]
        public string RepoBranches { get; set; }

        [JsonProperty("merge_requests")]
        public string MergeRequests { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }
}
