using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Trees.Requests;
using GitLabApiClient.Models.Trees.Responses;

namespace GitLabApiClient
{
    public sealed class TreesClient : ITreesClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly TreeQueryBuilder _treeQueryBuilder;

        internal TreesClient(GitLabHttpFacade httpFacade, TreeQueryBuilder treeQueryBuilder)
        {
            _httpFacade = httpFacade;
            _treeQueryBuilder = treeQueryBuilder;
        }

        public async Task<IList<Tree>> GetAsync(ProjectId projectId, Action<TreeQueryOptions> options = null)
        {
            var queryOptions = new TreeQueryOptions();
            options?.Invoke(queryOptions);

            string url = _treeQueryBuilder.Build($"projects/{projectId}/repository/tree", queryOptions);
            return await _httpFacade.GetPagedList<Tree>(url);
        }
    }
}
