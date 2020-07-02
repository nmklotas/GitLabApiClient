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

        [JsonProperty("open_issues_count")]
        public int OpenIssuesCount { get; set; }

        [JsonProperty("closed_issues_count")]
        public int ClosedIssuesCount { get; set; }

        [JsonProperty("open_merge_requests_count")]
        public int OpenMergeRequestsCount { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }
    }
}
