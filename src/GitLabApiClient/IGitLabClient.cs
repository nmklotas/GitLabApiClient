using System.Threading.Tasks;
using GitLabApiClient.Models.Oauth.Responses;

namespace GitLabApiClient
{
    public interface IGitLabClient
    {
        /// <summary>
        /// Access GitLab's issues API.
        /// </summary>
        IIssuesClient Issues { get; }

        /// <summary>
        /// Access GitLab's uploads API.
        /// </summary>
        IUploadsClient Uploads { get; }

        /// <summary>
        /// Access GitLab's merge requests API.
        /// </summary>
        IMergeRequestsClient MergeRequests { get; }

        /// <summary>
        /// Access GitLab's projects API.
        /// </summary>
        IProjectsClient Projects { get; }

        /// <summary>
        /// Access GitLab's users API.
        /// </summary>
        IUsersClient Users { get; }

        /// <summary>
        /// Access GitLab's groups API.
        /// </summary>
        IGroupsClient Groups { get; }

        /// <summary>
        /// Access GitLab's branches API.
        /// </summary>
        IBranchClient Branches { get; }

        /// <summary>
        /// Access GitLab's release API.
        /// </summary>
        IReleaseClient Releases { get; }

        /// <summary>
        /// Access GitLab's tags API.
        /// </summary>
        ITagClient Tags { get; }

        /// <summary>
        /// Access GitLab's webhook API.
        /// </summary>
        IWebhookClient Webhooks { get; }

        /// <summary>
        /// Access GitLab's commits API.
        /// </summary>
        ICommitsClient Commits { get; }

        /// <summary>
        /// Access GitLab's trees API.
        /// </summary>
        ITreesClient Trees { get; }

        /// <summary>
        /// Access GitLab's files API.
        /// </summary>
        IFilesClient Files { get; }

        /// <summary>
        /// Access GitLab's Markdown API.
        /// </summary>
        IMarkdownClient Markdown { get; }

        /// <summary>
        /// Acess GitLab's Pipeline API.
        /// </summary>
        IPipelineClient Pipelines { get; }

        /// <summary>
        /// Access GitLab's Runners API.
        /// </summary>
        IRunnersClient Runners { get; }

        /// <summary>
        /// Access GitLab's ToDo-List API.
        /// </summary>
        IToDoListClient ToDoList { get; }

        /// <summary>
        /// Access GitLab's Iterations API.
        /// </summary>
        IIterationsClient Iterations { get; }

        /// <summary>
        /// Host address of GitLab instance. For example https://gitlab.example.com or https://gitlab.example.com/api/v4/.
        /// </summary>
        string HostUrl { get; }

        /// <summary>
        /// Authenticates with GitLab API using user credentials.
        /// </summary>
        Task<AccessTokenResponse> LoginAsync(string username, string password, string scope = "api");
    }
}
