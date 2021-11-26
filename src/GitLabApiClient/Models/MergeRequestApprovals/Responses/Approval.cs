using System.Collections.Generic;
using GitLabApiClient.Models.MergeRequests.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequestApprovals.Responses
{
    public class Approval : ModifiableObject
    {
        [JsonProperty("project_id")]
        public int ProjectId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("state")]
        public MergeRequestState State { get; set; }

        [JsonProperty("merge_status")]
        public MergeStatus MergeStatus { get; set; }

        [JsonProperty("approved")]
        public bool Approved { get; set; }

        [JsonProperty("user_can_approve")]
        public bool UserCanApprove { get; set; }

        [JsonProperty("user_has_approved")]
        public bool UserHasApproved { get; set; }

        [JsonProperty("approval_rules_left")]
        public IList<MergeRequestApprovals.ApprovalRule> ApprovalRulesLeft { get; set; }

        [JsonProperty("merge_request_approvers_available")]
        public bool MergeRequestApproversAvailable { get; set; }

        [JsonProperty("multiple_approval_rules_available")]
        public bool MultipleApprovalRulesAvailable { get; set; }

        [JsonProperty("approved_by")]
        public IList<ApprovalUser> ApprovedBy { get; set; }

        [JsonProperty("suggested_approvers")]
        public IList<ApprovalUser> SuggestedApprovers { get; set; }

        [JsonProperty("approvals_required")]
        public int ApprovalsRequired { get; set; }

        [JsonProperty("approvals_left")]
        public int ApprovalsLeft { get; set; }
    }
}
