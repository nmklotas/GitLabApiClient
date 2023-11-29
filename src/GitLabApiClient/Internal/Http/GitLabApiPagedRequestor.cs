using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;

namespace GitLabApiClient.Internal.Http
{
    internal sealed class GitLabApiPagedRequestor
    {
        internal const int MaxItemsPerPage = 100;

        private readonly GitLabApiRequestor _requestor;

        public GitLabApiPagedRequestor(GitLabApiRequestor requestor) => _requestor = requestor;

        public async Task<(RateLimitPagingInfo rateLimitPagingInfo, IList<T>)> GetRateLimitPagedList<T>(string url, PageOptions pageOptions)
        {
            var (results, headers) = await _requestor.GetWithHeaders<IList<T>>(
                GetPagedUrl(url, pageOptions.Page, pageOptions.ItemsPerPage));

            return new ValueTuple<RateLimitPagingInfo, IList<T>>(RateLimitPagingInfo.FromHeaders(headers), results);
        }

        public async Task<IList<T>> GetPagedList<T>(string url, PageOptions pageOptions)
        {
            var result = new List<T>();

            if (pageOptions != null)
            {
                return await _requestor.Get<IList<T>>(GetPagedUrl(url, pageOptions.Page, pageOptions.ItemsPerPage));
            }

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

        private static string GetPagedUrl(string url, int pageNumber, int? itemsPerPage = null)
        {
            string parameterSymbol = url.Contains("?") ? "&" : "?";
            return $"{url}{parameterSymbol}per_page={itemsPerPage ?? MaxItemsPerPage}&page={pageNumber}";
        }
    }

    internal static class HttpResponseHeadersExtensions
    {
        public static T GetFirstHeaderValueOrDefault<T>(this HttpResponseHeaders headers, string headerKey)
            => headers.TryGetFirstHeaderValue<T>(headerKey, out var result) ? result : default;

        public static int? GetFirstHeaderIntValueOrNull(this HttpResponseHeaders headers, string headerKey)
            => headers.TryGetFirstHeaderValue(headerKey, out int result) ? result : null;

        public static DateTime? GetFirstHeaderDateTimeValueOrNull(this HttpResponseHeaders headers, string headerKey)
            => headers.TryGetFirstHeaderValue(headerKey, out DateTime result) ? result : null;

        private static bool TryGetFirstHeaderValue<T>(this HttpHeaders headers, string headerKey, out T result)
        {
            if (headers.TryGetValues(headerKey, out var headerValues) && headerValues.FirstOrDefault() is { } valueString && !valueString.IsNullOrEmpty())
            {
                result = (T)Convert.ChangeType(valueString, typeof(T));
                return true;
            }

            result = default;
            return false;
        }
    }
}
