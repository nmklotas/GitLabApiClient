using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Projects;

namespace GitLabApiClient
{
    public class ProjectsClient
    {
        private readonly GitlabHttpFacade _httpFacade;

        internal ProjectsClient(GitlabHttpFacade httpFacade) => 
            _httpFacade = httpFacade;

        public async Task<IList<Project>> GetAsync() =>
            await _httpFacade.GetAll<Project>("/projects/all");

        public async Task<IList<Project>> GetAccessibleAsync() => 
            await _httpFacade.GetAll<Project>("/projects");

        public async Task<IList<Project>> GetOwnedAsync() => 
            await _httpFacade.GetAll<Project>("/projects/owned");

        public async Task<Project> CreateAsync(CreateProjectRequest request) => 
            await _httpFacade.Post<Project>("/projects", request);

        public async Task DeleteAsync(int id) => 
            await _httpFacade.Delete($"/projects/{id}");
    }
}
