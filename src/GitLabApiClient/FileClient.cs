using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Files.Requests;
using GitLabApiClient.Models.Files.Responses;

namespace GitLabApiClient
{
    public sealed class FilesClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly FileQueryBuilder _fileQueryBuilder;

        internal FilesClient(GitLabHttpFacade httpFacade, FileQueryBuilder fileQueryBuilder)
        {
            _httpFacade = httpFacade;
            _fileQueryBuilder = fileQueryBuilder;
        }
        public async Task<File> GetAsync(ProjectId projectId, string filePath, Action<FileQueryOptions> options = null)
        {
            var queryOptions = new FileQueryOptions();
            options?.Invoke(queryOptions);

            string url = _fileQueryBuilder.Build($"projects/{projectId}/repository/files/{filePath.UrlEncode()}", queryOptions);
            return await _httpFacade.Get<File>(url);
        }
    }
}
