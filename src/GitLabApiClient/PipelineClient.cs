using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Job.Requests;
using GitLabApiClient.Models.Job.Responses;
using GitLabApiClient.Models.Pipelines.Requests;
using GitLabApiClient.Models.Pipelines.Responses;

namespace GitLabApiClient
{
    public sealed class PipelineClient : IPipelineClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly PipelineQueryBuilder _queryBuilder;
        private readonly JobQueryBuilder _jobQueryBuilder;

        internal PipelineClient(GitLabHttpFacade httpFacade, PipelineQueryBuilder queryBuilder, JobQueryBuilder jobQueryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
            _jobQueryBuilder = jobQueryBuilder;
        }

        public async Task<PipelineDetail> GetAsync(ProjectId projectId, int pipelineId) =>
            await _httpFacade.Get<PipelineDetail>($"projects/{projectId}/pipelines/{pipelineId}");

        public async Task<IList<Pipeline>> GetAsync(ProjectId projectId, Action<PipelineQueryOptions> options = null)
        {
            var queryOptions = new PipelineQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build($"projects/{projectId}/pipelines", queryOptions);
            return await _httpFacade.GetPagedList<Pipeline>(url);
        }

        public async Task<IList<PipelineVariable>> GetVariablesAsync(ProjectId projectId, int pipelineId) =>
            await _httpFacade.Get<IList<PipelineVariable>>($"projects/{projectId}/pipelines/{pipelineId}/variables");

        public async Task<IList<Job>> GetJobsAsync(ProjectId projectId, int pipelineId, Action<JobQueryOptions> options = null)
        {
            var queryOptions = new JobQueryOptions();
            options?.Invoke(queryOptions);

            var url = _jobQueryBuilder.Build($"projects/{projectId}/pipelines/{pipelineId}/jobs", queryOptions);
            return await _httpFacade.GetPagedList<Job>(url);
        }

        public async Task<PipelineDetail> CreateAsync(ProjectId projectId, CreatePipelineRequest request) =>
            await _httpFacade.Post<PipelineDetail>($"projects/{projectId}/pipeline", request);

        public async Task DeleteAsync(ProjectId projectId, int pipelineId) =>
            await _httpFacade.Delete($"projects/{projectId}/pipelines/{pipelineId}");

        public async Task<PipelineDetail> CancelAsync(ProjectId projectId, int pipelineId) =>
            await _httpFacade.Post<PipelineDetail>(
                $"projects/{projectId}/pipelines/{pipelineId}/cancel");

        public async Task<PipelineDetail> RetryAsync(ProjectId projectId, int pipelineId) =>
            await _httpFacade.Post<PipelineDetail>($"projects/{projectId}/pipelines/{pipelineId}/retry");
    }
}
