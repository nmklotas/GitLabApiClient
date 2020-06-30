using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Models.Users.Responses;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test.Internal.Http
{
    //TODO this is currently not working for root user...
    // [Collection("GitLabContainerFixture")]
    // public class GitLabHttpFacadeTest
    // {
    //     [Fact]
    //     public async Task CanLogin()
    //     {
    //         //arrange
    //         var sut = GetFacade();

    //         //act
    //         var session = await sut.LoginAsync(TestUserName, TestPassword);

    //         //assert
    //         session.Should().Match<Session>(s =>
    //             s.CanCreateGroup &&
    //             s.CanCreateProject &&
    //             s.IsAdmin &&
    //             !s.TwoFactorEnabled &&
    //             s.Username == TestUserName &&
    //             s.Name == TestName);

    //         session.PrivateToken.Should().NotBeNullOrEmpty();
    //     }
    // }
}
