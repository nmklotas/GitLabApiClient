using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Wikis.Requests;
using GitLabApiClient.Models.Wikis.Responses;

namespace GitLabApiClient
{
    public sealed class WikisClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly WikisQueryBuilder _queryBuilder;

        internal WikisClient(GitLabHttpFacade httpFacade, WikisQueryBuilder queryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
        }

        public async Task<IList<Wiki>> GetAllAsync(ProjectId projectId, Action<WikisQueryOptions> options = null)
        {
            var queryOptions = new WikisQueryOptions();
            options?.Invoke(queryOptions);
            string path = $"projects/{projectId}/wikis";

            string url = _queryBuilder.Build(path, queryOptions);

            return await _httpFacade.GetPagedList<Wiki>(url);
        }

        public async Task<Wiki> GetAsync(ProjectId projectId, string slug)
        {
            return await _httpFacade.Get<Wiki>($"projects/{projectId}/wikis/{slug}");
        }

        public async Task<Wiki> CreateAsync(ProjectId projectId, CreateWikiRequest request)
        {
            return await _httpFacade.Post<Wiki>($"projects/{projectId}/wikis", request);
        }

        public async Task<Wiki> UpdateAsync(ProjectId projectId, string slug, UpdateWikiRequest request)
        {
            var result = await _httpFacade.Put<Wiki>($"projects/{projectId}/wikis/{slug}", request);
            return result;
        }
    }
}
