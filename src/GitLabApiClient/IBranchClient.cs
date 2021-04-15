using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Branches.Requests;
using GitLabApiClient.Models.Branches.Responses;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient
{
    public interface IBranchClient
    {/// <summary>
     /// Retrieves a single branch
     /// </summary>
     /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
     /// <param name="branchName">The branch name.</param>
     /// <returns></returns>
        Task<Branch> GetAsync(ProjectId projectId, string branchName);

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options <see cref="BranchQueryOptions"/></param>
        /// <returns></returns>
        Task<IList<Branch>> GetAsync(ProjectId projectId, Action<BranchQueryOptions> options);

        /// <summary>
        /// Creates a branch
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create branch request</param>
        /// <returns></returns>
        Task<Branch> CreateAsync(ProjectId projectId, CreateBranchRequest request);

        /// <summary>
        /// Deletes a branch
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="branchName">The branch, you want deleted.</param>
        Task DeleteBranch(ProjectId projectId, string branchName);

        /// <summary>
        /// Deletes the merged branches
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        Task DeleteMergedBranches(ProjectId projectId);

        /// <summary>
        /// Retrieve a single protected branch information.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="branchName">The protected branch</param>
        /// <returns>A protected branch</returns>
        Task<ProtectedBranch> GetProtectedBranchesAsync(ProjectId projectId, string branchName);

        /// <summary>
        /// Retrieves a list of Protected Branches from a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>List of protected branches.</returns>
        Task<IList<ProtectedBranch>> GetProtectedBranchesAsync(ProjectId projectId);

        /// <summary>
        /// Protect a branch
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Protect branch request <see cref="ProtectBranchRequest"/>.</param>
        /// <returns>The newly protected branch.</returns>
        Task<ProtectedBranch> ProtectBranchAsync(ProjectId projectId, ProtectBranchRequest request);

        /// <summary>
        /// Unprotect a branch
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="branchName">The Branch, you want to unprotect.</param>
        Task UnprotectBranchAsync(ProjectId projectId, string branchName);
    }
}
