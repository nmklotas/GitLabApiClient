using FluentAssertions;
using Xunit;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class TagsClientTest
    {
        private const string ProjectId = "testProjectId";

        [Fact]
        public void TagsBaseUrlTest()
        {
            string baseUrl = TagClient.TagsBaseUrl(ProjectId);
            baseUrl.Should().Be("projects/" + ProjectId + "/repository/tags");
        }
    }
}
