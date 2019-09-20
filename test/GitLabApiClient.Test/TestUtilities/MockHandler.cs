using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GitLabApiClient.Test.TestUtilities
{
    public abstract class MockHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string requestUriPathAndQuery = request.RequestUri.PathAndQuery;
            return Task.FromResult(SendAsync(request.Method, requestUriPathAndQuery));
        }

        public abstract HttpResponseMessage SendAsync(HttpMethod method, string url);
    }
}