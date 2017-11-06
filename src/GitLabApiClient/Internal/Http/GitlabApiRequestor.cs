using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http.Serialization;

namespace GitLabApiClient.Internal.Http
{
    internal sealed class GitLabApiRequestor
    {
        private readonly HttpClient _client;
        private readonly RequestsJsonSerializer _jsonSerializer;

        public GitLabApiRequestor(HttpClient client, RequestsJsonSerializer jsonSerializer)
        {
            _client = client;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<T> Get<T>(string url, CancellationToken cancellationToken)
        {
            var responseMessage = await _client.GetAsync(url, cancellationToken);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }
        
        public async Task<T> Get<T>(string url)
        {
            return await Get<T>(url, CancellationToken.None);
        }
        
        public async Task<T> Post<T>(string url, object data, CancellationToken cancellationToken)
        {
            StringContent content = SerializeToString(data);
            var responseMessage = await _client.PostAsync(url, content, cancellationToken);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }
        
        public async Task<T> Post<T>(string url, object data)
        {
            return await Post<T>(url, data, CancellationToken.None);
        }
        
        public async Task<T> Post<T>(string url)
        {
            return await Post<T>(url, null, CancellationToken.None);
        }

        
        public async Task Post(string url, object data, CancellationToken cancellationToken)
        {
            StringContent content = SerializeToString(data);
            var responseMessage = await _client.PostAsync(url, content, cancellationToken);
            await EnsureSuccessStatusCode(responseMessage);
        }
        
        public async Task Post(string url, object data)
        {
            await Post(url, data, CancellationToken.None);
        }
        
		public async Task Post(string url)
		{
		    await Post(url, null, CancellationToken.None);
		}
        
        public async Task<T> Put<T>(string url, object data, CancellationToken cancellationToken)
        {
            StringContent content = SerializeToString(data);
            var responseMessage = await _client.PutAsync(url, content, cancellationToken);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }

        public async Task<T> Put<T>(string url, object data)
        {
            return await Put<T>(url, data, CancellationToken.None);
        }

        
        public async Task Put(string url, object data, CancellationToken cancellationToken)
        {
            StringContent content = SerializeToString(data);
            var responseMessage = await _client.PutAsync(url, content, cancellationToken);
            await EnsureSuccessStatusCode(responseMessage);
        }

		public async Task Put(string url, object data)
		{
		    await Put(url, data, CancellationToken.None);
		}
        
        public async Task Delete(string url, CancellationToken cancellationToken)
        {
            var responseMessage = await _client.DeleteAsync(url, cancellationToken);
            await EnsureSuccessStatusCode(responseMessage);
        }

        public async Task Delete(string url)
        {
            await Delete(url, CancellationToken.None);
        }
        
        public async Task<Tuple<T, HttpResponseHeaders>> GetWithHeaders<T>(string url, CancellationToken cancellationToken)
        {
            var responseMessage = await _client.GetAsync(url, cancellationToken);
            await EnsureSuccessStatusCode(responseMessage);
            return Tuple.Create(await ReadResponse<T>(responseMessage), responseMessage.Headers);
        }

        public async Task<Tuple<T, HttpResponseHeaders>> GetWithHeaders<T>(string url)
        {
            return await GetWithHeaders<T>(url, CancellationToken.None);
        }

        private static async Task EnsureSuccessStatusCode(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
                return;

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            throw new GitLabException(responseMessage.StatusCode, errorResponse ?? "");
        }

        private async Task<T> ReadResponse<T>(HttpResponseMessage responseMessage)
        {
            string response = await responseMessage.Content.ReadAsStringAsync();
            var result = _jsonSerializer.Deserialize<T>(response);
            return result;
        }

        private StringContent SerializeToString(object data)
        {
            string serializedObject = _jsonSerializer.Serialize(data);

            var content = data != null ?
                new StringContent(serializedObject) :
                new StringContent(string.Empty);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}
