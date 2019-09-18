using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Commits.Responses;

namespace GitLabApiClient
{
    public sealed class CommitsClient
    {
        private readonly IGitLabHttpFacade _httpFacade;

        internal CommitsClient(IGitLabHttpFacade httpFacade) => 
            _httpFacade = httpFacade;
        public async Task<Commit> GetAsync(string projectId, string sha) =>
           await _httpFacade.Get<Commit>(CommitsBaseUrl(projectId) + "/" + sha);

        private static string CommitsBaseUrl(string projectId)
        {
            return $"projects/{projectId}/repository/commits";
        }
    }
}
