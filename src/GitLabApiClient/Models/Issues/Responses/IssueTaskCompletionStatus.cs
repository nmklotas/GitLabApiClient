using Newtonsoft.Json;

namespace GitLabApiClient.Models.Issues.Responses
{
    public class IssueTaskCompletionStatus
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("completed_count")]
        public int Completed { get; set; }
    }
}
