using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Files.Requests;
using GitLabApiClient.Models.Files.Responses;

namespace GitLabApiClient
{
    public sealed class FilesClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly FileQueryBuilder _fileeQueryBuilder;

        internal FilesClient(GitLabHttpFacade httpFacade, FileQueryBuilder fileQueryBuilder)
        {
            _httpFacade = httpFacade;
            _fileeQueryBuilder = fileQueryBuilder;
        }
        public async Task<File> GetAsync(string projectId, string fullpath, Action<FileQueryOptions> options)
        {
            var queryOptions = new FileQueryOptions();
            options?.Invoke(queryOptions);

            string url = _fileeQueryBuilder.Build(TreeBaseUrl(projectId, fullpath), queryOptions);
            return await _httpFacade.Get<File>(url);
        }

        private static string TreeBaseUrl(string projectId, string fullpath)
        {
            return $"projects/{projectId}/repository/files/{HttpUtility.UrlEncode(fullpath)}";
        }
    }
}
