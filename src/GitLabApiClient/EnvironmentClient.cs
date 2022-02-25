using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Environments.Requests;
using GitLabApiClient.Models.Environments.Responses;
using Environment = GitLabApiClient.Models.Environments.Responses.Environment;

namespace GitLabApiClient
{
    public sealed class EnvironmentClient : IEnvironmentsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly EnvironmentsQueryBuilder _environmentQueryBuilder;

        internal EnvironmentClient(
            GitLabHttpFacade httpFacade,
            EnvironmentsQueryBuilder environmentQueryBuilder)
        {
            _httpFacade = httpFacade;
            _environmentQueryBuilder = environmentQueryBuilder;
        }

        /// <summary>
        /// Retrieves an environment by its name
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="environmentId">The ID of the environment to retrieve.</param>
        /// <returns></returns>
        public async Task<Environment> GetAsync(ProjectId projectId, int environmentId) =>
            await _httpFacade.Get<Environment>($"projects/{projectId}/environments/{environmentId}");

        /// <summary>
        /// Get a list of environments
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options <see cref="EnvironmentsQueryOptions"/>.</param>
        /// <returns></returns>
        public async Task<IList<Environment>> GetAsync(ProjectId projectId, Action<EnvironmentsQueryOptions> options = null)
        {
            var queryOptions = new EnvironmentsQueryOptions();
            options?.Invoke(queryOptions);

            string url = _environmentQueryBuilder.Build($"projects/{projectId}/environments", queryOptions);
            return await _httpFacade.GetPagedList<Environment>(url);
        }

        /// <summary>
        /// Create an environment
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create environment request.</param>
        /// <returns></returns>
        public async Task<Environment> CreateAsync(ProjectId projectId, CreateEnvironmentRequest request) =>
            await _httpFacade.Post<Environment>($"projects/{projectId}/environments", request);

        /// <summary>
        /// Update an environment
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update environment request</param>
        /// <returns></returns>
        public async Task<Environment> UpdateAsync(ProjectId projectId, UpdateEnvironmentRequest request) =>
            await _httpFacade.Put<Environment>($"projects/{projectId}/environments/{request.EnvironmentId}", request);

        /// <summary>
        /// Stop an environment
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The ID of the environment to stop.</param>
        /// <returns></returns>
        public async Task StopAsync(ProjectId projectId, int environmentId) =>
            await _httpFacade.Post($"projects/{projectId}/environments/{environmentId}/stop");

        /// <summary>
        /// Delete an environment
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The ID of the environment to delete.</param>
        /// <returns></returns>
        public async Task DeleteAsync(ProjectId projectId, int environmentId) =>
            await _httpFacade.Delete($"projects/{projectId}/environments/{environmentId}");
    }
}
