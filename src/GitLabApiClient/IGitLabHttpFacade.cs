using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Models.Uploads.Requests;
using GitLabApiClient.Models.Uploads.Responses;
using GitLabApiClient.Models.Users.Responses;

namespace GitLabApiClient.Http
{
    internal interface IGitLabHttpFacade
    {
        Task Delete(string uri);
        Task<T> Get<T>(string uri);
        Task<IList<T>> GetPagedList<T>(string uri);
        Task<Session> LoginAsync(string username, string password);
        Task Post(string uri, object data = null);
        Task<T> Post<T>(string uri, object data = null) where T : class;
        Task<Upload> PostFile(string uri, CreateUploadRequest uploadRequest);
        Task Put(string uri, object data);
        Task<T> Put<T>(string uri, object data);
    }
}