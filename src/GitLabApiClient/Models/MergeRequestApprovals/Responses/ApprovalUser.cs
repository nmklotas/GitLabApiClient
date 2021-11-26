using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequestApprovals.Responses
{
    public class ApprovalUser
    {
        [JsonProperty("user")]
        public Assignee User { get; set; }
    }
}
