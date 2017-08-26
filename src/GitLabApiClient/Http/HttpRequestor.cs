using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitLabApiClient.Http
{
    internal class HttpRequestor
    {
        private readonly HttpClient _client;

        public HttpRequestor(HttpClient client) => _client = client;

        public async Task<T> Put<T>(string url, object data)
        {
            StringContent content = SerializeToString(data, false);
            var responseMessage = await _client.PutAsync(GetAPIUrl(url), content);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }

        public async Task<T> Post<T>(string url, object data = null)
        {
            StringContent content = SerializeToString(data, true);
            var responseMessage = await _client.PostAsync(GetAPIUrl(url), content);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }

        public async Task Delete(string url)
        {
            var responseMessage = await _client.DeleteAsync(GetAPIUrl(url));
            await EnsureSuccessStatusCode(responseMessage);
        }

        public async Task<T> Get<T>(string url)
        {
            var responseMessage = await _client.GetAsync(GetAPIUrl(url));
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }

        public async Task<IList<T>> GetAll<T>(string url)
        {
            var result = new List<T>();

            string nextUrlToLoad = GetAPIUrl(url);
            while (nextUrlToLoad != null)
            {
                var responseMessage = await _client.GetAsync(nextUrlToLoad);
                await EnsureSuccessStatusCode(responseMessage);

                if (responseMessage.Headers.TryGetValues("Link", out IEnumerable<string> links))
                {
                    string link = links.FirstOrDefault();
                    string[] nextLink = null;

                    if (!string.IsNullOrEmpty(link))
                        nextLink = link.Split(',').
                            Select(l => l.Split(';')).
                            FirstOrDefault(pair => pair[1].Contains("next"));

                    nextUrlToLoad = nextLink?[0].Trim('<', '>', ' ');
                }
                else
                {
                    nextUrlToLoad = null;
                }

                result.AddRange(await ReadResponse<List<T>>(responseMessage));
            }

            return result;
        }

        private static async Task EnsureSuccessStatusCode(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
                return;

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            throw new GitLabException(errorResponse ?? "");
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

        private static string GetAPIUrl(string url)
        {
            if (!url.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                url = "/" + url;

            if (!url.StartsWith("/api/v4", StringComparison.OrdinalIgnoreCase))
                url = "/api/v4" + url;

            return url;
        }
    }
}
