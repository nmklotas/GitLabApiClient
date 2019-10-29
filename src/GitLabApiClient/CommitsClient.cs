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
    public sealed class CommitsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly CommitQueryBuilder _commitQueryBuilder;

        internal CommitsClient(GitLabHttpFacade httpFacade, CommitQueryBuilder commitQueryBuilder)
        {
            _httpFacade = httpFacade;
            _commitQueryBuilder = commitQueryBuilder;
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
    }
}
