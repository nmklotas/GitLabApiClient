using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.MergeRequests.Requests;
using GitLabApiClient.Models.MergeRequests.Responses;
using GitLabApiClient.Test.Utilities;
using Xunit;

namespace GitLabApiClient.Test
{
    [Collection("GitLabContainerFixture")]
    public class MergeRequestClientTest : IAsyncLifetime
    {
        private readonly MergeRequestsClient _sut = new MergeRequestsClient(
            GitLabApiHelper.GetFacade(), new MergeRequestsQueryBuilder(), new ProjectMergeRequestsQueryBuilder());

        [Fact]
        public async Task CreatedMergeRequestCanBeRetrieved()
        {
            var mergeRequest = await _sut.CreateAsync(new CreateMergeRequest(GitLabApiHelper.TestProjectTextId, "sourcebranch1", "master", "Title")
            {
                AssigneeId = 1,
                Description = "Description",
                Labels = new[] { "Label1" },
                RemoveSourceBranch = true,
            }, CancellationToken.None);

            mergeRequest.Should().Match(Assert());

            var retrievedMergeRequest = (await _sut.GetAsync(GitLabApiHelper.TestProjectId, CancellationToken.None)).Single();

            retrievedMergeRequest.Should().Match(Assert());

            Expression<Func<MergeRequest, bool>> Assert()
            {
                return m =>
                    m.Assignee.Id == 1 &&
                    m.ForceRemoveSourceBranch == true &&
                    m.Title == "Title" &&
                    m.Description == "Description" &&
                    m.Labels.SequenceEqual(new[] { "Label1" }) &&
                    m.ProjectId == GitLabApiHelper.TestProjectTextId &&
                    m.SourceBranch == "sourcebranch1" &&
                    m.State == MergeRequestState.Opened &&
                    m.TargetBranch == "master";
            }
        }

        [Fact]
        public async Task CreatedMergeRequestCanBeUpdated()
        {
            var createdMergeRequest = await _sut.CreateAsync(new CreateMergeRequest(GitLabApiHelper.TestProjectTextId, "sourcebranch1", "master", "Title1")
            {
                AssigneeId = 1,
                Description = "Description1",
                Labels = new[] { "Label1" },
                MilestoneId = 1
            }, CancellationToken.None);

            var updatedMergeRequest = await _sut.UpdateAsync(new UpdateMergeRequest(GitLabApiHelper.TestProjectTextId, createdMergeRequest.Iid)
            {
                AssigneeId = 1,
                Description = "Description11",
                Title = "Title11",
                Labels = new[] { "Label11"},
                MilestoneId = 11,
                TargetBranch = "master11",
                RemoveSourceBranch = true
            }, CancellationToken.None);

            updatedMergeRequest.Should().Match<MergeRequest>(
                m => m.Assignee.Id == 1 &&
                     m.Description == "Description11" &&
                     m.Title == "Title11" &&
                     m.Labels.SequenceEqual(new[] {"Label11"}) &&
                     m.TargetBranch == "master11" &&
                     m.ForceRemoveSourceBranch == true);
        }

        [Fact]
        public async Task MergeRequestWithoutCommitsCannotBeAccepted()
        {
            var createdMergeRequest = await _sut.CreateAsync(new CreateMergeRequest(GitLabApiHelper.TestProjectTextId, "sourcebranch1", "master", "Title1")
            {
                AssigneeId = 1,
                Description = "Description1",
                Labels = new[] { "Label1" },
                MilestoneId = 1
            }, CancellationToken.None);

            Func<Task<MergeRequest>> acceptAction = () => 
                _sut.AcceptAsync(new AcceptMergeRequest(GitLabApiHelper.TestProjectTextId, createdMergeRequest.Iid), CancellationToken.None);

            acceptAction.ShouldThrow<GitLabException>().
                WithMessage("{\"message\":\"405 Method Not Allowed\"}").
                Where(e => e.HttpStatusCode == HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task MergeRequestCanBeClosed()
        {
            var createdMergeRequest = await _sut.CreateAsync(new CreateMergeRequest(GitLabApiHelper.TestProjectTextId, "sourcebranch1", "master", "Title1"),
                                                             CancellationToken.None);

            var updatedMergeRequest = await _sut.UpdateAsync(new UpdateMergeRequest(GitLabApiHelper.TestProjectTextId, createdMergeRequest.Iid)
            {
                State = RequestedMergeRequestState.Close
            }, CancellationToken.None);

            updatedMergeRequest.State.Should().Be(MergeRequestState.Closed);
        }

        public Task InitializeAsync() => DeleteAllMergeRequests();

        public Task DisposeAsync() => DeleteAllMergeRequests();

        private async Task DeleteAllMergeRequests()
        {
            var mergeRequests = await _sut.GetAsync(GitLabApiHelper.TestProjectId, CancellationToken.None);
            await Task.WhenAll(mergeRequests.Select(
                m => _sut.DeleteAsync(GitLabApiHelper.TestProjectId, m.Iid, CancellationToken.None)));
        }
    }
}
