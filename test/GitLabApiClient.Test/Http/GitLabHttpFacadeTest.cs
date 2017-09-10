using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Test.Common;
using GitLabApiClient.Users.Responses;
using Xunit;

namespace GitLabApiClient.Test.Http
{
    public class GitLabHttpFacadeTest
    {
        [Fact]
        public async Task CanLogin()
        {
            //arrange
            var facade = GitLabApiHelper.GetFacade();

            //act
            var session = await facade.LoginAsync("test-gitlabapiclient", "KZKSRcxx");

            //assert
            session.Should().Match<Session>(s =>
                s.CanCreateGroup &&
                s.CanCreateProject &&
                !s.IsAdmin &&
                !s.TwoFactorEnabled &&
                s.Username == "test-gitlabapiclient" &&
                s.Name == "test-gitlabapiclient");

            session.PrivateToken.Should().NotBeNullOrEmpty();
        }
    }
}
