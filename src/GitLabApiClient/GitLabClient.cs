using System;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Users;

namespace GitLabApiClient
{
    public class GitLabClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        public GitLabClient(string hostUrl, string authenticationToken = "")
        {
            if (string.IsNullOrEmpty(hostUrl))
                throw new ArgumentException(nameof(hostUrl));

            if (authenticationToken == null)
                throw new ArgumentNullException(nameof(authenticationToken));

            _httpFacade = new GitLabHttpFacade(
                hostUrl,
                authenticationToken);

            var projectQueryBuilder = new ProjectsQueryBuilder();
            var issuesQueryBuilder = new IssuesQueryBuilder();

            Issues = new IssuesClient(_httpFacade, issuesQueryBuilder);
            MergeRequests = new MergeRequestsClient(_httpFacade);
            Projects = new ProjectsClient(_httpFacade, projectQueryBuilder);
            Users = new UsersClient(_httpFacade);
        }

        public IssuesClient Issues { get; }

        public MergeRequestsClient MergeRequests { get; }

        public ProjectsClient Projects { get; }

        public UsersClient Users { get; }

        public Task<Session> LoginAsync(string username, string password) =>
            _httpFacade.LoginAsync(username, password);
    }
}
