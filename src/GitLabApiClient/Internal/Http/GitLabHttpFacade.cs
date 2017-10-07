using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http.Serialization;
using GitLabApiClient.Models.Users.Responses;

namespace GitLabApiClient.Internal.Http
{
    internal sealed class GitLabHttpFacade
    {
        private const string PrivateToken = "PRIVATE-TOKEN";

        private readonly object _locker = new object();
        private readonly HttpClient _httpClient;
        private readonly GitLabApiRequestor _requestor;
        private readonly GitLabApiPagedRequestor _pagedRequestor;

        public GitLabHttpFacade(string hostUrl, RequestsJsonSerializer jsonSerializer, string authenticationToken = "")
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(hostUrl)
            };

            _httpClient.DefaultRequestHeaders.Add(PrivateToken, authenticationToken);
            _requestor = new GitLabApiRequestor(_httpClient, jsonSerializer);
            _pagedRequestor = new GitLabApiPagedRequestor(_requestor);
        }

        public Task<IList<T>> GetPagedList<T>(string uri) =>
            _pagedRequestor.GetPagedList<T>(uri);

        public Task<T> Get<T>(string uri) =>
            _requestor.Get<T>(uri);

        public Task<T> Post<T>(string uri, object data = null) where T : class =>
            _requestor.Post<T>(uri, data);

        public Task Post(string uri, object data = null) =>
            _requestor.Post(uri, data);

        public Task<T> Put<T>(string uri, object data) =>
            _requestor.Put<T>(uri, data);

		public Task Put(string uri, object data) =>
			_requestor.Put(uri, data);

		public Task Delete(string uri) =>
            _requestor.Delete(uri);

        public async Task<Session> LoginAsync(string username, string password)
        {
            var session = await _requestor.Post<Session>($"session?login={username}&password={password}");

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
