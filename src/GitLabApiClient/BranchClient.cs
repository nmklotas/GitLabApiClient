using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
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

        public async Task<Branch> GetAsync(object projectId, string branchName) =>
            await _httpFacade.Get<Branch>($"{projectId.ProjectBaseUrl()}/repository/branches/{branchName}");

        public async Task<IList<Branch>> GetAsync(object projectId, Action<BranchQueryOptions> options)
        {
            var queryOptions = new BranchQueryOptions();
            options?.Invoke(queryOptions);

            string url = _branchQueryBuilder.Build($"{projectId.ProjectBaseUrl()}/repository/branches", queryOptions);
            return await _httpFacade.GetPagedList<Branch>(url);
        }

        public async Task<Branch> CreateAsync(object projectId, CreateBranchRequest request) =>
            await _httpFacade.Post<Branch>($"{projectId.ProjectBaseUrl()}/repository/branches", request);

        public async Task DeleteBranch(object projectId, string branchName) =>
            await _httpFacade.Delete($"{projectId.ProjectBaseUrl()}/repository/branches/{branchName}");

        public async Task DeleteMergedBranches(object projectId) =>
            await _httpFacade.Delete($"{projectId.ProjectBaseUrl()}/repository/merged_branches");

        public async Task<ProtectedBranch> GetProtectedBranchesAsync(object projectId, string branchName) =>
            await _httpFacade.Get<ProtectedBranch>($"{projectId.ProjectBaseUrl()}/protected_branches/{branchName}");

        public async Task<IList<ProtectedBranch>> GetProtectedBranchesAsync(object projectId) =>
            await _httpFacade.GetPagedList<ProtectedBranch>($"{projectId.ProjectBaseUrl()}/protected_branches");

        public async Task<ProtectedBranch> ProtectBranchAsync(object projectId, ProtectBranchRequest request) =>
            await _httpFacade.Post<ProtectedBranch>($"{projectId.ProjectBaseUrl()}/protected_branches", request);

        public async Task UnprotectBranchAsync(object projectId, string branchName) =>
            await _httpFacade.Delete($"{projectId.ProjectBaseUrl()}/protected_branches/{branchName}");
    }
}
