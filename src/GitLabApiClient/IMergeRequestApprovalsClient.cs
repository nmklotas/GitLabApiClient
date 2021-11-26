using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.MergeRequestApprovals.Requests;
using GitLabApiClient.Models.MergeRequestApprovals.Responses;

namespace GitLabApiClient
{
    public interface IMergeRequestApprovalsClient
    {
        Task ApproveAsync(ProjectId projectId, int mergeRequestIid, string sha = null);
        Task UnapproveAsync(ProjectId projectId, int mergeRequestIid);
        Task<Approval> GetAsync(ProjectId projectId, int mergeRequestIid);
        Task<ProjectConfiguration> GetProjectConfiguration(ProjectId projectId);
        Task<ProjectConfiguration> SetProjectConfiguration(ProjectConfigurationRequest request);
        Task<IList<ApprovalRule>> GetProjectApprovalRules(ProjectId projectId);
        Task<ApprovalRule> CreateProjectApprovalRule(CreateRegularApprovalRulesRequest request);
        Task<ApprovalRule> UpdateProjectApprovalRule(UpdateRegularApprovalRulesRequest request);
        Task DeleteProjectApprovalRule(ProjectId project, int approvalRule);
        Task<ApprovalRule> CreateProjectAnyApproverRule(ProjectId projectId, int count);
        Task<ApprovalRule> UpdateProjectAnyApproverRule(ProjectId projectId, int anyApproverRuleId, int count);
    }
}
