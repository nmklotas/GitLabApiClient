using GitLabApiClient.Internal.Paths;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequestApprovals.Requests
{
    public class UpdateRegularApprovalRulesRequest : CreateRegularApprovalRulesRequest
    {
        [JsonProperty("id")]
        public int? Id { get; }

        public UpdateRegularApprovalRulesRequest(ProjectId projectId, int ruleId) : base(projectId)
        {
            Id = ruleId;
        }
    }
}
