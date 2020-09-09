using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequestApprovals.Responses
{
    public class ApprovalState
    {
        public class ApprovalRule : Responses.ApprovalRule
        {
            [JsonProperty("eligible_approvers")]
            public IList<Assignee> EligibleApprovers { get; set; }
            [JsonProperty("approved_by")]
            public IList<Assignee> ApprovedBy { get; set; }
            [JsonProperty("overridden")]
            public bool Overridden { get; set; }
            [JsonProperty("code_owner")]
            public bool CodeOwner { get; set; }
            [JsonProperty("approved")]
            public bool Approved { get; set; }
        }

        [JsonProperty("approval_rules_overwritten")]
        public bool ApprovalRulesOverwritten { get; set; }

        [JsonProperty("rules")]
        public IList<ApprovalRule> Rules { get; set; }
    }
}
