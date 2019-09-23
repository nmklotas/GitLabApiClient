using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Releases.Requests;
using GitLabApiClient.Models.Releases.Responses;

namespace GitLabApiClient
{
    public sealed class ReleaseClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly ReleaseQueryBuilder _releaseQueryBuilder;

        internal ReleaseClient(
            GitLabHttpFacade httpFacade,
            ReleaseQueryBuilder releaseQueryBuilder)
        {
            _httpFacade = httpFacade;
            _releaseQueryBuilder = releaseQueryBuilder;
        }

        /// <summary>
        /// Retrieves a release by its name
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The release, you want to retrieve.</param>
        /// <returns></returns>
        public async Task<Release> GetAsync(object projectId, string tagName) =>
            await _httpFacade.Get<Release>($"{ReleaseBaseUrl(projectId)}/{tagName}");

        /// <summary>
        /// Get a list of releases
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options <see cref="ReleaseQueryOptions"/>.</param>
        /// <returns></returns>
        public async Task<IList<Release>> GetAsync(object projectId, Action<ReleaseQueryOptions> options = null)
        {
            var queryOptions = new ReleaseQueryOptions();
            options?.Invoke(queryOptions);

            string url = _releaseQueryBuilder.Build(ReleaseBaseUrl(projectId), queryOptions);
            return await _httpFacade.GetPagedList<Release>(url);
        }

        /// <summary>
        /// Create a release
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create release request.</param>
        /// <returns></returns>
        public async Task<Release> CreateAsync(object projectId, CreateReleaseRequest request) =>
            await _httpFacade.Post<Release>(ReleaseBaseUrl(projectId), request);

        /// <summary>
        /// Update a release
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Update release request</param>
        /// <returns></returns>
        public async Task<Release> UpdateAsync(object projectId, UpdateReleaseRequest request) =>
            await _httpFacade.Put<Release>(ReleaseBaseUrl(projectId), request);

        /// <summary>
        /// Delete a release
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The tag name of the release, you want to delete.</param>
        /// <returns></returns>
        public async Task DeleteAsync(object projectId, string tagName) =>
            await _httpFacade.Delete($"{ReleaseBaseUrl(projectId)}/{tagName}");

        public static string ReleaseBaseUrl(object projectId) =>
            $"{projectId.ProjectBaseUrl()}/releases";
    }
}
