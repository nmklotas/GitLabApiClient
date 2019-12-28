using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http.Serialization;
using GitLabApiClient.Models.Oauth.Requests;
using GitLabApiClient.Models.Oauth.Responses;
using GitLabApiClient.Models.Uploads.Requests;
using GitLabApiClient.Models.Uploads.Responses;

namespace GitLabApiClient.Internal.Http
{
    internal sealed class GitLabHttpFacade
    {
        private const string PrivateToken = "PRIVATE-TOKEN";

        private readonly object _locker = new object();
        private readonly HttpClient _httpClient;
        private GitLabApiRequestor _requestor;
        private GitLabApiPagedRequestor _pagedRequestor;

        private GitLabHttpFacade(string hostUrl, RequestsJsonSerializer jsonSerializer)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(hostUrl) };

            Setup(jsonSerializer);
        }

        public GitLabHttpFacade(string hostUrl, RequestsJsonSerializer jsonSerializer, string authenticationToken = "") :
            this(hostUrl, jsonSerializer)
        {
            switch (authenticationToken.Length)
            {
                case 0:
                    break;
                case 20:
                    _httpClient.DefaultRequestHeaders.Add(PrivateToken, authenticationToken);
                    break;
                case 64:
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
                    break;
                default:
                    throw new ArgumentException("Unsupported authentication token provide, please private an oauth or private token");
            }
        }

        public GitLabHttpFacade(RequestsJsonSerializer jsonSerializer, HttpClient httpClient)
        {
            _httpClient = httpClient;
            Setup(jsonSerializer);
        }

        private void Setup(RequestsJsonSerializer jsonSerializer)
        {
            // allow tls 1.1 and 1.2
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            // ReSharper disable once InconsistentlySynchronizedField
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

        public Task<Upload> PostFile(string uri, CreateUploadRequest uploadRequest) =>
            _requestor.PostFile(uri, uploadRequest);

        public Task<T> Put<T>(string uri, object data) =>
            _requestor.Put<T>(uri, data);

        public Task Put(string uri, object data) =>
            _requestor.Put(uri, data);

        public Task Delete(string uri) =>
            _requestor.Delete(uri);

        public async Task<AccessTokenResponse> LoginAsync(AccessTokenRequest accessTokenRequest)
        {
            // ReSharper disable once InconsistentlySynchronizedField
            string url = $"{_httpClient.BaseAddress.GetLeftPart(UriPartial.Authority)}/oauth/token";
            var accessTokenResponse = await _requestor.Post<AccessTokenResponse>(url, accessTokenRequest);

            lock (_locker)
            {
                if (_httpClient.DefaultRequestHeaders.Contains(PrivateToken))
                    _httpClient.DefaultRequestHeaders.Remove(PrivateToken);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenResponse.AccessToken);
            }

            return accessTokenResponse;
        }
    }
}
