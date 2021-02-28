using Newtonsoft.Json;

namespace GitLabApiClient.Models.Commits.Responses
{
    public class CommitStats
    {
        [JsonProperty("additions")]
        public int Additions { get; set; }
        [JsonProperty("deletions")]
        public int Deletions { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
