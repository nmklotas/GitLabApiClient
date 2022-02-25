using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Environments.Requests;
using GitLabApiClient.Models.Environments.Responses;
using Environment = GitLabApiClient.Models.Environments.Responses.Environment;

namespace GitLabApiClient
{
    public interface IEnvironmentsClient
    {
        /// <summary>
        /// Get a list of environments
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="environmentId">The ID of the environment to retrieve.</param>
        /// <returns></returns>
        Task<Environment> GetAsync(ProjectId projectId, int environmentId);

        /// <summary>
        /// Get a list of environments
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options <see cref="EnvironmentsQueryOptions"/>.</param>
        /// <returns></returns>
        Task<IList<Environment>> GetAsync(ProjectId projectId, Action<EnvironmentsQueryOptions> options = null);

        /// <summary>
        /// Create an environment
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create environment request.</param>
        /// <returns></returns>
        Task<Environment> CreateAsync(ProjectId projectId, CreateEnvironmentRequest request);

        /// <summary>
        /// Update an environment
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update environment request</param>
        /// <returns></returns>
        Task<Environment> UpdateAsync(ProjectId projectId, UpdateEnvironmentRequest request);

        /// <summary>
        /// Stop an environment
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="environmentId">The ID of the environment to stop.</param>
        /// <returns></returns>
        Task StopAsync(ProjectId projectId, int environmentId);

        /// <summary>
        /// Delete an environment
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="environmentId">The ID of the environment to delete.</param>
        /// <returns></returns>
        Task DeleteAsync(ProjectId projectId, int environmentId);
    }
}
