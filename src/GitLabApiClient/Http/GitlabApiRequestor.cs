using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitLabApiClient.Http.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GitLabApiClient.Http
{
    internal sealed class GitLabApiRequestor
    {
        private readonly HttpClient _client;

        public GitLabApiRequestor(HttpClient client)
        {
            _client = client;

            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new EmptyCollectionContractResolver()
                };

                settings.Converters.Add(new StringEnumConverter());
                return settings;
            };
        }

        public async Task<T> Get<T>(string url)
        {
            var responseMessage = await _client.GetAsync(url);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }

        public async Task<T> Post<T>(string url, object data = null)
        {
            StringContent content = SerializeToString(data, true);
            var responseMessage = await _client.PostAsync(url, content);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }

        public async Task<T> Put<T>(string url, object data)
        {
            StringContent content = SerializeToString(data, false);
            var responseMessage = await _client.PutAsync(url, content);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }

        public async Task Delete(string url)
        {
            var responseMessage = await _client.DeleteAsync(url);
            await EnsureSuccessStatusCode(responseMessage);
        }

        public async Task<Tuple<T, HttpResponseHeaders>> GetWithHeaders<T>(string url)
        {
            var responseMessage = await _client.GetAsync(url);
            await EnsureSuccessStatusCode(responseMessage);
            return Tuple.Create(await ReadResponse<T>(responseMessage), responseMessage.Headers);
        }

        private static async Task EnsureSuccessStatusCode(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
                return;

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            throw new GitLabException(responseMessage.StatusCode, errorResponse ?? "");
        }

        private static async Task<T> ReadResponse<T>(HttpResponseMessage responseMessage)
        {
            string response = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(response);
            return result;
        }

        private static StringContent SerializeToString(object data, bool ignoreNullValues)
        {
            string serializedObject = ignoreNullValues ?
                JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }) :
                JsonConvert.SerializeObject(data);

            var content = data != null ?
                new StringContent(serializedObject) :
                new StringContent(string.Empty);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}
