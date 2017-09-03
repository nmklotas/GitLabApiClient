using System;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Users;
using GitLabApiClient.Utilities;

namespace GitLabApiClient
{
    /// <summary>
    /// A client for the GitLab API v4. You can read more about the api here: https://docs.gitlab.com/ce/api/README.html.
    /// </summary>
    public sealed class GitLabClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        /// <summary>
        /// Creates a new instance of the GitLab API v4 client pointing to the specified hostUrl.
        /// </summary>
        /// <param name="hostUrl">Host address of GitLab instance. For example https://gitlab.example.com or https://gitlab.example.com/api/v4/ </param>
        /// <param name="authenticationToken">Personal access token. Obtained from GitLab profile settings.</param>
        public GitLabClient(string hostUrl, string authenticationToken = "")
        {
            Guard.NotEmpty(hostUrl, nameof(hostUrl));
            Guard.NotNull(authenticationToken, nameof(authenticationToken));

            _httpFacade = new GitLabHttpFacade(
                PrefixBaseUrl(hostUrl),
                authenticationToken);

            var projectQueryBuilder = new ProjectsQueryBuilder();
            var projectIssuesQueryBuilder = new ProjectIssuesQueryBuilder();
            var issuesQueryBuilder = new IssuesQueryBuilder();
            var mergeRequestsQueryBuilder = new MergeRequestsQueryBuilder();
            var projectMergeRequestsQueryBuilder = new ProjectMergeRequestsQueryBuilder();

            Issues = new IssuesClient(_httpFacade, issuesQueryBuilder, projectIssuesQueryBuilder);
            MergeRequests = new MergeRequestsClient(_httpFacade, mergeRequestsQueryBuilder, projectMergeRequestsQueryBuilder);
            Projects = new ProjectsClient(_httpFacade, projectQueryBuilder);
            Users = new UsersClient(_httpFacade);
        }

        /// <summary>
        /// Access GitLab's issues API.
        /// </summary>
        public IssuesClient Issues { get; }

        /// <summary>
        /// Access GitLab's merge requests API.
        /// </summary>
        public MergeRequestsClient MergeRequests { get; }

        /// <summary>
        /// Access GitLab's projects API.
        /// </summary>
        public ProjectsClient Projects { get; }

        /// <summary>
        /// Access GitLab's users API.
        /// </summary>
        public UsersClient Users { get; }

        /// <summary>
        /// Authenticates with GitLab API using user credentials.
        /// </summary>
        public Task<Session> LoginAsync(string username, string password)
        {
            Guard.NotEmpty(username, nameof(username));
            Guard.NotEmpty(password, nameof(password));
            return _httpFacade.LoginAsync(username, password);
        }

        private static string PrefixBaseUrl(string url)
        {
            if (!url.EndsWith("/", StringComparison.OrdinalIgnoreCase))
                url = url + "/";

            if (!url.EndsWith("/api/v4", StringComparison.OrdinalIgnoreCase))
                url = url + "/api/v4/";

            return url;
        }
    }
}
