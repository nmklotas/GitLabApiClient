using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Merges;
using Newtonsoft.Json;

namespace GitLabApiClient
{
    public class MergeRequestsClient
    {
        private readonly GitlabHttpFacade _httpFacade;

        internal MergeRequestsClient(GitlabHttpFacade httpFacade) => 
            _httpFacade = httpFacade;

        public async Task<IList<MergeRequest>> GetAsync(int projectId) => 
            await _httpFacade.GetAll<MergeRequest>($"/projects/{projectId}/merge_requests");

        public async Task<IList<MergeRequest>> GetAsync(int projectId, MergeRequestState state) => 
            await _httpFacade.GetAll<MergeRequest>($"/projects/{projectId}/merge_requests?state={state.ToString().ToLowerInvariant()}");

        public async Task<MergeRequest> CreateAsync(CreateMergeRequest request) => 
            await _httpFacade.Post<MergeRequest>($"/projects/{request.ProjectId}/merge_requests", request);

        public async Task<MergeRequest> UpdateAsync(EditMergeRequest request) => 
            await _httpFacade.Put<MergeRequest>($"/projects/{request.ProjectId}/merge_requests/{request.MergeRequestId}", request);

        public async Task<MergeRequest> AcceptAsync(int projectId, int mergeRequestId, string message) => 
            await _httpFacade.Put<MergeRequest>($"/projects/{projectId}/merge_requests/{mergeRequestId}/merge", 
                new MergeCommitMessage
                {
                    Message = message
                });

        public async Task DeleteAsync(int projectId, int mergeRequestId) =>
            await _httpFacade.Delete($"/projects/{projectId}/merge_requests/{mergeRequestId}");

        private class MergeCommitMessage
        {
            [JsonProperty("merge_commit_message")]
            public string Message { get; set; }
        }
    }
}
