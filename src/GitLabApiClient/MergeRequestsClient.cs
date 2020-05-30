using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.AwardEmojis.Responses;
using GitLabApiClient.Models.Discussions.Responses;
using GitLabApiClient.Models.MergeRequests.Requests;
using GitLabApiClient.Models.MergeRequests.Responses;
using GitLabApiClient.Models.Notes.Requests;
using GitLabApiClient.Models.Notes.Responses;
using GitLabApiClient.Models.Pipelines.Responses;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to retrieve, modify, accept, create merge requests.
    /// Every API call to merge requests must be authenticated.
    /// <exception cref="GitLabException">Thrown if request to GitLab API does not indicate success</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class MergeRequestsClient : IMergeRequestsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly MergeRequestsQueryBuilder _mergeRequestsQueryBuilder;
        private readonly ProjectMergeRequestsQueryBuilder _projectMergeRequestsQueryBuilder;
        private readonly ProjectMergeRequestsNotesQueryBuilder _projectMergeRequestNotesQueryBuilder;

        internal MergeRequestsClient(
            GitLabHttpFacade httpFacade,
            MergeRequestsQueryBuilder mergeRequestsQueryBuilder,
            ProjectMergeRequestsQueryBuilder projectMergeRequestsQueryBuilder,
            ProjectMergeRequestsNotesQueryBuilder projectMergeRequestNotesQueryBuilder)
        {
            _httpFacade = httpFacade;
            _mergeRequestsQueryBuilder = mergeRequestsQueryBuilder;
            _projectMergeRequestsQueryBuilder = projectMergeRequestsQueryBuilder;
            _projectMergeRequestNotesQueryBuilder = projectMergeRequestNotesQueryBuilder;
        }

        /// <summary>
        /// Retrieves merge request from a project.
        /// By default returns opened merged requests created by anyone.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Merge requests retrieval options.</param>
        /// <returns>Merge requests satisfying options.</returns>
        public async Task<IList<MergeRequest>> GetAsync(ProjectId projectId, Action<ProjectMergeRequestsQueryOptions> options = null)
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
        /// Get single Merge requests by id
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The Internal Merge Request Id.</param>
        /// <returns>Shows information about a single merge request.</returns>
        public async Task<MergeRequest> GetAsync(ProjectId projectId, int mergeRequestId)
            => await _httpFacade.Get<MergeRequest>($"projects/{projectId}/merge_requests/{mergeRequestId}");
        /// <summary>
        /// Creates merge request.
        /// </summary>
        /// <returns>The newly created merge request.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create Merge request.</param>
        public async Task<MergeRequest> CreateAsync(ProjectId projectId, CreateMergeRequest request) =>
            await _httpFacade.Post<MergeRequest>($"projects/{projectId}/merge_requests", request);

        /// <summary>
        /// Updates merge request.
        /// </summary>
        /// <returns>The updated merge request.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The Internal Merge Request Id.</param>
        /// <param name="request">Update Merge request.</param>
        public async Task<MergeRequest> UpdateAsync(ProjectId projectId, int mergeRequestId, UpdateMergeRequest request) =>
            await _httpFacade.Put<MergeRequest>($"projects/{projectId}/merge_requests/{mergeRequestId}", request);

        /// <summary>
        /// Accepts merge request.
        /// </summary>
        /// <returns>The accepted merge request.</returns>
        /// /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The Internal Merge Request Id.</param>
        /// <param name="request">Accept Merge request.</param>
        public async Task<MergeRequest> AcceptAsync(ProjectId projectId, int mergeRequestId, AcceptMergeRequest request)
        {
            return await _httpFacade.Put<MergeRequest>(
                $"projects/{projectId}/merge_requests/{mergeRequestId}/merge", request);
        }

        /// <summary>
        /// Deletes merge request.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The Internal Merge Request Id.</param>
        public async Task DeleteAsync(ProjectId projectId, int mergeRequestId) =>
            await _httpFacade.Delete($"projects/{projectId}/merge_requests/{mergeRequestId}");

        /// <summary>
        /// Creates a new note (comment) to a single Merge Request.
        /// </summary>
        /// <returns>The newly created issue note.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The IID of an Merge Request.</param>
        /// <param name="request">Create Merge Request note request.</param>
        public async Task<Note> CreateNoteAsync(ProjectId projectId, int mergeRequestId, CreateMergeRequestNoteRequest request) =>
            await _httpFacade.Post<Note>($"projects/{projectId}/merge_requests/{mergeRequestId}/notes", request);

        /// <summary>
        /// Retrieves notes (comments) of a merge request.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestIid">Iid of the merge request.</param>
        /// <param name="options">MergeRequestNotes retrieval options.</param>
        /// <returns>Merge requests satisfying options.</returns>
        public async Task<IList<Note>> GetNotesAsync(ProjectId projectId, int mergeRequestIid, Action<MergeRequestNotesQueryOptions> options = null)
        {
            var queryOptions = new MergeRequestNotesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _projectMergeRequestNotesQueryBuilder.Build($"projects/{projectId}/merge_requests/{mergeRequestIid}/notes", queryOptions);
            return await _httpFacade.GetPagedList<Note>(url);
        }

        /// <summary>
        /// List erge request pipelines
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The Internal Merge Request Id.</param>
        /// <returns>Get a list of merge request pipelines.</returns>
        public async Task<IList<Pipeline>> GetPipelinesAsync(ProjectId projectId, int mergeRequestId)
            => await _httpFacade.Get<List<Pipeline>>($"projects/{projectId}/merge_requests/{mergeRequestId}/pipelines");

        /// <summary>
        /// Retrieves discussions of a merge request.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestIid">Iid of the merge request.</param>
        public async Task<IList<Discussion>> GetDiscussionsAsync(ProjectId projectId, int mergeRequestIid) =>
            await _httpFacade.GetPagedList<Discussion>($"projects/{projectId}/merge_requests/{mergeRequestIid}/discussions");

        /// <summary>
        /// Retrieves a list of all award emoji for a specified merge request.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestIid">The Internal Merge Request Id.</param>
        public async Task<IList<AwardEmoji>> GetAwardEmojisAsync(ProjectId projectId, int mergeRequestIid) =>
            await _httpFacade.GetPagedList<AwardEmoji>($"projects/{projectId}/merge_requests/{mergeRequestIid}/award_emoji");

    }
}
