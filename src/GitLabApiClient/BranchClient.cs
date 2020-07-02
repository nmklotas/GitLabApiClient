using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Branches.Requests;
using GitLabApiClient.Models.Branches.Responses;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient
{
    public sealed class BranchClient : IBranchClient
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

        /// <summary>
        /// Retrieves a single branch
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="branchName">The branch name.</param>
        /// <returns></returns>
        public async Task<Branch> GetAsync(ProjectId projectId, string branchName) =>
            await _httpFacade.Get<Branch>($"projects/{projectId}/repository/branches/{branchName}");

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options <see cref="BranchQueryOptions"/></param>
        /// <returns></returns>
        public async Task<IList<Branch>> GetAsync(ProjectId projectId, Action<BranchQueryOptions> options)
        {
            var queryOptions = new BranchQueryOptions();
            options?.Invoke(queryOptions);

            string url = _branchQueryBuilder.Build($"projects/{projectId}/repository/branches", queryOptions);
            return await _httpFacade.GetPagedList<Branch>(url);
        }

        /// <summary>
        /// Creates a branch
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create branch request</param>
        /// <returns></returns>
        public async Task<Branch> CreateAsync(ProjectId projectId, CreateBranchRequest request) =>
            await _httpFacade.Post<Branch>($"projects/{projectId}/repository/branches", request);

        /// <summary>
        /// Deletes a branch
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="branchName">The branch, you want deleted.</param>
        public async Task DeleteBranch(ProjectId projectId, string branchName) =>
            await _httpFacade.Delete($"projects/{projectId}/repository/branches/{branchName}");

        /// <summary>
        /// Deletes the merged branches
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task DeleteMergedBranches(ProjectId projectId) =>
            await _httpFacade.Delete($"projects/{projectId}/repository/merged_branches");

        /// <summary>
        /// Retrieve a single protected branch information.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="branchName">The protected branch</param>
        /// <returns>A protected branch</returns>
        public async Task<ProtectedBranch> GetProtectedBranchesAsync(ProjectId projectId, string branchName) =>
            await _httpFacade.Get<ProtectedBranch>($"projects/{projectId}/protected_branches/{branchName}");

        /// <summary>
        /// Retrieves a list of Protected Branches from a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>List of protected branches.</returns>
        public async Task<IList<ProtectedBranch>> GetProtectedBranchesAsync(ProjectId projectId) =>
            await _httpFacade.GetPagedList<ProtectedBranch>($"projects/{projectId}/protected_branches");

        /// <summary>
        /// Protect a branch
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Protect branch request <see cref="ProtectBranchRequest"/>.</param>
        /// <returns>The newly protected branch.</returns>
        public async Task<ProtectedBranch> ProtectBranchAsync(ProjectId projectId, ProtectBranchRequest request) =>
            await _httpFacade.Post<ProtectedBranch>($"projects/{projectId}/protected_branches", request);

        /// <summary>
        /// Unprotect a branch
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="branchName">The Branch, you want to unprotect.</param>
        public async Task UnprotectBranchAsync(ProjectId projectId, string branchName) =>
            await _httpFacade.Delete($"projects/{projectId}/protected_branches/{branchName}");
    }
}
