using System;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Http.Serialization;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Oauth.Requests;
using GitLabApiClient.Models.Oauth.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// A client for the GitLab API v4. You can read more about the api here: https://docs.gitlab.com/ce/api/README.html.
    /// </summary>
    public sealed class GitLabClient : IGitLabClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        /// <summary>
        /// Creates a new instance of the GitLab API v4 client pointing to the specified hostUrl.
        /// </summary>
        /// <param name="hostUrl">Host address of GitLab instance. For example https://gitlab.example.com or https://gitlab.example.com/api/v4/ </param>
        /// <param name="authenticationToken">Personal access token. Obtained from GitLab profile settings.</param>
        /// <param name="httpMessageHandler">Optional handler for HTTP messages. Used for SSL pinning or canceling validation for example.</param>
        /// <param name="clientTimeout">Timespan with the HTTP client timeout if null default timeout is used. Sometimes required to upload files to a gitlab instance depending on filesize and networkspeed.</param>
        public GitLabClient(string hostUrl, string authenticationToken = "", HttpMessageHandler httpMessageHandler = null, TimeSpan? clientTimeout = null)
        {
            Guard.NotEmpty(hostUrl, nameof(hostUrl));
            Guard.NotNull(authenticationToken, nameof(authenticationToken));
            HostUrl = FixBaseUrl(hostUrl);

            var jsonSerializer = new RequestsJsonSerializer();

            _httpFacade = new GitLabHttpFacade(
                HostUrl,
                jsonSerializer,
                authenticationToken,
                httpMessageHandler,
                clientTimeout);

            var projectQueryBuilder = new ProjectsQueryBuilder();
            var projectIssueNotesQueryBuilder = new ProjectIssueNotesQueryBuilder();
            var projectMergeRequestsNotesQueryBuilder = new ProjectMergeRequestsNotesQueryBuilder();
            var issuesQueryBuilder = new IssuesQueryBuilder();
            var mergeRequestsQueryBuilder = new MergeRequestsQueryBuilder();
            var projectMilestonesQueryBuilder = new MilestonesQueryBuilder();
            var projectMergeRequestsQueryBuilder = new ProjectMergeRequestsQueryBuilder();
            var groupsQueryBuilder = new GroupsQueryBuilder();
            var groupLabelsQueryBuilder = new GroupLabelsQueryBuilder();
            var projectsGroupsQueryBuilder = new ProjectsGroupQueryBuilder();
            var branchQueryBuilder = new BranchQueryBuilder();
            var releaseQueryBuilder = new ReleaseQueryBuilder();
            var tagQueryBuilder = new TagQueryBuilder();
            var commitQueryBuilder = new CommitQueryBuilder();
            var commitRefsQueryBuilder = new CommitRefsQueryBuilder();
            var commitStatusesQueryBuilder = new CommitStatusesQueryBuilder();
            var pipelineQueryBuilder = new PipelineQueryBuilder();
            var treeQueryBuilder = new TreeQueryBuilder();
            var jobQueryBuilder = new JobQueryBuilder();
            var toDoListBuilder = new ToDoListQueryBuilder();
            var iterationsBuilder = new IterationsQueryBuilder();

            Issues = new IssuesClient(_httpFacade, issuesQueryBuilder, projectIssueNotesQueryBuilder);
            Uploads = new UploadsClient(_httpFacade);
            MergeRequests = new MergeRequestsClient(_httpFacade, mergeRequestsQueryBuilder, projectMergeRequestsQueryBuilder, projectMergeRequestsNotesQueryBuilder);
            Projects = new ProjectsClient(_httpFacade, projectQueryBuilder, projectMilestonesQueryBuilder, jobQueryBuilder);
            Users = new UsersClient(_httpFacade);
            Groups = new GroupsClient(_httpFacade, groupsQueryBuilder, projectsGroupsQueryBuilder, projectMilestonesQueryBuilder, groupLabelsQueryBuilder);
            Branches = new BranchClient(_httpFacade, branchQueryBuilder);
            Releases = new ReleaseClient(_httpFacade, releaseQueryBuilder);
            Tags = new TagClient(_httpFacade, tagQueryBuilder);
            Webhooks = new WebhookClient(_httpFacade);
            Commits = new CommitsClient(_httpFacade, commitQueryBuilder, commitRefsQueryBuilder, commitStatusesQueryBuilder);
            Markdown = new MarkdownClient(_httpFacade);
            Pipelines = new PipelineClient(_httpFacade, pipelineQueryBuilder, jobQueryBuilder);
            Trees = new TreesClient(_httpFacade, treeQueryBuilder);
            Files = new FilesClient(_httpFacade);
            Runners = new RunnersClient(_httpFacade);
            ToDoList = new ToDoListClient(_httpFacade, toDoListBuilder);
            Iterations = new IterationsClient(_httpFacade, iterationsBuilder);
            Connection = new ConnectionClient(_httpFacade);
        }

        /// <summary>
        /// Access GitLab's issues API.
        /// </summary>
        public IIssuesClient Issues { get; }

        /// <summary>
        /// Access GitLab's uploads API.
        /// </summary>
        public IUploadsClient Uploads { get; }

        /// <summary>
        /// Access GitLab's merge requests API.
        /// </summary>
        public IMergeRequestsClient MergeRequests { get; }

        /// <summary>
        /// Access GitLab's projects API.
        /// </summary>
        public IProjectsClient Projects { get; }

        /// <summary>
        /// Access GitLab's users API.
        /// </summary>
        public IUsersClient Users { get; }

        /// <summary>
        /// Access GitLab's groups API.
        /// </summary>
        public IGroupsClient Groups { get; }

        /// <summary>
        /// Access GitLab's branches API.
        /// </summary>
        public IBranchClient Branches { get; }

        /// <summary>
        /// Access GitLab's release API.
        /// </summary>
        public IReleaseClient Releases { get; }

        /// <summary>
        /// Access GitLab's tags API.
        /// </summary>
        public ITagClient Tags { get; }

        /// <summary>
        /// Access GitLab's webhook API.
        /// </summary>
        public IWebhookClient Webhooks { get; }

        /// <summary>
        /// Access GitLab's commits API.
        /// </summary>
        public ICommitsClient Commits { get; }

        /// <summary>
        /// Access GitLab's trees API.
        /// </summary>
        public ITreesClient Trees { get; }

        /// <summary>
        /// Access GitLab's files API.
        /// </summary>
        public IFilesClient Files { get; }

        /// <summary>
        /// Access GitLab's Markdown API.
        /// </summary>
        public IMarkdownClient Markdown { get; }

        /// <summary>
        /// Acess GitLab's Pipeline API.
        /// </summary>
        public IPipelineClient Pipelines { get; }

        /// <summary>
        /// Access GitLab's Runners API.
        /// </summary>
        public IRunnersClient Runners { get; }

        /// <summary>
        /// Access GitLab's ToDo-List API.
        /// </summary>
        public IToDoListClient ToDoList { get; }

        /// <summary>
        /// Provides a client connection to make rest requests to HTTP endpoints.
        /// </summary>
        public ConnectionClient Connection { get; }

        /// <inheritdoc/>
        public IIterationsClient Iterations { get; }

        /// <summary>
        /// Host address of GitLab instance. For example https://gitlab.example.com or https://gitlab.example.com/api/v4/.
        /// </summary>
        public string HostUrl { get; }

        /// <summary>
        /// Authenticates with GitLab API using user credentials.
        /// </summary>
        public Task<AccessTokenResponse> LoginAsync(string username, string password, string scope = "api")
        {
            Guard.NotEmpty(username, nameof(username));
            Guard.NotEmpty(password, nameof(password));
            var accessTokenRequest = new AccessTokenRequest
            {
                GrantType = "password",
                Scope = scope,
                Username = username,
                Password = password
            };
            return _httpFacade.LoginAsync(accessTokenRequest);
        }

        private static string FixBaseUrl(string url)
        {
            url = url.TrimEnd('/');

            if (!url.EndsWith("/api/v4", StringComparison.OrdinalIgnoreCase))
                url += "/api/v4";

            return url + "/";
        }
    }
}
