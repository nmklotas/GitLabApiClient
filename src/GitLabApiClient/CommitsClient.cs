using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Commits.Requests;
using GitLabApiClient.Models.Commits.Responses;

namespace GitLabApiClient
{
    public sealed class CommitsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly CommitQueryBuilder _commitQueryBuilder;

        internal CommitsClient(GitLabHttpFacade httpFacade, CommitQueryBuilder commitQueryBuilder)
        {
            _httpFacade = httpFacade;
            _commitQueryBuilder = commitQueryBuilder;
        }
            
        public async Task<Commit> GetAsync(string projectId, string sha) =>
           await _httpFacade.Get<Commit>(CommitsBaseUrl(projectId) + "/" + sha);

        public async Task<IList<Commit>> GetAsync(string projectId, Action<CommitQueryOptions> options)
        {
            var queryOptions = new CommitQueryOptions(projectId);
            options?.Invoke(queryOptions);

            string url = _commitQueryBuilder.Build(CommitsBaseUrl(projectId), queryOptions);
            return await _httpFacade.GetPagedList<Commit>(url);
        }

        private static string CommitsBaseUrl(string projectId)
        {
            return $"projects/{projectId}/repository/commits";
        }
    }
}
