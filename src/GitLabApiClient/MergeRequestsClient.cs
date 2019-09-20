using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.MergeRequests.Requests;
using GitLabApiClient.Models.MergeRequests.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to retrieve, modify, accept, create merge requests.
    /// Every API call to merge requests must be authenticated.
    /// <exception cref="GitLabException">Thrown if request to GitLab API does not indicate success</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class MergeRequestsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly MergeRequestsQueryBuilder _mergeRequestsQueryBuilder;
        private readonly ProjectMergeRequestsQueryBuilder _projectMergeRequestsQueryBuilder;

        internal MergeRequestsClient(
            GitLabHttpFacade httpFacade,
            MergeRequestsQueryBuilder mergeRequestsQueryBuilder,
            ProjectMergeRequestsQueryBuilder projectMergeRequestsQueryBuilder)
        {
            _httpFacade = httpFacade;
            _mergeRequestsQueryBuilder = mergeRequestsQueryBuilder;
            _projectMergeRequestsQueryBuilder = projectMergeRequestsQueryBuilder;
        }

        /// <summary>
        /// Retrieves merge request from a project.
        /// By default returns opened merged requests created by anyone.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="options">Merge requests retrieval options.</param>
        /// <returns>Merge requests satisfying options.</returns>
        public async Task<IList<MergeRequest>> GetAsync(int projectId, Action<ProjectMergeRequestsQueryOptions> options = null)
        {
            var projectMergeRequestOptions = new ProjectMergeRequestsQueryOptions();
            options?.Invoke(projectMergeRequestOptions);

            string query = _projectMergeRequestsQueryBuilder.
                Build($"projects/{projectId}/merge_requests", projectMergeRequestOptions);

            return await _httpFacade.GetPagedList<MergeRequest>(query);
        }

        /// <summary>
        /// Retrieves merge request from all projects the authenticated user has access to.
        /// By default returns opened merged requests created by anyone.
        /// </summary>
        /// <param name="options">Merge requests retrieval options.</param>
        /// <returns>Merge requests satisfying options.</returns>
        public async Task<IList<MergeRequest>> GetAsync(Action<MergeRequestsQueryOptions> options = null)
        {
            var mergeRequestOptions = new MergeRequestsQueryOptions();
            options?.Invoke(mergeRequestOptions);

            string query = _mergeRequestsQueryBuilder.
                Build("merge_requests", mergeRequestOptions);

            return await _httpFacade.GetPagedList<MergeRequest>(query);
        }

        /// <summary>
        /// Creates merge request.
        /// </summary>
        /// <returns>The newly created merge request.</returns>
        public async Task<MergeRequest> CreateAsync(CreateMergeRequest request) =>
            await _httpFacade.Post<MergeRequest>($"projects/{request.ProjectId}/merge_requests", request);

        /// <summary>
        /// Updates merge request.
        /// </summary>
        /// <returns>The updated merge request.</returns>
        public async Task<MergeRequest> UpdateAsync(UpdateMergeRequest request) =>
            await _httpFacade.Put<MergeRequest>($"projects/{request.ProjectId}/merge_requests/{request.MergeRequestId}", request);

        /// <summary>
        /// Accepts merge request.
        /// </summary>
        /// <returns>The accepted merge request.</returns>
        public async Task<MergeRequest> AcceptAsync(AcceptMergeRequest request)
        {
            return await _httpFacade.Put<MergeRequest>(
                $"projects/{request.ProjectId}/merge_requests/{request.MergeRequestId}/merge", request);
        }

        /// <summary>
        /// Deletes merge request.
        /// </summary>
        public async Task DeleteAsync(int projectId, int mergeRequestId) =>
            await _httpFacade.Delete($"projects/{projectId}/merge_requests/{mergeRequestId}");
    }
}
