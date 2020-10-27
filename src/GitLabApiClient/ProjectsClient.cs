using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Job.Requests;
using GitLabApiClient.Models.Job.Responses;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Runners.Responses;
using GitLabApiClient.Models.Uploads.Requests;
using GitLabApiClient.Models.Users.Responses;
using GitLabApiClient.Models.Variables.Request;
using GitLabApiClient.Models.Variables.Response;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to retrieve, modify, create projects.
    /// <exception cref="GitLabException">Thrown if request to GitLab API fails</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class ProjectsClient : IProjectsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly ProjectsQueryBuilder _queryBuilder;
        private readonly MilestonesQueryBuilder _queryMilestonesBuilder;
        private readonly JobQueryBuilder _jobQueryBuilder;

        internal ProjectsClient(GitLabHttpFacade httpFacade, ProjectsQueryBuilder queryBuilder, MilestonesQueryBuilder queryMilestonesBuilder, JobQueryBuilder jobQueryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
            _queryMilestonesBuilder = queryMilestonesBuilder;
            _jobQueryBuilder = jobQueryBuilder;
        }

        /// <summary>
        /// Retrieves project by its id, path or <see cref="Project"/>.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task<Project> GetAsync(ProjectId projectId) =>
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
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task<IList<User>> GetUsersAsync(ProjectId projectId) =>
            await _httpFacade.GetPagedList<User>($"projects/{projectId}/users");

        /// <summary>
        /// Retrieves project variables by its id.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        public async Task<IList<Variable>> GetVariablesAsync(ProjectId projectId) =>
            await _httpFacade.GetPagedList<Variable>($"projects/{projectId}/variables");

        /// <summary>
        /// Get the labels list of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task<IList<Label>> GetLabelsAsync(ProjectId projectId) =>
            await _httpFacade.GetPagedList<Label>($"projects/{projectId}/labels");

        /// <summary>
        /// Get the milestone list of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options.</param>
        public async Task<IList<Milestone>> GetMilestonesAsync(ProjectId projectId, Action<MilestonesQueryOptions> options = null)
        {
            var queryOptions = new MilestonesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryMilestonesBuilder.Build($"projects/{projectId}/milestones", queryOptions);
            return await _httpFacade.GetPagedList<Milestone>(url);
        }

        /// <summary>
        /// Get the jobs list of a project
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options.</param>
        public async Task<IList<Job>> GetJobsAsync(ProjectId projectId, Action<JobQueryOptions> options = null)
        {
            var queryOptions = new JobQueryOptions();
            options?.Invoke(queryOptions);

            var url = _jobQueryBuilder.Build($"projects/{projectId}/jobs", queryOptions);
            return await _httpFacade.GetPagedList<Job>(url);
        }

        /// <summary>
        /// Retrieves project milestone by its id
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="milestoneId">Id of the milestone.</param>
        public async Task<Milestone> GetMilestoneAsync(ProjectId projectId, int milestoneId) =>
            await _httpFacade.Get<Milestone>($"projects/{projectId}/milestones/{milestoneId}");

        /// <summary>
        /// Get the runners list of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task<IList<Runner>> GetRunnersAsync(ProjectId projectId) =>
            await _httpFacade.GetPagedList<Runner>($"projects/{projectId}/runners");

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
        public async Task<Label> CreateLabelAsync(ProjectId projectId, CreateProjectLabelRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Label>($"projects/{projectId}/labels", request);
        }

        /// <summary>
        /// Creates new project variable.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create variable request.</param>
        /// <returns>Newly created variable.</returns>
        public async Task<Variable> CreateVariableAsync(ProjectId projectId, CreateVariableRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Variable>($"projects/{projectId}/variables", request);
        }

        /// <summary>
        /// Creates new project milestone.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create milestone request.</param>
        /// <returns>Newly created milestone.</returns>
        public async Task<Milestone> CreateMilestoneAsync(ProjectId projectId, CreateProjectMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Milestone>($"projects/{projectId}/milestones", request);
        }

        /// <summary>
        /// Updates existing project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update project request.</param>
        /// <returns>Newly modified project.</returns>
        public async Task<Project> UpdateAsync(ProjectId projectId, UpdateProjectRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Project>($"projects/{projectId}", request);
        }

        /// <summary>
        /// Updates an existing label with new name or new color. At least one parameter is required, to update the label.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update label request.</param>
        /// <returns>Newly modified label.</returns>
        public async Task<Label> UpdateLabelAsync(ProjectId projectId, UpdateProjectLabelRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Label>($"projects/{projectId}/labels", request);
        }

        /// <summary>
        /// Updates an existing project milestone.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="milestoneId">The ID of a project milestone.</param>
        /// <param name="request">Update milestone request.</param>
        /// <returns>Newly modified milestone.</returns>
        public async Task<Milestone> UpdateMilestoneAsync(ProjectId projectId, int milestoneId, UpdateProjectMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Milestone>($"projects/{projectId}/milestones/{milestoneId}", request);
        }

        /// <summary>
        /// Updates an existing project variable.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update variable request.</param>
        /// <returns>Newly modified variable.</returns>
        public async Task<Variable> UpdateVariableAsync(ProjectId projectId, UpdateProjectVariableRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Variable>($"projects/{projectId}/variables/{request.Key}", request);
        }

        /// <summary>
        /// Deletes project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task DeleteAsync(ProjectId projectId) =>
            await _httpFacade.Delete($"projects/{projectId}");

        /// <summary>
        /// Deletes project labels.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="name">Name of the label.</param>
        public async Task DeleteLabelAsync(ProjectId projectId, string name) =>
            await _httpFacade.Delete($"projects/{projectId}/labels?name={name}");

        /// <summary>
        /// Deletes project milestone. Only for user with developer access to the project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="milestoneId">The ID of the project�s milestone.</param>
        public async Task DeleteMilestoneAsync(ProjectId projectId, int milestoneId) =>
            await _httpFacade.Delete($"projects/{projectId}/milestones/{milestoneId}");

        /// <summary>
        /// Deletes project variable
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="key">The Key ID of the variable.</param>
        public async Task DeleteVariableAsync(ProjectId projectId, string key) =>
            await _httpFacade.Delete($"projects/{projectId}/variables/{key}");

        /// <summary>
        /// Archive project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task ArchiveAsync(ProjectId projectId) =>
            await _httpFacade.Post($"projects/{projectId}/archive");

        /// <summary>
        /// Unarchive project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task UnArchiveAsync(ProjectId projectId) =>
            await _httpFacade.Post($"projects/{projectId}/unarchive");

        public async Task<Project> Transfer(ProjectId projectId, TransferProjectRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Project>($"projects/{projectId}/transfer", request);
        }

        /// <summary>
        /// Request the export of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        public async Task ExportAsync(ProjectId projectId)
        {
            await _httpFacade.Post($"projects/{projectId}/export");
        }

        /// <summary>
        /// Get status of the export.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>Status of the export</returns>
        public async Task<ExportStatus> GetExportStatusAsync(ProjectId projectId)
        {
            return await _httpFacade.Get<ExportStatus>($"projects/{projectId}/export");
        }

        /// <summary>
        /// Download an exported project if it exists
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="outputPath">The filename that should contain the contents of the download after the download completes</param>
        /// <returns>Status of the export</returns>
        public async Task ExportDownloadAsync(ProjectId projectId, string outputPath)
        {
            await _httpFacade.GetFile($"projects/{projectId}/export/download", outputPath);
        }

        /// <summary>
        /// Request the import of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>Status of the import (including the id of the new project)</returns>
        public async Task<ImportStatus> ImportAsync(ImportProjectRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var parameters = new Dictionary<string, string>();
            foreach (var prop in request.GetType().GetProperties())
            {
                if (prop.GetValue(request) != null && prop.Name != nameof(ImportProjectRequest.File))
                {
                    parameters.Add((prop.GetCustomAttributes(typeof(Newtonsoft.Json.JsonPropertyAttribute), false)[0] as Newtonsoft.Json.JsonPropertyAttribute).PropertyName, prop.GetValue(request).ToString());
                }
            }

            using (var stream = System.IO.File.OpenRead(request.File))
            {
                return await _httpFacade.PostFile<ImportStatus>($"projects/import", parameters, new CreateUploadRequest(stream, request.Path + ".tar.gz"));
            }
        }

        /// <summary>
        /// Get status of the import.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>Status of the import</returns>
        public async Task<ImportStatus> GetImportStatusAsync(ProjectId projectId)
        {
            return await _httpFacade.Get<ImportStatus>($"projects/{projectId}/import");
        }
    }
}
