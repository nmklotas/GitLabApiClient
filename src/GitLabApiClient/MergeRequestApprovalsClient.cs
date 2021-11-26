using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.MergeRequestApprovals.Requests;
using GitLabApiClient.Models.MergeRequestApprovals.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to approve and unapprove merge requests.
    /// Every API call to merge requests must be authenticated.
    /// <exception cref="GitLabException">Thrown if request to GitLab API does not indicate success</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class MergeRequestApprovalsClient : IMergeRequestApprovalsClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal MergeRequestApprovalsClient(
            GitLabHttpFacade httpFacade
        )
        {
            _httpFacade = httpFacade;
        }

        public async Task ApproveAsync(ProjectId projectId, int mergeRequestIid, string sha = null)
        {
            string query = $"projects/{projectId}/merge_requests/{mergeRequestIid}/approve";
            if (sha != null)
            {
                query += $"?sha={sha}";
            }

            await _httpFacade.Post(query);
        }

        public async Task UnapproveAsync(ProjectId projectId, int mergeRequestIid)
        {
            string query = $"projects/{projectId}/merge_requests/{mergeRequestIid}/unapprove";
            await _httpFacade.Post(query);
        }

        public async Task<Approval> GetAsync(ProjectId projectId, int mergeRequestIid) =>
            await _httpFacade.Get<Approval>($"projects/{projectId}/merge_requests/{mergeRequestIid}/approvals");

        public async Task<ProjectConfiguration> GetProjectConfiguration(ProjectId projectId) =>
            await _httpFacade.Get<ProjectConfiguration>($"projects/{projectId}/approvals");

        public async Task<ProjectConfiguration> SetProjectConfiguration(ProjectConfigurationRequest request) =>
            await _httpFacade.Post<ProjectConfiguration>($"projects/{request.ProjectId}/approvals", request);

        public async Task<IList<ApprovalRule>> GetProjectApprovalRules(ProjectId projectId) =>
            await _httpFacade.Get<IList<ApprovalRule>>($"projects/{projectId}/approval_rules");

        public async Task<ApprovalRule> CreateProjectApprovalRule(CreateRegularApprovalRulesRequest request) =>
            await _httpFacade.Post<ApprovalRule>($"projects/{request.ProjectId}/approval_rules", request);

        public async Task<ApprovalRule> UpdateProjectApprovalRule(UpdateRegularApprovalRulesRequest request) =>
            await _httpFacade.Put<ApprovalRule>($"projects/{request.ProjectId}/approval_rules/{request.Id}", request);

        public async Task DeleteProjectApprovalRule(ProjectId project, int approvalRule) =>
            await _httpFacade.Delete($"projects/{project}/approval_rules/{approvalRule}");

        public async Task<ApprovalRule> CreateProjectAnyApproverRule(ProjectId projectId, int count) =>
            await _httpFacade.Post<ApprovalRule>($"projects/{projectId}/approval_rules",
                new CreateAnyUserApprovalRulesRequest(projectId, count)
            );

        public async Task<ApprovalRule> UpdateProjectAnyApproverRule(ProjectId projectId, int anyApproverRuleId,
            int count) =>
            await _httpFacade.Put<ApprovalRule>($"projects/{projectId}/approval_rules/{anyApproverRuleId}",
                new CreateAnyUserApprovalRulesRequest(anyApproverRuleId, count)
            );
    }
}
