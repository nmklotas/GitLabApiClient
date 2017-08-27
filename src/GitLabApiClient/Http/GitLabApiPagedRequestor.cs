using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitLabApiClient.Http
{
    internal class GitLabApiPagedRequestor
    {
        private readonly GitLabApiRequestor _requestor;

        public GitLabApiPagedRequestor(GitLabApiRequestor requestor) => _requestor = requestor;

        public async Task<IList<T>> GetPagedList<T>(string url)
        {
            var result = new List<T>();

            var responseMessage = await _requestor.GetWithHeaders<IList<T>>(url);
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

        private List<string> GetPagedUrls(string url, HttpResponseHeaders headers)
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

            const int maxItemsPerPage = 100;
            int pagesCount = (int)Math.Ceiling((double)totalPagesCount / maxItemsPerPage);

            var pagedUrls = new List<string>();
            for (int i = 0; i < pagesCount; i++)
                pagedUrls.Add($"{url}?per_page={maxItemsPerPage}&page={2 + i}");

            return pagedUrls;
        }
    }
}
