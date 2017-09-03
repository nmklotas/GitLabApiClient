using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitLabApiClient.Http
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
            var responseMessage = await _requestor.GetWithHeaders<IList<T>>(GetPagedUrl(url, 1));
            result.AddRange(responseMessage.Item1);

            //get paged urls
            var pagedUrls = GetPagedUrls(url, responseMessage.Item2);
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

        private static List<string> GetPagedUrls(string originalUrl, HttpResponseHeaders headers)
        {
            if (!headers.TryGetValues("X-Total-Pages", out IEnumerable<string> totalPagesValue))
                return new List<string>();

            string totalPages = totalPagesValue.FirstOrDefault();
            if (totalPages == null)
                return new List<string>();

            if (!int.TryParse(totalPages, out int totalPagesCount))
                return new List<string>();

            if (totalPagesCount == 1)
                //one page was already retrieved by the first request
                return new List<string>();

            //1 less page because it was already requested by the first request
            int pagesCount = totalPagesCount - 1;

            var pagedUrls = new List<string>();
            for (int i = 0; i < pagesCount; i++)
                pagedUrls.Add(GetPagedUrl(originalUrl, 2 + i));

            return pagedUrls;
        }

        private static string GetPagedUrl(string url, int pageNumber)
        {
            string parameterSymbol = url.Contains("?") ? "&" : "?";
            return $"{url}{parameterSymbol}per_page={MaxItemsPerPage}&page={pageNumber}";
        }
    }
}
