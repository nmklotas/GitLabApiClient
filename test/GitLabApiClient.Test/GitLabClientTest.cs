using System;
using FluentAssertions;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Http.Serialization;
using GitLabApiClient.Models.Oauth.Requests;
using GitLabApiClient.Models.Users.Responses;
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

        [Fact]
        public void InvalidToken()
        {
            Action action = () => new GitLabClient("https://gitlab.com/api/v4/", "HelloWorld");
            action.Should()
                .Throw<ArgumentException>()
                .WithMessage("Unsupported authentication token provide, please private an oauth or private token");
        }

        [Fact]
        public void CheckClients()
        {
            var sut = new GitLabClient("https://gitlab.com/api/v4/");
            sut.Branches.Should().BeAssignableTo(typeof(BranchClient));
            sut.Commits.Should().BeAssignableTo(typeof(CommitsClient));
            sut.Files.Should().BeAssignableTo(typeof(FilesClient));
            sut.Groups.Should().BeAssignableTo(typeof(GroupsClient));
            sut.Issues.Should().BeAssignableTo(typeof(IssuesClient));
            sut.Markdown.Should().BeAssignableTo(typeof(MarkdownClient));
            sut.Pipelines.Should().BeAssignableTo(typeof(PipelineClient));
            sut.Projects.Should().BeAssignableTo(typeof(ProjectsClient));
            sut.Releases.Should().BeAssignableTo(typeof(ReleaseClient));
            sut.Tags.Should().BeAssignableTo(typeof(TagClient));
            sut.Trees.Should().BeAssignableTo(typeof(TreesClient));
            sut.Uploads.Should().BeAssignableTo(typeof(UploadsClient));
            sut.Users.Should().BeAssignableTo(typeof(UsersClient));
            sut.Webhooks.Should().BeAssignableTo(typeof(WebhookClient));
            sut.MergeRequests.Should().BeAssignableTo(typeof(MergeRequestsClient));
            sut.ToDoList.Should().BeAssignableTo(typeof(ToDoListClient));
        }

        [Trait("Category", "LinuxIntegration")]
        [Collection("GitLabContainerFixture")]
        public class Integration
        {
            [Fact]
            public async void CanLogin()
            {
                var sut = new GitLabClient(GitLabContainerFixture.GitlabHost);
                var accessTokenResponse = await sut.LoginAsync(GitLabApiHelper.TestUserName, GitLabApiHelper.TestUserPassword);
                accessTokenResponse.Scope.Should().Be("api");
                accessTokenResponse.CreatedAt.Should().NotBeNull();
                accessTokenResponse.AccessToken.Should().HaveLength(64);
                accessTokenResponse.RefreshToken.Should().HaveLength(64);
                accessTokenResponse.TokenType.Should().BeEquivalentTo("bearer");
                var currentSessionAsync = await sut.Users.GetCurrentSessionAsync();
                currentSessionAsync.Username.Should().Be(GitLabApiHelper.TestUserName);

                sut = new GitLabClient(GitLabContainerFixture.GitlabHost, accessTokenResponse.AccessToken);
                currentSessionAsync = await sut.Users.GetCurrentSessionAsync();
                currentSessionAsync.Username.Should().Be(GitLabApiHelper.TestUserName);

                var facadeSut = new GitLabHttpFacade(GitLabContainerFixture.GitlabHost, new RequestsJsonSerializer(),
                    GitLabContainerFixture.Token);
                currentSessionAsync = await facadeSut.Get<Session>("user");
                currentSessionAsync.Username.Should().Be(GitLabApiHelper.TestUserName);
                accessTokenResponse = await facadeSut.LoginAsync(new AccessTokenRequest
                {
                    GrantType = "password",
                    Scope = "api",
                    Username = GitLabApiHelper.TestUserName,
                    Password = GitLabApiHelper.TestUserPassword
                });
                currentSessionAsync = await facadeSut.Get<Session>("user");
                currentSessionAsync.Username.Should().Be(GitLabApiHelper.TestUserName);

                facadeSut = new GitLabHttpFacade(GitLabContainerFixture.GitlabHost, new RequestsJsonSerializer(),
                    accessTokenResponse.AccessToken);
                currentSessionAsync = await facadeSut.Get<Session>("user");
                currentSessionAsync.Username.Should().Be(GitLabApiHelper.TestUserName);
            }
        }
    }
}
