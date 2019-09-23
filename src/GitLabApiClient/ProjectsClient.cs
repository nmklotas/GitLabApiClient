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
        /// Retrieves project by its id, path or <see cref="Project"/>.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task<Project> GetAsync(object projectId) =>
            await _httpFacade.Get<Project>(projectId.ProjectBaseUrl());

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
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task<IList<User>> GetUsersAsync(object projectId) =>
            await _httpFacade.GetPagedList<User>($"{projectId.ProjectBaseUrl()}/users");

        /// <summary>
        /// Get the labels list of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task<IList<Label>> GetLabelsAsync(object projectId) =>
            await _httpFacade.GetPagedList<Label>($"{projectId.ProjectBaseUrl()}/labels");

        /// <summary>
        /// Get the milestone list of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options.</param>
        public async Task<IList<Milestone>> GetMilestonesAsync(object projectId, Action<MilestonesQueryOptions> options = null)
        {
            var queryOptions = new MilestonesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryMilestonesBuilder.Build($"{projectId.ProjectBaseUrl()}/milestones", queryOptions);
            return await _httpFacade.GetPagedList<Milestone>(url);
        }

        /// <summary>
        /// Retrieves project milestone by its id
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="milestoneId">Id of the milestone.</param>
        public async Task<Milestone> GetMilestoneAsync(object projectId, int milestoneId) =>
            await _httpFacade.Get<Milestone>($"{projectId.ProjectBaseUrl()}/milestones/{milestoneId}");

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
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create label request.</param>
        /// <returns>Newly created label.</returns>
        public async Task<Label> CreateLabelAsync(object projectId, CreateProjectLabelRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Label>($"{projectId.ProjectBaseUrl()}/labels", request);
        }

        /// <summary>
        /// Creates new project milestone.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create milestone request.</param>
        /// <returns>Newly created milestone.</returns>
        public async Task<Milestone> CreateMilestoneAsync(object projectId, CreateProjectMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Milestone>($"{projectId.ProjectBaseUrl()}/milestones", request);
        }

        /// <summary>
        /// Updates existing project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update project request.</param>
        /// <returns>Newly modified project.</returns>
        public async Task<Project> UpdateAsync(object projectId, UpdateProjectRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Project>(projectId.ProjectBaseUrl(), request);
        }

        /// <summary>
        /// Updates an existing label with new name or new color. At least one parameter is required, to update the label.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update label request.</param>
        /// <returns>Newly modified label.</returns>
        public async Task<Label> UpdateLabelAsync(object projectId, UpdateProjectLabelRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Label>($"{projectId.ProjectBaseUrl()}/labels", request);
        }

        /// <summary>
        /// Updates an existing project milestone.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="milestoneId">The ID of a project milestone.</param>
        /// <param name="request">Update milestone request.</param>
        /// <returns>Newly modified milestone.</returns>
        public async Task<Milestone> UpdateMilestoneAsync(object projectId, int milestoneId, UpdateProjectMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Milestone>($"{projectId.ProjectBaseUrl()}/milestones/{milestoneId}", request);
        }

        /// <summary>
        /// Deletes project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task DeleteAsync(object projectId) =>
            await _httpFacade.Delete(projectId.ProjectBaseUrl());

        /// <summary>
        /// Deletes project labels.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="name">Name of the label.</param>
        public async Task DeleteLabelAsync(object projectId, string name) =>
            await _httpFacade.Delete($"{projectId.ProjectBaseUrl()}/labels?name={name}");

        /// <summary>
        /// Deletes project milestone. Only for user with developer access to the project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="milestoneId">The ID of the project�s milestone.</param>
        public async Task DeleteMilestoneAsync(object projectId, int milestoneId) =>
            await _httpFacade.Delete($"{projectId.ProjectBaseUrl()}/milestones/{milestoneId}");

        /// <summary>
        /// Archive project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task ArchiveAsync(object projectId) =>
            await _httpFacade.Post($"{projectId.ProjectBaseUrl()}/archive");

        /// <summary>
        /// Unarchive project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task UnArchiveAsync(object projectId) =>
            await _httpFacade.Post($"{projectId.ProjectBaseUrl()}/unarchive");

        public async Task<Project> Transfer(object projectId, TransferProjectRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Project>($"{projectId.ProjectBaseUrl()}/transfer", request);
        }
    }
}
