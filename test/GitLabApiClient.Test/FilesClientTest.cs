using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class FilesClientTest
    {
        private readonly FilesClient _sut = new FilesClient(GetFacade());

        [Fact]
        public async Task GetFile()
        {
            var file = await _sut.GetAsync(TestProjectId, "README.md");
            file.Content.Should().NotBeNull();
            file.Encoding.Should().Be("base64");
            file.Reference.Should().Be("master");
            file.Filename.Should().Be("README.md");
            file.FullPath.Should().Be("README.md");
            file.Content.Should().Be("IyBUZXN0IHByb2plY3QKCkhlbGxvIHdvcmxkCg==");
            file.Size.Should().Be(28);
            file.ContentSha256.Should().Be("b6cb63af62daa14162368903ca4e42350cb1d855446febbdb22fb5c24f9aeedb");
            file.BlobId.Should().HaveLength(40);
            file.CommitId.Should().HaveLength(40);
            file.LastCommitId.Should().HaveLength(40);
            file.ContentDecoded.Should().Be("# Test project\n\nHello world\n");
        }
    }
}
