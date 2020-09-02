using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.MergeRequests.Requests;
using GitLabApiClient.Models.MergeRequests.Responses;
using GitLabApiClient.Test.Utilities;
using Xunit;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class MergeRequestClientTest : IAsyncLifetime
    {
        private readonly MergeRequestsClient _sut = new MergeRequestsClient(
            GitLabApiHelper.GetFacade(), new MergeRequestsQueryBuilder(), new ProjectMergeRequestsQueryBuilder(),
            new ProjectMergeRequestsNotesQueryBuilder());

        [Fact]
        public async Task CreatedMergeRequestCanBeRetrieved()
        {
            var mergeRequest = await _sut.CreateAsync(GitLabApiHelper.TestProjectTextId, new CreateMergeRequest("sourcebranch1", "master", "Title")
            {
                AssigneeId = 1,
                Description = "Description",
                Labels = new[] { "Label1" },
                RemoveSourceBranch = true,
            });

            mergeRequest.Should().Match(Assert());

            var retrievedMergeRequest = (await _sut.GetAsync(GitLabApiHelper.TestProjectId)).Single();

            retrievedMergeRequest.Should().Match(Assert());

            Expression<Func<MergeRequest, bool>> Assert()
            {
                return m =>
                    m.Assignee.Id == 1 &&
                    m.MergedBy == null &&
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
            var createdMergeRequest = await _sut.CreateAsync(GitLabApiHelper.TestProjectTextId, new CreateMergeRequest("sourcebranch1", "master", "Title1")
            {
                AssigneeId = 1,
                Description = "Description1",
                Labels = new[] { "Label1" },
                MilestoneId = 1
            });

            var updatedMergeRequest = await _sut.UpdateAsync(GitLabApiHelper.TestProjectTextId, createdMergeRequest.Iid, new UpdateMergeRequest()
            {
                AssigneeId = 1,
                Description = "Description11",
                Title = "Title11",
                Labels = new[] { "Label11" },
                MilestoneId = 11,
                TargetBranch = "master11",
                RemoveSourceBranch = true
            });

            updatedMergeRequest.Should().Match<MergeRequest>(
                m => m.Assignee.Id == 1 &&
                     m.Description == "Description11" &&
                     m.Title == "Title11" &&
                     m.Labels.SequenceEqual(new[] { "Label11" }) &&
                     m.TargetBranch == "master11" &&
                     m.ForceRemoveSourceBranch == true);
        }

        [Fact]
        public async Task MergeRequestWithoutCommitsCannotBeAccepted()
        {
            var createdMergeRequest = await _sut.CreateAsync(GitLabApiHelper.TestProjectTextId, new CreateMergeRequest("sourcebranch1", "master", "Title1")
            {
                AssigneeId = 1,
                Description = "Description1",
                Labels = new[] { "Label1" },
                MilestoneId = 1
            });

            Func<Task<MergeRequest>> acceptAction = () =>
                _sut.AcceptAsync(GitLabApiHelper.TestProjectTextId, createdMergeRequest.Iid, new AcceptMergeRequest());

            acceptAction.Should().Throw<GitLabException>().
                WithMessage("{\"message\":\"405 Method Not Allowed\"}").
                Where(e => e.HttpStatusCode == HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task MergeRequestCanBeClosed()
        {
            var createdMergeRequest = await _sut.CreateAsync(GitLabApiHelper.TestProjectTextId, new CreateMergeRequest("sourcebranch1", "master", "Title1"));

            var updatedMergeRequest = await _sut.UpdateAsync(GitLabApiHelper.TestProjectTextId, createdMergeRequest.Iid, new UpdateMergeRequest()
            {
                State = RequestedMergeRequestState.Close
            });

            updatedMergeRequest.State.Should().Be(MergeRequestState.Closed);
        }

        public Task InitializeAsync() => DeleteAllMergeRequests();

        public Task DisposeAsync() => DeleteAllMergeRequests();

        private async Task DeleteAllMergeRequests()
        {
            var mergeRequests = await _sut.GetAsync(GitLabApiHelper.TestProjectId);
            await Task.WhenAll(mergeRequests.Select(
                m => _sut.DeleteAsync(GitLabApiHelper.TestProjectId, m.Iid)));
        }
    }
}
