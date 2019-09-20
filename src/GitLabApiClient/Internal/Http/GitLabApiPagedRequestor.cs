using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitLabApiClient.Internal.Http
{
    internal sealed class GitLabApiPagedRequestor
    {
        private const int MaxItemsPerPage = 100;

        private readonly GitLabApiRequestor _requestor;

        public GitLabApiPagedRequestor(GitLabApiRequestor requestor) => _requestor = requestor;

        public async Task<IList<T>> GetPagedList<T>(string url)
        {
            var result = new List<T>();

            //make first request and it will get available pages in the headers
            var response = await _requestor.GetWithHeaders<IList<T>>(GetPagedUrl(url, 1));
            var results = response.Item1;
            var headers = response.Item2;
            result.AddRange(results);
            int totalPages = headers.GetFirstHeaderValueOrDefault<int>("X-Total-Pages");
            int nextPage = headers.GetFirstHeaderValueOrDefault<int>("X-Next-Page");

            switch (totalPages)
            {
                // X-Total-Pages is not always present due to performance concern so we have to take the slow path of nextPage
                case 0 when nextPage > 1:
                    return await GetNextPageList(url, nextPage, result);
                case 0:
                case 1:
                    return result;
                default:
                    return await GetTotalPagedList(url, totalPages, result);
            }
        }

        private async Task<IList<T>> GetNextPageList<T>(string url, int nextPage, List<T> result)
        {
            do
            {
                string pagedUrl = GetPagedUrl(url, nextPage);
                var response = await _requestor.GetWithHeaders<IList<T>>(pagedUrl);
                var results = response.Item1;
                var headers = response.Item2;
                result.AddRange(results);
                nextPage = headers.GetFirstHeaderValueOrDefault<int>("X-Next-Page");
            }
            while (nextPage > 1);

            return result;
        }

        private async Task<IList<T>> GetTotalPagedList<T>(string url, int totalPages, List<T> result)
        {
            //get paged urls
            var pagedUrls = GetPagedUrls(url, totalPages);
            if (pagedUrls.Count == 0)
                return result;

            int partitionSize = Environment.ProcessorCount;
            var remainingUrls = pagedUrls;
            do
            {
                var responses = remainingUrls.Take(partitionSize).Select(
                    async u => await _requestor.Get<IList<T>>(u));

                var results = await Task.WhenAll(responses);
                result.AddRange(results.SelectMany(r => r));
                remainingUrls = remainingUrls.Skip(partitionSize).ToList();
            }
            while (remainingUrls.Any());

            return result;
        }

        private static List<string> GetPagedUrls(string originalUrl, int totalPages)
        {
            var pagedUrls = new List<string>();

            for (int i = 2; i <= totalPages; i++)
                pagedUrls.Add(GetPagedUrl(originalUrl, i));

            return pagedUrls;
        }

        private static string GetPagedUrl(string url, int pageNumber)
        {
            string parameterSymbol = url.Contains("?") ? "&" : "?";
            return $"{url}{parameterSymbol}per_page={MaxItemsPerPage}&page={pageNumber}";
        }
    }

    internal static class HttpResponseHeadersExtensions
    {
        public static T GetFirstHeaderValueOrDefault<T>(
            this HttpResponseHeaders headers,
            string headerKey)
        {
            var toReturn = default(T);

            if (!headers.TryGetValues(headerKey, out var headerValues))
                return toReturn;

            string valueString = headerValues.FirstOrDefault();
            return valueString == null ? toReturn : (T)Convert.ChangeType(valueString, typeof(T));
        }
    }
}
