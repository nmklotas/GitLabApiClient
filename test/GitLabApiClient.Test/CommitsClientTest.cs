using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Commits.Requests.CreateCommitRequest;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class CommitsClientTest
    {
        private readonly CommitsClient _sut = new CommitsClient(
            GetFacade(), new CommitQueryBuilder(), new CommitRefsQueryBuilder(), new CommitStatusesQueryBuilder());

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task CreateCommitRequest(bool autoEncodeToBase64)
        {
            //We need a distinction as otherwise the create won't work for the second run (already exists).
            string suffix = autoEncodeToBase64 ? "_autoEncoded" : "";

            //Create
            var actions = new List<CreateCommitRequestAction>
            {
                new CreateCommitRequestAction(CreateCommitRequestActionType.Create, "file" + suffix)
                {
                    Content = "content"
                }
            };
            var createCommitRequest = new CreateCommitRequest("master", "create", actions);

            var commit = await _sut.CreateAsync(TestProjectId, createCommitRequest, autoEncodeToBase64);
            commit.Id.Should().NotBeNullOrEmpty();

            //Update
            actions = new List<CreateCommitRequestAction>
            {
                new CreateCommitRequestAction(CreateCommitRequestActionType.Update, "file" + suffix)
                {
                    Content = "new content"
                }
            };
            createCommitRequest = new CreateCommitRequest("master", "update", actions);

            commit = await _sut.CreateAsync(TestProjectId, createCommitRequest, autoEncodeToBase64);
            commit.Id.Should().NotBeNullOrEmpty();

            //Move
            actions = new List<CreateCommitRequestAction>
            {
                new CreateCommitRequestAction(CreateCommitRequestActionType.Move, "subfolder/file" + suffix)
                {
                    PreviousPath = "file" + suffix
                }
            };
            createCommitRequest = new CreateCommitRequest("master", "move", actions);

            commit = await _sut.CreateAsync(TestProjectId, createCommitRequest, autoEncodeToBase64);
            commit.Id.Should().NotBeNullOrEmpty();
            commit.AuthorEmail.Should().Be(TestUserEmail);
            commit.AuthorName.Should().Be(TestName);
            commit.CommitStats.Should().NotBeNull();

            //Delete
            actions = new List<CreateCommitRequestAction>
            {
                new CreateCommitRequestAction(CreateCommitRequestActionType.Delete, "subfolder/file" + suffix)
            };
            createCommitRequest = new CreateCommitRequest("master", "delete", actions)
            {
                AuthorEmail = TestExtraUserEmail,
                AuthorName = TestExtraName,
                Stats = false
            };

            commit = await _sut.CreateAsync(TestProjectId, createCommitRequest, autoEncodeToBase64);
            commit.Id.Should().NotBeNullOrEmpty();
            commit.AuthorEmail.Should().Be(TestExtraUserEmail);
            commit.AuthorName.Should().Be(TestExtraName);
            commit.CommitStats.Should().BeNull();
        }
    }
}
