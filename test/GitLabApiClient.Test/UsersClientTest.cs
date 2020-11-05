using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Models.Users.Requests;
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

        [Fact]
        public async Task CreateImpersonationToken()
        {
            var token = await _sut.CreateImpersonationTokenAsync(TestUserId, new CreateUserImpersonationTokenRequest("test_token", new ApiScope[] { ApiScope.Api }));
            token.Name.Should().Be("test_token");
            token.Revoked.Should().Be(false);
            token.Active.Should().Be(true);
            token.Impersonation.Should().Be(true);
            token.Scopes.Should().Contain(Models.Users.Requests.ApiScope.Api);
            token.Token.Should().NotBeNull();
        }

    }
}
