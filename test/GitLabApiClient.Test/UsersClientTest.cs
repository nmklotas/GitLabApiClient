using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class UsersClientTest
    {
        private readonly UsersClient _sut = new UsersClient(GetFacade());

        [Fact]
        public async Task CurrentUserSessionCanBeRetrieved()
        {
            var session = await _sut.GetCurrentSessionAsync();
            session.Username.Should().Be(TestUserName);
            session.Name.Should().Be(TestName);
        }

        [Fact]
        public async Task UserRetrievedByName()
        {
            var user = await _sut.GetAsync(TestUserName);
            user.Username.Should().Be(TestUserName);
            user.Name.Should().Be(TestName);
        }

        [Fact]
        public async Task NonExistingUserRetrievedAsNull()
        {
            var user = await _sut.GetAsync("nonexistingusername");
            user.Should().BeNull();
        }
    }
}
