using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Issues;

namespace GitLabApiClient
{
    public class IssuesClient
    {
        private readonly GitlabHttpFacade _httpFacade;

        internal IssuesClient(GitlabHttpFacade httpFacade) =>
            _httpFacade = httpFacade;

        public async Task<IList<Issue>> GetAsync(int projectId) => 
            await _httpFacade.GetAll<Issue>($"/projects/{projectId}/issues");

        public async Task<Issue> GetAsync(int projectId, int issueId) => 
            await _httpFacade.Get<Issue>($"/projects/{projectId}/issues/{issueId}");

        public async Task<IList<Issue>> GetOwnedAsync() =>
            await _httpFacade.GetAll<Issue>("/issues");

        public async Task<Issue> CreateAsync(CreateIssueRequest request) => 
            await _httpFacade.Post<Issue>($"/projects/{request.ProjectId}/issues", request);

        public async Task<Issue> EditAsync(EditIssueRequest request) =>
            await _httpFacade.Put<Issue>($"/projects/{request.ProjectId}/issues/{request.IssueId}", request);
    }
}
