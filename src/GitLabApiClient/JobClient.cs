using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Job.Responses;

namespace GitLabApiClient
{
    public sealed class JobClient : IJobClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal JobClient(GitLabHttpFacade httpFacade) => _httpFacade = httpFacade;

        public async Task<Job> GetAsync(ProjectId projectId, int jobId) =>
            await _httpFacade.Get<Job>($"projects/{projectId}/jobs/{jobId}");

        public async Task<Job> RetryAsync(ProjectId projectId, int jobId) =>
            await _httpFacade.Post<Job>($"projects/{projectId}/jobs/{jobId}/retry");

        public async Task<Job> PlayAsync(ProjectId projectId, int jobId) =>
            await _httpFacade.Post<Job>($"projects/{projectId}/jobs/{jobId}/play");

        public async Task<Job> CancelAsync(ProjectId projectId, int jobId) =>
            await _httpFacade.Post<Job>($"projects/{projectId}/jobs/{jobId}/cancel");

        public async Task<Job> EraseAsync(ProjectId projectId, int jobId) =>
            await _httpFacade.Post<Job>($"projects/{projectId}/jobs/{jobId}/erase");
    }
}
