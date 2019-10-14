using Newtonsoft.Json;

namespace GitLabApiClient.Models.Commits.Responses
{
    public class CommitRef
    {
        [JsonProperty("type")]
        public CommitRefType Type { get; set; } = CommitRefType.All;

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
