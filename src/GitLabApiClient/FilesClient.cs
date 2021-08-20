using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Files.Responses;

namespace GitLabApiClient
{
    public sealed class FilesClient : IFilesClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal FilesClient(GitLabHttpFacade httpFacade) => _httpFacade = httpFacade;

        public async Task<File> GetAsync(ProjectId projectId, string filePath, string reference = "master")
        {
            return await _httpFacade.Get<File>($"projects/{projectId}/repository/files/{filePath.UrlEncode()}?ref={reference}");
        }
    }
}
