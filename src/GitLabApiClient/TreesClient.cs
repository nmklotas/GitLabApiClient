using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Trees.Requests;
using GitLabApiClient.Models.Trees.Responses;

namespace GitLabApiClient
{
    public sealed class TreesClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly TreeQueryBuilder _treeQueryBuilder;

        internal TreesClient(GitLabHttpFacade httpFacade, TreeQueryBuilder treeQueryBuilder)
        {
            _httpFacade = httpFacade;
            _treeQueryBuilder = treeQueryBuilder;
        }
        public async Task<IList<Tree>> GetAsync(string projectId, string branch, string path, Action<TreeQueryOptions> options)
        {
            var queryOptions = new TreeQueryOptions(branch, path);
            options?.Invoke(queryOptions);

            string url = _treeQueryBuilder.Build(TreeBaseUrl(projectId), queryOptions);
            return await _httpFacade.GetPagedList<Tree>(url);
        }

        private static string TreeBaseUrl(string projectId)
        {
            return $"projects/{projectId}/repository/tree";
        }
    }
}
