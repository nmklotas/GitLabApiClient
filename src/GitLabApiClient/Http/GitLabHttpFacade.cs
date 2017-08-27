using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Models.Users;

namespace GitLabApiClient.Http
{
    internal class GitLabHttpFacade
    {
        private const string PrivateToken = "PRIVATE-TOKEN";

        private readonly object _locker = new object();
        private readonly HttpClient _httpClient;
        private readonly GitLabApiRequestor _requestor;
        private readonly GitLabApiPagedRequestor _pagedRequestor;

        public GitLabHttpFacade(string hostUrl, string authenticationToken = "")
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(hostUrl)
            };

            _httpClient.DefaultRequestHeaders.Add(PrivateToken, authenticationToken);
            _requestor = new GitLabApiRequestor(_httpClient);
            _pagedRequestor = new GitLabApiPagedRequestor(_requestor);
        }

        public Task<IList<T>> GetPagedList<T>(string uri) =>
            _pagedRequestor.GetPagedList<T>(uri);

        public Task<T> Get<T>(string uri) =>
            _requestor.Get<T>(uri);

        public Task<T> Post<T>(string uri, object data) where T : class =>
            _requestor.Post<T>(uri, data);

        public Task<T> Put<T>(string uri, object data) =>
            _requestor.Put<T>(uri, data);

        public Task Delete(string uri) =>
            _requestor.Delete(uri);

        public async Task<Session> LoginAsync(string username, string password)
        {
            var session = await _requestor.Post<Session>($"/session?login={username}&password={password}");

            lock (_locker)
            {
                if (_httpClient.DefaultRequestHeaders.Contains(PrivateToken))
                    _httpClient.DefaultRequestHeaders.Remove(PrivateToken);

                _httpClient.DefaultRequestHeaders.Add(PrivateToken, session.PrivateToken);
            }

            return session;
        }
    }
}
