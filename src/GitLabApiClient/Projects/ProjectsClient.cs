using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Projects.Requests;
using GitLabApiClient.Projects.Requests.Queries;
using GitLabApiClient.Projects.Responses;
using GitLabApiClient.Users.Responses;
using GitLabApiClient.Utilities;

namespace GitLabApiClient.Projects
{
    /// <summary>
    /// Used to query GitLab API to retrieve, modify, create projects.
    /// <exception cref="GitLabException">Thrown if request to GitLab API fails</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class ProjectsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly ProjectsQueryBuilder _queryBuilder;

        internal ProjectsClient(GitLabHttpFacade httpFacade, ProjectsQueryBuilder queryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Retrieves project by it's id.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        public async Task<Project> GetAsync(int projectId) =>
            await _httpFacade.Get<Project>($"projects/{projectId}");

        /// <summary>
        /// Get a list of visible projects for authenticated user. 
        /// When accessed without authentication, only public projects are returned.
        /// </summary>
        /// <param name="options">Query options.</param>
        public async Task<IList<Project>> GetAsync(Action<ProjectQueryOptions> options = null)
        {
            var queryOptions = new ProjectQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build("projects", queryOptions);
            return await _httpFacade.GetPagedList<Project>(url);
        }

        /// <summary>
        /// Get the users list of a project.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        public async Task<IList<User>> GetUsers(int projectId) =>
            await _httpFacade.GetPagedList<User>($"projects/{projectId}/users");

        /// <summary>
        /// Creates new project.
        /// </summary>
        /// <param name="request">Create project request.</param>
        /// <returns>Newly created project.</returns>
        public async Task<Project> CreateAsync(CreateProjectRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Project>("projects", request);
        }

        /// <summary>
        /// Updates existing project.
        /// </summary>
        /// <param name="request">Update project request.</param>
        /// <returns>Newly modified project.</returns>
        public async Task<Project> UpdateAsync(UpdateProjectRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Project>($"projects/{request.ProjectId}", request);
        }

        /// <summary>
        /// Deletes project.
        /// </summary>
        /// <param name="id">Id of the project.</param>
        public async Task DeleteAsync(int id) => 
            await _httpFacade.Delete($"projects/{id}");
    }
}