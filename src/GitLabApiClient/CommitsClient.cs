using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Tags.Requests;
using GitLabApiClient.Models.Tags.Responses;

namespace GitLabApiClient
{
    public sealed class CommitsClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal CommitsClient(GitLabHttpFacade httpFacade) => _httpFacade = httpFacade;
        public async Task<Commit> GetAsync(string projectId, string sha) =>
           await _httpFacade.Get<Commit>(CommitsBaseUrl(projectId) + "/{sha}");

        public static string CommitsBaseUrl(string projectId)
        {
            return $"projects/{projectId}/repository/commits";
        }
    }
}
