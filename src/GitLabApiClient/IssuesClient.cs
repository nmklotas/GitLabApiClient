using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Issues;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to retrieve, modify, create issues.
    /// Every call to issues API must be authenticated.
    /// <exception cref="GitLabException">Thrown if request to GitLab API does not indicate success</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class IssuesClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly IssuesQueryBuilder _queryBuilder;
        private readonly ProjectIssuesQueryBuilder _projectIssuesQueryBuilder;

        internal IssuesClient(
            GitLabHttpFacade httpFacade, 
            IssuesQueryBuilder queryBuilder,
            ProjectIssuesQueryBuilder projectIssuesQueryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
            _projectIssuesQueryBuilder = projectIssuesQueryBuilder;
        }

        /// <summary>
        /// Retrieves project issue.
        /// </summary>
        public async Task<Issue> GetAsync(int projectId, int issueId) => 
            await _httpFacade.Get<Issue>($"/projects/{projectId}/issues/{issueId}");

        /// <summary>
        /// Retrieves issues from a project.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Issue>> GetAsync(string projectId, Action<ProjectIssuesQueryOptions> options = null)
        {
            var queryOptions = new ProjectIssuesQueryOptions(projectId);
            options?.Invoke(queryOptions);

            string url = _projectIssuesQueryBuilder.Build("/issues", queryOptions);
            return await _httpFacade.GetPagedList<Issue>(url);
        }

        /// <summary>
        /// Retrieves issues from all projects.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Issue>> GetAsync(Action<IssuesQueryOptions> options = null)
        {
            var queryOptions = new IssuesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build("/issues", queryOptions);
            return await _httpFacade.GetPagedList<Issue>(url);
        }

        /// <summary>
        /// Creates new issue.
        /// </summary>
        /// <returns>The newly created issue.</returns>
        public async Task<Issue> CreateAsync(CreateIssueRequest request) => 
            await _httpFacade.Post<Issue>($"/projects/{request.ProjectId}/issues", request);

        /// <summary>
        /// Updated existing issue.
        /// </summary>
        /// <returns>The updated issue.</returns>
        public async Task<Issue> UpdateAsync(UpdateIssueRequest request) =>
            await _httpFacade.Put<Issue>($"/projects/{request.ProjectId}/issues/{request.IssueId}", request);
    }
}
