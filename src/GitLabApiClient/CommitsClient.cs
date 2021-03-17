using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Commits.Requests;
using GitLabApiClient.Models.Commits.Responses;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient
{
    public sealed class CommitsClient : ICommitsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly CommitQueryBuilder _commitQueryBuilder;
        private readonly CommitRefsQueryBuilder _commitRefsQueryBuilder;
        private readonly CommitStatusesQueryBuilder _commitStatusesQueryBuilder;

        internal CommitsClient(GitLabHttpFacade httpFacade, CommitQueryBuilder commitQueryBuilder, CommitRefsQueryBuilder commitRefsQueryBuilder, CommitStatusesQueryBuilder commitStatusesQueryBuilder)
        {
            _httpFacade = httpFacade;
            _commitQueryBuilder = commitQueryBuilder;
            _commitRefsQueryBuilder = commitRefsQueryBuilder;
            _commitStatusesQueryBuilder = commitStatusesQueryBuilder;
        }

        /// <summary>
        /// Get a commit from commit sha
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="sha">The commit hash or name of a repository branch or tag</param>
        /// <returns></returns>
        public async Task<Commit> GetAsync(ProjectId projectId, string sha) =>
           await _httpFacade.Get<Commit>($"projects/{projectId}/repository/commits/{sha}");

        /// <summary>
        /// Retrieve a list of commits from a project
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query Options <see cref="CommitQueryOptions"/>.</param>
        /// <returns></returns>
        public async Task<IList<Commit>> GetAsync(ProjectId projectId, Action<CommitQueryOptions> options = null)
        {
            var queryOptions = new CommitQueryOptions();
            options?.Invoke(queryOptions);

            string url = _commitQueryBuilder.Build($"projects/{projectId}/repository/commits", queryOptions);
            return await _httpFacade.GetPagedList<Commit>(url);
        }

        /// <summary>
        /// Retrieve a list of references (from branch or / and tag) that this commit belongs to
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query Options <see cref="CommitRefsQueryOptions"/>.</param>
        /// <param name="sha">The commit hash or name of a repository branch or tag</param>
        /// <returns></returns>
        public async Task<IList<CommitRef>> GetRefsAsync(ProjectId projectId, string sha, Action<CommitRefsQueryOptions> options)
        {
            var queryOptions = new CommitRefsQueryOptions();
            options?.Invoke(queryOptions);

            string url = _commitRefsQueryBuilder.Build($"projects/{projectId}/repository/commits/{sha}/refs", queryOptions);
            return await _httpFacade.GetPagedList<CommitRef>(url);
        }

        /// <summary>
        /// Retrieve a list of differences in this commit
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="sha">The commit hash or name of a repository branch or tag</param>
        /// <returns></returns>
        public async Task<IList<Diff>> GetDiffsAsync(ProjectId projectId, string sha)
        {
            string url = $"projects/{projectId}/repository/commits/{sha}/diff";
            return await _httpFacade.GetPagedList<Diff>(url);
        }

        /// <summary>
        /// Retrieve a list of statuses in this commit
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query Options <see cref="CommitStatusesQueryOptions"/>.</param>
        /// <param name="sha">The commit hash</param>
        /// <returns></returns>
        public async Task<IList<CommitStatuses>> GetStatusesAsync(ProjectId projectId, string sha, Action<CommitStatusesQueryOptions> options = null)
        {
            var queryOptions = new CommitStatusesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _commitStatusesQueryBuilder.Build($"projects/{projectId}/repository/commits/{sha}/statuses", queryOptions);
            return await _httpFacade.GetPagedList<CommitStatuses>(url);
        }
    }
}
