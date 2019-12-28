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
