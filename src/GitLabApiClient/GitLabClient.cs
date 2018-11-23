using System;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Http.Serialization;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Users.Responses;

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
            HostUrl = hostUrl;

            var jsonSerializer = new RequestsJsonSerializer();

            _httpFacade = new GitLabHttpFacade(
                FixBaseUrl(hostUrl),
                jsonSerializer,
                authenticationToken);

            var projectQueryBuilder = new ProjectsQueryBuilder();
            var projectIssuesQueryBuilder = new ProjectIssuesQueryBuilder();
            var issuesQueryBuilder = new IssuesQueryBuilder();
            var mergeRequestsQueryBuilder = new MergeRequestsQueryBuilder();
            var projectMilestonesQueryBuilder = new ProjectMilestonesQueryBuilder();
            var projectMergeRequestsQueryBuilder = new ProjectMergeRequestsQueryBuilder();
            var groupsQueryBuilder = new GroupsQueryBuilder();
            var projectsGroupsQueryBuilder = new ProjectsGroupQueryBuilder();

            Issues = new IssuesClient(_httpFacade, issuesQueryBuilder, projectIssuesQueryBuilder);
            MergeRequests = new MergeRequestsClient(_httpFacade, mergeRequestsQueryBuilder, projectMergeRequestsQueryBuilder);
            Projects = new ProjectsClient(_httpFacade, projectQueryBuilder, projectMilestonesQueryBuilder);
            Users = new UsersClient(_httpFacade);
            Groups = new GroupsClient(_httpFacade, groupsQueryBuilder, projectsGroupsQueryBuilder);
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
        /// Access GitLab's groups API.
        /// </summary>
        public GroupsClient Groups { get; }

        /// <summary>
        /// Host address of GitLab instance. For example https://gitlab.example.com or https://gitlab.example.com/api/v4/.
        /// </summary>
        public string HostUrl { get; }

        /// <summary>
        /// Authenticates with GitLab API using user credentials.
        /// </summary>
        public Task<Session> LoginAsync(string username, string password)
        {
            Guard.NotEmpty(username, nameof(username));
            Guard.NotEmpty(password, nameof(password));
            return _httpFacade.LoginAsync(username, password);
        }

        private static string FixBaseUrl(string url)
        {
            if (!url.EndsWith("/", StringComparison.OrdinalIgnoreCase))
                url += "/";

            if (!url.EndsWith("/api/v4/", StringComparison.OrdinalIgnoreCase))
                url += "/api/v4/";

            return url;
        }
    }
}
