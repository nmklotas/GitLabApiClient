using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Commits.Requests;
using GitLabApiClient.Models.Commits.Requests.CreateCommitRequest;
using GitLabApiClient.Models.Commits.Responses;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient
{
    public interface ICommitsClient
    {
        /// <summary>
        /// Get a commit from commit sha
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="sha">The commit hash or name of a repository branch or tag</param>
        /// <returns></returns>
        Task<Commit> GetAsync(ProjectId projectId, string sha);

        /// <summary>
        /// Retrieve a list of commits from a project
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query Options <see cref="CommitQueryOptions"/>.</param>
        /// <returns></returns>
        Task<IList<Commit>> GetAsync(ProjectId projectId, Action<CommitQueryOptions> options = null);

        /// <summary>
        /// Retrieve a list of references (from branch or / and tag) that this commit belongs to
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query Options <see cref="CommitRefsQueryOptions"/>.</param>
        /// <param name="sha">The commit hash or name of a repository branch or tag</param>
        /// <returns></returns>
        Task<IList<CommitRef>> GetRefsAsync(ProjectId projectId, string sha, Action<CommitRefsQueryOptions> options);

        /// <summary>
        /// Retrieve a list of differences in this commit
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="sha">The commit hash or name of a repository branch or tag</param>
        /// <returns></returns>
        Task<IList<Diff>> GetDiffsAsync(ProjectId projectId, string sha);

        /// <summary>
        /// Retrieve a list of statuses in this commit
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query Options <see cref="CommitStatusesQueryOptions"/>.</param>
        /// <param name="sha">The commit hash</param>
        /// <returns></returns>
        Task<IList<CommitStatuses>> GetStatusesAsync(ProjectId projectId, string sha, Action<CommitStatusesQueryOptions> options = null);

        /// <summary>
        /// Creates a commit with multiple files and actions.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create commit request.</param>
        /// <param name="autoEncodeToBase64">Automatically encode contents to base64 (default false).</param>
        Task<Commit> CreateAsync(ProjectId projectId, CreateCommitRequest request, bool autoEncodeToBase64 = false);
    }
}
