using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Branches.Responses;

public sealed class ApprovalRules
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("approvals_required")]
    public int ApprovalsRequired { get; set; }

    [JsonProperty("protected_branches")]
    public IList<ProtectedBranch> ProtectedBranches { get; set; }

    [JsonProperty("applies_to_all_protected_branches")]
    public bool AppliesToAllProtectedBranches { get; set; }

}
