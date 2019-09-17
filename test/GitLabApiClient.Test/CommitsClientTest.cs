using System.Threading.Tasks;
using FluentAssertions;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;
using Xunit;
using GitLabApiClient.Internal.Http;
using Moq;
using GitLabApiClient.Models.Releases.Responses;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class CommitsClientTest
    {
        [Fact]
        public async void GetCommitBySha()
        {
            string projectId = "id";
            string sha = "sha-tmp";
            var commit = new Commit();
            var task = new Mock<Task<Commit>>();
            task.Setup(t => t.Result).Returns(commit);
            var httpFacade = new Mock<GitLabHttpFacade>();
            httpFacade.Setup(c => c.Get<Commit>($"projects/{projectId}/repository/commits/{sha}")).Returns(task.Object);
            var commitsClient = new CommitsClient(httpFacade.Object);

            var commitFromClient = await commitsClient.GetAsync(projectId, sha);
            commitFromClient.Should().BeSameAs(commit);
        }

    }
}
