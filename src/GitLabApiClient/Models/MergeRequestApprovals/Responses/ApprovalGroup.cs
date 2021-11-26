using GitLabApiClient.Models.Groups.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequestApprovals.Responses
{
    public class ApprovalGroup
    {
        [JsonProperty("group")]
        public Group Group { get; set; }
    }
}
