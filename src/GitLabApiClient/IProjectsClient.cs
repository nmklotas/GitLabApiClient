using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Job.Requests;
using GitLabApiClient.Models.Job.Responses;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Runners.Responses;
using GitLabApiClient.Models.Users.Responses;
using GitLabApiClient.Models.Variables.Request;
using GitLabApiClient.Models.Variables.Response;

namespace GitLabApiClient
{
    public interface IProjectsClient
    {
        /// <summary>
        /// Retrieves project by its id, path or <see cref="Project"/>.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        Task<Project> GetAsync(ProjectId projectId);

        /// <summary>
        /// Get a list of visible projects for authenticated user.
        /// When accessed without authentication, only public projects are returned.
        /// </summary>
        /// <param name="options">Query options.</param>
        Task<IList<Project>> GetAsync(Action<ProjectQueryOptions> options = null);

        /// <summary>
        /// Get the users list of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        Task<IList<User>> GetUsersAsync(ProjectId projectId);

        /// <summary>
        /// Retrieves project variables by its id.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        Task<IList<Variable>> GetVariablesAsync(ProjectId projectId);

        /// <summary>
        /// Get the labels list of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        Task<IList<Label>> GetLabelsAsync(ProjectId projectId);

        /// <summary>
        /// Get the milestone list of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options.</param>
        Task<IList<Milestone>> GetMilestonesAsync(ProjectId projectId, Action<MilestonesQueryOptions> options = null);

        /// <summary>
        /// Get the jobs list of a project
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options.</param>
        Task<IList<Job>> GetJobsAsync(ProjectId projectId, Action<JobQueryOptions> options = null);

        /// <summary>
        /// Retrieves project milestone by its id
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="milestoneId">Id of the milestone.</param>
        Task<Milestone> GetMilestoneAsync(ProjectId projectId, int milestoneId);

        /// <summary>
        /// Get the runners list of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        Task<IList<Runner>> GetRunnersAsync(ProjectId projectId);

        /// <summary>
        /// Creates new project.
        /// </summary>
        /// <param name="request">Create project request.</param>
        /// <returns>Newly created project.</returns>
        Task<Project> CreateAsync(CreateProjectRequest request);

        /// <summary>
        /// Creates new project label.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create label request.</param>
        /// <returns>Newly created label.</returns>
        Task<Label> CreateLabelAsync(ProjectId projectId, CreateProjectLabelRequest request);

        /// <summary>
        /// Creates new project variable.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create variable request.</param>
        /// <returns>Newly created variable.</returns>
        Task<Variable> CreateVariableAsync(ProjectId projectId, CreateVariableRequest request);

        /// <summary>
        /// Creates new project milestone.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create milestone request.</param>
        /// <returns>Newly created milestone.</returns>
        Task<Milestone> CreateMilestoneAsync(ProjectId projectId, CreateProjectMilestoneRequest request);

        /// <summary>
        /// Updates existing project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update project request.</param>
        /// <returns>Newly modified project.</returns>
        Task<Project> UpdateAsync(ProjectId projectId, UpdateProjectRequest request);

        /// <summary>
        /// Updates an existing label with new name or new color. At least one parameter is required, to update the label.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update label request.</param>
        /// <returns>Newly modified label.</returns>
        Task<Label> UpdateLabelAsync(ProjectId projectId, UpdateProjectLabelRequest request);

        /// <summary>
        /// Updates an existing project milestone.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="milestoneId">The ID of a project milestone.</param>
        /// <param name="request">Update milestone request.</param>
        /// <returns>Newly modified milestone.</returns>
        Task<Milestone> UpdateMilestoneAsync(ProjectId projectId, int milestoneId, UpdateProjectMilestoneRequest request);

        /// <summary>
        /// Updates an existing project variable.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update variable request.</param>
        /// <returns>Newly modified variable.</returns>
        Task<Variable> UpdateVariableAsync(ProjectId projectId, UpdateProjectVariableRequest request);

        /// <summary>
        /// Deletes project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        Task DeleteAsync(ProjectId projectId);

        /// <summary>
        /// Deletes project labels.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="name">Name of the label.</param>
        Task DeleteLabelAsync(ProjectId projectId, string name);

        /// <summary>
        /// Deletes project milestone. Only for user with developer access to the project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="milestoneId">The ID of the project�s milestone.</param>
        Task DeleteMilestoneAsync(ProjectId projectId, int milestoneId);

        /// <summary>
        /// Deletes project variable
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="key">The Key ID of the variable.</param>
        Task DeleteVariableAsync(ProjectId projectId, string key);

        /// <summary>
        /// Archive project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        Task ArchiveAsync(ProjectId projectId);

        /// <summary>
        /// Unarchive project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        Task UnArchiveAsync(ProjectId projectId);

        Task<Project> Transfer(ProjectId projectId, TransferProjectRequest request);
        /// <summary>
        /// Request the export of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        Task ExportAsync(ProjectId projectId);

        /// <summary>
        /// Get status of the export.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>Status of the export</returns>
        Task<ExportStatus> GetExportStatusAsync(ProjectId projectId);

        /// <summary>
        /// Download an exported project if it exists
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="outputPath">The filename that should contain the contents of the download after the download completes</param>
        /// <returns>Status of the export</returns>
        Task ExportDownloadAsync(ProjectId projectId, string outputPath);

        /// <summary>
        /// Request the import of a project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>Status of the import (including the id of the new project)</returns>
        Task<ImportStatus> ImportAsync(ImportProjectRequest request);

        /// <summary>
        /// Get status of the import.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>Status of the import</returns>
        Task<ImportStatus> GetImportStatusAsync(ProjectId projectId);
    }
}
