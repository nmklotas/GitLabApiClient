using FluentAssertions;
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
    }
}
