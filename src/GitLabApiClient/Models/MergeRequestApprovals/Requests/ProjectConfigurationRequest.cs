using GitLabApiClient.Internal.Paths;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequestApprovals.Requests
{
    public class ProjectConfigurationRequest
    {
        public ProjectConfigurationRequest(ProjectId projectId)
        {
            ProjectId = projectId;
        }

        [JsonProperty("id")]
        public ProjectId ProjectId { get; }

        [JsonProperty("reset_approvals_on_push")]
        public bool? ResetApprovalsOnPush { get; set; }

        [JsonProperty("disable_overriding_approvers_per_merge_request")]
        public bool? DisableOverridingApproversPerMergeRequest { get; set; }

        [JsonProperty("merge_requests_author_approval")]
        public bool? MergeRequestAuthorApproval { get; set; }

        [JsonProperty("merge_requests_disable_committers_approval")]
        public bool? MergeRequestDisableCommitersApproval { get; set; }

        [JsonProperty("require_password_to_approve")]
        public bool? RequirePasswordToApprove { get; set; }
    }
}
