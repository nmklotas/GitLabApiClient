using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;

namespace GitLabApiClient
{
    /// <summary>
    /// Provides a client connection to make rest requests to HTTP endpoints.
    /// <exception cref="GitLabException">Thrown if request to GitLab API does not indicate success</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class ConnectionClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal ConnectionClient(
            GitLabHttpFacade httpFacade
        )
        {
            _httpFacade = httpFacade;
        }

        /// <summary>
        /// Performs an asynchronous HTTP GET request.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        public Task<T> GetAsync<T>(string uri)
            => _httpFacade.Get<T>(uri);

        /// <summary>
        /// Performs an asynchronous HTTP paged GET request that iterates all pages.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        public Task<IList<T>> GetPagedListAsync<T>(string uri)
            => _httpFacade.GetPagedList<T>(uri);
    }
}
