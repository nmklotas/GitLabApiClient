using System.Threading.Tasks;
using FluentAssertions;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;
using Xunit;
using GitLabApiClient.Http;
using Moq;
using System.Diagnostics.CodeAnalysis;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Commits.Responses;

namespace GitLabApiClient.Test
{
    [ExcludeFromCodeCoverage]
    public class CommitsClientTest
    {
        [Fact]
        public async void GetCommitBySha()
        {
            string projectId = "id";
            string sha = "sha-tmp";
            string uri = $"projects/{projectId}/repository/commits/{sha}";

            var commit = new Commit();
            var httpFacade = new Mock<GitLabHttpFacade>(MockBehavior.Strict);
            httpFacade.Setup(c => c.Get<Commit>(uri)).ReturnsAsync(commit);
            var commitsClient = new CommitsClient(httpFacade.Object, new CommitQueryBuilder());

            var commitFromClient = await commitsClient.GetAsync(projectId, sha);
            commitFromClient.Should().BeSameAs(commit);
        }

    }
}
