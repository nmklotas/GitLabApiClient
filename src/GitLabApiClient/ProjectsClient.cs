using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Projects;
using GitLabApiClient.Models.Users;
using GitLabApiClient.Utilities;

namespace GitLabApiClient
{
    public class ProjectsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly ProjectsQueryBuilder _queryBuilder;

        internal ProjectsClient(GitLabHttpFacade httpFacade, ProjectsQueryBuilder queryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
        }

        public async Task<Project> GetAsync(int projectId) =>
            await _httpFacade.Get<Project>($"/projects/{projectId}");

        public async Task<IList<Project>> GetAsync(Action<ProjectQueryOptions> options = null)
        {
            var queryOptions = new ProjectQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build("/projects", queryOptions);
            return await _httpFacade.GetPagedList<Project>(url);
        }

        public async Task<IList<User>> GetUsers(int projectId) =>
            await _httpFacade.GetPagedList<User>($"/projects/{projectId}/users");

        public async Task<Project> CreateAsync(CreateProjectRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Project>("/projects", request);
        }

        public async Task DeleteAsync(int id) => 
            await _httpFacade.Delete($"/projects/{id}");
    }
}