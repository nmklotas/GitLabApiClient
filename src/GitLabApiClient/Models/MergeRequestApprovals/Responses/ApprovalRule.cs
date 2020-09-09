using System.Collections.Generic;
using GitLabApiClient.Models.Groups.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequestApprovals.Responses
{
    public class ApprovalRule : MergeRequestApprovals.ApprovalRule
    {
        [JsonProperty("approvals_required")]
        public int ApprovalsRequired { get; set; }

        [JsonProperty("users")]
        public List<Assignee> Users { get; set; }

        [JsonProperty("groups")]
        public List<Group> Groups { get; set; }

        [JsonProperty("contains_hidden_groups")]
        public bool ContainsHiddenGroups { get; set; }
    }
}
