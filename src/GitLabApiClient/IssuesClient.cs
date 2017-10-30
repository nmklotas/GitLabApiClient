using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;

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
        public async Task<Issue> GetAsync(int projectId, int issueId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var registration = cancellationToken.Register(cancellationToken.ThrowIfCancellationRequested))
            {
            }
            return await _httpFacade.Get<Issue>($"projects/{projectId}/issues/{issueId}", cancellationToken);
        }
        
        /// <summary>
        /// Retrieves issues from a project.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Issue>> GetProjectIssuesAsync(string projectId, CancellationToken cancellationToken)
        {
            return await GetProjectIssuesAsync(projectId, null, cancellationToken);
        }
        
        /// <summary>
        /// Retrieves issues from a project.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="options">Issues retrieval options.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Issue>> GetProjectIssuesAsync(string projectId,
                                                              Action<ProjectIssuesQueryOptions> options,
                                                              CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var registration = cancellationToken.Register(cancellationToken.ThrowIfCancellationRequested))
            {
            }
            var queryOptions = new ProjectIssuesQueryOptions(projectId);
            options?.Invoke(queryOptions);

            string url = _projectIssuesQueryBuilder.Build($"projects/{projectId}/issues", queryOptions);
            return await _httpFacade.GetPagedList<Issue>(url, cancellationToken);
        }

        /// <summary>
        /// Retrieves issues from all projects.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Issue>> GetAsync(CancellationToken cancellationToken)
        {
            return await GetAsync(null, cancellationToken);
        }
        
        /// <summary>
        /// Retrieves issues from all projects.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="options">Issues retrieval options.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Issue>> GetAsync(Action<IssuesQueryOptions> options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var registration = cancellationToken.Register(cancellationToken.ThrowIfCancellationRequested))
            {
            }
            var queryOptions = new IssuesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build("issues", queryOptions);
            return await _httpFacade.GetPagedList<Issue>(url, cancellationToken);
        }

        /// <summary>
        /// Creates new issue.
        /// </summary>
        /// <returns>The newly created issue.</returns>
        public async Task<Issue> CreateAsync(CreateIssueRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var registration = cancellationToken.Register(cancellationToken.ThrowIfCancellationRequested))
            {
            }
            return await _httpFacade.Post<Issue>($"projects/{request.ProjectId}/issues", request, cancellationToken);
        }


        /// <summary>
        /// Updated existing issue.
        /// </summary>
        /// <returns>The updated issue.</returns>
        public async Task<Issue> UpdateAsync(UpdateIssueRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var registration = cancellationToken.Register(cancellationToken.ThrowIfCancellationRequested))
            {
            }
            return await _httpFacade.Put<Issue>($"projects/{request.ProjectId}/issues/{request.IssueId}", request, cancellationToken);
        }
            
    }
}
