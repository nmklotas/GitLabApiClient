using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models;

namespace GitLabApiClient
{
    public class GitLabClient
    {
        private readonly GitlabHttpFacade _httpFacade;

        public GitLabClient(string hostUrl, string authenticationToken = "")
        {
            _httpFacade = new GitlabHttpFacade(hostUrl, authenticationToken);
            Issues = new IssuesClient(_httpFacade);
            MergeRequests = new MergeRequestsClient(_httpFacade);
            Projects = new ProjectsClient(_httpFacade);
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
