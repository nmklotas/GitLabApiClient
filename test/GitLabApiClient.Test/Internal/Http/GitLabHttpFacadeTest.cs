using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Models.Users.Responses;
using GitLabApiClient.Test.Utilities;
using Xunit;

namespace GitLabApiClient.Test.Internal.Http
{
    public class GitLabHttpFacadeTest
    {
        [Fact]
        public async Task CanLogin()
        {
            //arrange
            var facade = GitLabApiHelper.GetFacade();

            //act
            var session = await facade.LoginAsync("root", "hariseldon");

            //assert
            session.Should().Match<Session>(s =>
                s.CanCreateGroup &&
                s.CanCreateProject &&
                s.IsAdmin &&
                !s.TwoFactorEnabled &&
                s.Username == "root" &&
                s.Name == "Administrator");

            session.PrivateToken.Should().NotBeNullOrEmpty();
           
        }
    }
}
