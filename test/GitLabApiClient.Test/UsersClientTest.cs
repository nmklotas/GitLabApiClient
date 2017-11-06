using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Test.Utilities;
using Xunit;

namespace GitLabApiClient.Test
{
    [Collection("GitLabContainerFixture")]
    public class UsersClientTest
    {
        private readonly UsersClient _sut = new UsersClient(GitLabApiHelper.GetFacade());

        [Fact]
        public async Task CurrentUserSessionCanBeRetrieved()
        {
            var session = await _sut.GetCurrentSessionAsync(CancellationToken.None);
            session.Username.Should().Be("test-gitlabapiclient");
            session.Name.Should().Be("test-gitlabapiclient");
        }

        [Fact]
        public async Task UserRetrievedByName()
        {
            var user = await _sut.GetAsync("test-gitlabapiclient", CancellationToken.None);
            user.Username.Should().Be("test-gitlabapiclient");
            user.Name.Should().Be("test-gitlabapiclient");
        }

        [Fact]
        public async Task NonExistingUserRetrievedAsNull()
        {
            var user = await _sut.GetAsync("test-gixxxtlabapiclient", CancellationToken.None);
            user.Should().BeNull();
        }
    }
}
