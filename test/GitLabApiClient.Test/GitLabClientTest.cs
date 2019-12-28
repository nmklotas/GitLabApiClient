using FluentAssertions;
using GitLabApiClient.Test.Utilities;
using Xunit;

namespace GitLabApiClient.Test
{
    public class GitLabClientTest
    {
        [Theory]
        [InlineData("https://gitlab.com/api/v4/")]
        [InlineData("https://gitlab.com/api/v4")]
        [InlineData("https://gitlab.com/")]
        [InlineData("https://gitlab.com")]
        public void GitLabClientCanBeConstructed(string hostUrl)
        {
            var sut = new GitLabClient(hostUrl);
            sut.HostUrl.Should().Be("https://gitlab.com/api/v4/");
        }

        [Fact]
        public void CheckClients()
        {
            var sut = new GitLabClient("https://gitlab.com/api/v4/");
            sut.Branches.Should().BeAssignableTo(typeof(BranchClient));
            sut.Commits.Should().BeAssignableTo(typeof(CommitsClient));
            sut.Groups.Should().BeAssignableTo(typeof(GroupsClient));
            sut.Issues.Should().BeAssignableTo(typeof(IssuesClient));
            sut.Markdown.Should().BeAssignableTo(typeof(MarkdownClient));
            sut.Pipelines.Should().BeAssignableTo(typeof(PipelineClient));
            sut.Projects.Should().BeAssignableTo(typeof(ProjectsClient));
            sut.Releases.Should().BeAssignableTo(typeof(ReleaseClient));
            sut.Tags.Should().BeAssignableTo(typeof(TagClient));
            sut.Uploads.Should().BeAssignableTo(typeof(UploadsClient));
            sut.Users.Should().BeAssignableTo(typeof(UsersClient));
            sut.Webhooks.Should().BeAssignableTo(typeof(WebhookClient));
            sut.MergeRequests.Should().BeAssignableTo(typeof(MergeRequestsClient));
        }

        [Trait("Category", "LinuxIntegration")]
        [Collection("GitLabContainerFixture")]
        public class Integration
        {
            [Fact]
            public async void CanLogin()
            {
                var sut = new GitLabClient(GitLabContainerFixture.GitlabHost);
                var session = await sut.LoginAsync(GitLabApiHelper.TestUserName, GitLabApiHelper.TestUserPassword);
                session.Username.Should().Be(GitLabApiHelper.TestUserName);
            }
        }
    }
}
