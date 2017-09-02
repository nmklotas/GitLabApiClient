using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Issues;

namespace GitLabApiClient
{
    public class IssuesClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly IssuesQueryBuilder _queryBuilder;

        internal IssuesClient(GitLabHttpFacade httpFacade, IssuesQueryBuilder queryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
        }

        public async Task<Issue> GetAsync(int projectId, int issueId) => 
            await _httpFacade.Get<Issue>($"/projects/{projectId}/issues/{issueId}");

        public async Task<IList<Issue>> GetAsync(Action<IssuesQueryOptions> options = null)
        {
            var queryOptions = new IssuesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build("/issues", queryOptions);
            return await _httpFacade.GetPagedList<Issue>(url);
        }

        public async Task<IList<Issue>> GetAsync(string projectId, Action<ProjectIssuesQueryOptions> options = null)
        {
            var queryOptions = new ProjectIssuesQueryOptions(projectId);
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build("/issues", queryOptions);
            return await _httpFacade.GetPagedList<Issue>(url);
        }

        public async Task<Issue> CreateAsync(CreateIssueRequest request) => 
            await _httpFacade.Post<Issue>($"/projects/{request.ProjectId}/issues", request);

        public async Task<Issue> EditAsync(UpdateIssueRequest request) =>
            await _httpFacade.Put<Issue>($"/projects/{request.ProjectId}/issues/{request.IssueId}", request);
    }
}
