using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Projects;
using GitLabApiClient.Models.Users;

namespace GitLabApiClient
{
    public class ProjectsClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal ProjectsClient(GitLabHttpFacade httpFacade) => 
            _httpFacade = httpFacade;

        public async Task<IList<Project>> GetAsync() => 
            await _httpFacade.GetPagedList<Project>("/projects");

        public async Task<Project> GetAsync(int projectId) =>
            await _httpFacade.Get<Project>($"/projects/{projectId}");

        public async Task<IList<Project>> GetAsync(string name) =>
            await _httpFacade.GetPagedList<Project>($"/projects/?search={name}");

        public async Task<IList<Project>> GetByUserIdAsync(int userId) =>
            await _httpFacade.GetPagedList<Project>($"/users/{userId}/projects");

        public async Task<IList<User>> GetUsers(int projectId) =>
            await _httpFacade.GetPagedList<User>($"/projects/{projectId}/users");

        public async Task<Project> CreateAsync(CreateProjectRequest request) => 
            await _httpFacade.Post<Project>("/projects", request);

        public async Task DeleteAsync(int id) => 
            await _httpFacade.Delete($"/projects/{id}");
    }
}
