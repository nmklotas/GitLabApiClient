using System.Collections.Generic;
using GitLabApiClient.Internal.Paths;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequestApprovals.Requests
{
    public abstract class CreateApprovalRulesRequest
    {
        protected CreateApprovalRulesRequest(ProjectId projectId)
        {
            ProjectId = projectId;
        }

        public ProjectId ProjectId { get; }

        [JsonProperty("rule_type")]
        public abstract string RuleType { get; }

        [JsonProperty("approvals_required")]
        public int? ApprovalsRequired { get; set; }
    }

    public class CreateRegularApprovalRulesRequest : CreateApprovalRulesRequest
    {
        public CreateRegularApprovalRulesRequest(ProjectId projectId) : base(projectId)
        {
        }
        public override string RuleType => "regular";

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("user_ids")]
        public List<int> UserIds { get; set; }

        [JsonProperty("group_ids")]
        public List<int> GroupIds { get; set; }

        [JsonProperty("protected_branch_ids")]
        public List<int> ProtectedBranchIds { get; set; }

    }

    internal class CreateAnyUserApprovalRulesRequest : CreateApprovalRulesRequest
    {
        public CreateAnyUserApprovalRulesRequest(ProjectId projectId, int count) : base(projectId)
        {
            ApprovalsRequired = count;
        }

        public override string RuleType => "any_approver";

        [JsonProperty("name")]
        public string Name => "All Members";
    }
}
