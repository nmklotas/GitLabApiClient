using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Branches.Requests;
using GitLabApiClient.Models.Branches.Responses;

namespace GitLabApiClient
{
    public sealed class BranchClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly BranchQueryBuilder _branchQueryBuilder;

        internal BranchClient(
            GitLabHttpFacade httpFacade,
            BranchQueryBuilder branchQueryBuilder)
        {
            _httpFacade = httpFacade;
            _branchQueryBuilder = branchQueryBuilder;
        }

        public async Task<Branch> GetAsync(string projectId, string branchName) =>
            await _httpFacade.Get<Branch>($"projects/{projectId}/repository/branches/{branchName}");

        public async Task<IList<Branch>> GetAsync(string projectId, Action<BranchQueryOptions> options)
        {
            var queryOptions = new BranchQueryOptions();
            options?.Invoke(queryOptions);

            string url = _branchQueryBuilder.Build($"projects/{projectId}/repository/branches", queryOptions);
            return await _httpFacade.GetPagedList<Branch>(url);
        }

        public async Task<Branch> CreateAsync(CreateBranchRequest request) =>
            await _httpFacade.Post<Branch>($"projects/{request.ProjectId}/repository/branches", request);

        public async Task DeleteBranch(DeleteBranchRequest request) =>
            await _httpFacade.Delete($"projects/{request.ProjectId}/repository/branches/{request.BranchName}");

        public async Task DeleteMergedBranches(DeleteMergedBranchesRequest request) =>
            await _httpFacade.Delete($"projects/{request.ProjectId}/repository/merged_branches");

        public async Task<ProtectedBranch> GetProtectedBranchesAsync(string projectId, string branchName) =>
            await _httpFacade.Get<ProtectedBranch>($"projects/{projectId}/protected_branches/{branchName}");

        public async Task<IList<ProtectedBranch>> GetProtectedBranchesAsync(string projectId) =>
            await _httpFacade.GetPagedList<ProtectedBranch>($"projects/{projectId}/protected_branches");

        public async Task<ProtectedBranch> ProtectBranchAsync(ProtectBranchRequest request) =>
            await _httpFacade.Post<ProtectedBranch>($"projects/{request.ProjectId}/protected_branches", request);

        public async Task UnprotectBranchAsync(string projectId, string branchName) =>
            await _httpFacade.Delete($"projects/{projectId}/protected_branches/{branchName}");
    }
}
