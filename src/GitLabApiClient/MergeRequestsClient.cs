using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Merges;
using GitLabApiClient.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient
{
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

        public async Task<IList<MergeRequest>> GetAsync(int projectId, Action<ProjectMergeRequestsQueryOptions> options = null)
        {
            var projectMergeRequestOptions = new ProjectMergeRequestsQueryOptions(projectId);
            options?.Invoke(projectMergeRequestOptions);

            string query = _mergeRequestsQueryBuilder.
                Build($"/projects/{projectId}/merge_requests", projectMergeRequestOptions);

            return await _httpFacade.GetPagedList<MergeRequest>(query);
        }

        public async Task<IList<MergeRequest>> GetAsync(Action<MergeRequestsQueryOptions> options = null)
        {
            var projectMergeRequestOptions = new MergeRequestsQueryOptions();
            options?.Invoke(projectMergeRequestOptions);

            string query = _projectMergeRequestsQueryBuilder.
                Build("/merge_requests", projectMergeRequestOptions);

            return await _httpFacade.GetPagedList<MergeRequest>(query);
        }

        public async Task<MergeRequest> CreateAsync(CreateMergeRequest request) => 
            await _httpFacade.Post<MergeRequest>($"/projects/{request.ProjectId}/merge_requests", request);

        public async Task<MergeRequest> UpdateAsync(UpdateMergeRequest request) => 
            await _httpFacade.Put<MergeRequest>($"/projects/{request.ProjectId}/merge_requests/{request.MergeRequestId}", request);

        public async Task<MergeRequest> AcceptAsync(int projectId, int mergeRequestId, string mergeCommitMessage)
        {
            Guard.NotEmpty(mergeCommitMessage, nameof(mergeCommitMessage));

            var commitMessage = new MergeCommitMessage
            {
                Message = mergeCommitMessage
            };

            return await _httpFacade.Put<MergeRequest>(
                $"/projects/{projectId}/merge_requests/{mergeRequestId}/merge", commitMessage);
        }

        public async Task DeleteAsync(int projectId, int mergeRequestId) =>
            await _httpFacade.Delete($"/projects/{projectId}/merge_requests/{mergeRequestId}");

        private class MergeCommitMessage
        {
            [JsonProperty("merge_commit_message")]
            public string Message { get; set; }
        }
    }
}
