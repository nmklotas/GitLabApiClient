using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http.Serialization;
using GitLabApiClient.Models.Uploads.Requests;
using GitLabApiClient.Models.Uploads.Responses;

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

        public async Task<T> Get<T>(string url)
        {
            var responseMessage = await _client.GetAsync(url);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }

        public async Task GetFile(string url, string outputPath)
        {
            var response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            var inputStream = await response.Content.ReadAsStreamAsync();
            using (var outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                await inputStream.CopyToAsync(outputStream);
            }
        }

        public async Task<T> Post<T>(string url, object data = null)
        {
            StringContent content = SerializeToString(data);
            var responseMessage = await _client.PostAsync(url, content);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }

        public async Task Post(string url, object data = null)
        {
            StringContent content = SerializeToString(data);
            var responseMessage = await _client.PostAsync(url, content);
            await EnsureSuccessStatusCode(responseMessage);
        }

        public async Task<Upload> PostFile(string url, CreateUploadRequest uploadRequest)
        {
            return await PostFile<Upload>(url, null, uploadRequest);
        }

        public async Task<T> PostFile<T>(string url, Dictionary<string, string> keyValues, CreateUploadRequest uploadRequest)
        {
            using (var uploadContent =
                new MultipartFormDataContent($"Upload----{DateTime.Now.Ticks}"))
            {
                uploadContent.Add(new StreamContent(uploadRequest.Stream), "file", uploadRequest.FileName);

                if (keyValues != null)
                {
                    foreach (var kv in keyValues)
                    {
                        uploadContent.Add(new StringContent(kv.Value), kv.Key);
                    }
                }

                var responseMessage = await _client.PostAsync(url, uploadContent);
                await EnsureSuccessStatusCode(responseMessage);

                return await ReadResponse<T>(responseMessage);
            }
        }

        public async Task<T> Put<T>(string url, object data)
        {
            StringContent content = SerializeToString(data);
            var responseMessage = await _client.PutAsync(url, content);
            await EnsureSuccessStatusCode(responseMessage);
            return await ReadResponse<T>(responseMessage);
        }

        public async Task Put(string url, object data)
        {
            StringContent content = SerializeToString(data);
            var responseMessage = await _client.PutAsync(url, content);
            await EnsureSuccessStatusCode(responseMessage);
        }

        public async Task Delete(string url)
        {
            var responseMessage = await _client.DeleteAsync(url);
            await EnsureSuccessStatusCode(responseMessage);
        }

        public async Task Delete(string url, object data)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = SerializeToString(data)
            };
            var responseMessage = await _client.SendAsync(request);
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
