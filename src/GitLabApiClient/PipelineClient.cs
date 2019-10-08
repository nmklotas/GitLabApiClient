using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Models.Pipelines.Requests;
using GitLabApiClient.Models.Pipelines.Responses;

namespace GitLabApiClient
{
    public sealed class PipelineClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly PipelineQueryBuilder _queryBuilder;

        internal PipelineClient(GitLabHttpFacade httpFacade, PipelineQueryBuilder queryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
        }

        public async Task<PipelineDetail> GetAsync(string projectId, int pipelineId) =>
            await _httpFacade.Get<PipelineDetail>($"projects/{projectId}/pipelines/{pipelineId}");

        public async Task<IList<Pipeline>> GetAsync(string projectId, Action<PipelineQueryOptions> options = null)
        {
            var queryOptions = new PipelineQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build($"projects/{projectId}/pipelines", queryOptions);
            return await _httpFacade.GetPagedList<Pipeline>(url);
        }

        public async Task<IList<PipelineVariable>> GetVariablesAsync(string projectId, int pipelineId) =>
            await _httpFacade.Get<IList<PipelineVariable>>($"projects/{projectId}/pipelines/{pipelineId}/variables");
    }
}
