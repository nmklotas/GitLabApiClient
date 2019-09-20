using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Users.Responses;

namespace GitLabApiClient
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
        private readonly MilestonesQueryBuilder _queryMilestonesBuilder;

        internal ProjectsClient(GitLabHttpFacade httpFacade, ProjectsQueryBuilder queryBuilder, MilestonesQueryBuilder queryMilestonesBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
            _queryMilestonesBuilder = queryMilestonesBuilder;
        }

        /// <summary>
        /// Retrieves project by its id.
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
        public async Task<IList<User>> GetUsersAsync(int projectId) =>
            await _httpFacade.GetPagedList<User>($"projects/{projectId}/users");

        /// <summary>
        /// Get the labels list of a project.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        public async Task<IList<Label>> GetLabelsAsync(int projectId) =>
            await _httpFacade.GetPagedList<Label>($"projects/{projectId}/labels");

        /// <summary>
        /// Get the milestone list of a project.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="options">Query options.</param>
        public async Task<IList<Milestone>> GetMilestonesAsync(int projectId, Action<MilestonesQueryOptions> options = null)
        {
            var queryOptions = new MilestonesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryMilestonesBuilder.Build($"projects/{projectId}/milestones", queryOptions);
            return await _httpFacade.GetPagedList<Milestone>(url);
        }

        /// <summary>
        /// Retrieves project milestone by its id
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="milestoneId">Id of the milestone.</param>
        public async Task<Milestone> GetMilestoneAsync(int projectId, int milestoneId) =>
            await _httpFacade.Get<Milestone>($"projects/{projectId}/milestones/{milestoneId}");

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
        /// Creates new project label.
        /// </summary>
        /// <param name="request">Create label request.</param>
        /// <returns>Newly created label.</returns>
        public async Task<Label> CreateLabelAsync(CreateProjectLabelRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Label>($"projects/{request.ProjectId}/labels", request);
        }

        /// <summary>
        /// Creates new project milestone.
        /// </summary>
        /// <param name="request">Create milestone request.</param>
        /// <returns>Newly created milestone.</returns>
        public async Task<Milestone> CreateMilestoneAsync(CreateProjectMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Milestone>($"projects/{request.ProjectId}/milestones", request);
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
        /// Updates an existing label with new name or new color. At least one parameter is required, to update the label.
        /// </summary>
        /// <param name="request">Update label request.</param>
        /// <returns>Newly modified label.</returns>
        public async Task<Label> UpdateLabelAsync(UpdateProjectLabelRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Label>($"projects/{request.ProjectId}/labels", request);
        }

        /// <summary>
        /// Updates an existing project milestone.
        /// </summary>
        /// <param name="request">Update milestone request.</param>
        /// <returns>Newly modified milestone.</returns>
        public async Task<Milestone> UpdateMilestoneAsync(UpdateProjectMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Milestone>($"projects/{request.ProjectId}/milestones/{request.MilestoneId}", request);
        }

        /// <summary>
        /// Deletes project.
        /// </summary>
        /// <param name="id">Id of the project.</param>
        public async Task DeleteAsync(int id) =>
            await _httpFacade.Delete($"projects/{id}");

        /// <summary>
        /// Deletes project labels.
        /// </summary>
        /// <param name="id">Id of the project.</param>
        /// <param name="name">Name of the label.</param>
        public async Task DeleteLabelAsync(int id, string name) =>
            await _httpFacade.Delete($"projects/{id}/labels?name={name}");

        /// <summary>
        /// Deletes project milestone. Only for user with developer access to the project.
        /// </summary>
        /// <param name="id">Id of the project.</param>
        /// <param name="milestoneId">The ID of the project�s milestone.</param>
        public async Task DeleteMilestoneAsync(int id, int milestoneId) =>
            await _httpFacade.Delete($"projects/{id}/milestones/{milestoneId}");

        /// <summary>
        /// Archive project.
        /// </summary>
        /// <param name="id">Id of the project.</param>
        public async Task ArchiveAsync(int id) =>
            await _httpFacade.Post($"projects/{id}/archive");

        /// <summary>
        /// Unarchive project.
        /// </summary>
        /// <param name="id">Id of the project.</param>
        public async Task UnArchiveAsync(int id) =>
            await _httpFacade.Post($"projects/{id}/unarchive");

        public async Task<Project> Transfer(string id, TransferProjectRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Project>($"projects/{id}/transfer", request);
        }
    }
}