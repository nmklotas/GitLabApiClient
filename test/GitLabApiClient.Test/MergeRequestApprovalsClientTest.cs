using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.MergeRequestApprovals.Responses;
using GitLabApiClient.Models.MergeRequests.Requests;
using GitLabApiClient.Test.Utilities;
using Xunit;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class MergeRequestApprovalsClientTest
    {
        private readonly MergeRequestsClient _mrSuts = new MergeRequestsClient(
            GitLabApiHelper.GetFacade(), new MergeRequestsQueryBuilder(), new ProjectMergeRequestsQueryBuilder(),
            new ProjectMergeRequestsNotesQueryBuilder());
        private readonly MergeRequestApprovalsClient _sut = new MergeRequestApprovalsClient(
            GitLabApiHelper.GetFacade()
        );

        [Fact]
        public async Task TestApproveUnapprove()
        {
            var approvals = await _sut.GetAsync(GitLabApiHelper.TestProjectTextId, _createdMr);
            Assert(approvals.ApprovedBy, false);

            await _sut.ApproveAsync(GitLabApiHelper.TestProjectTextId, _createdMr);
            approvals = await _sut.GetAsync(GitLabApiHelper.TestProjectTextId, _createdMr);
            Assert(approvals.ApprovedBy, true);

            await _sut.UnapproveAsync(GitLabApiHelper.TestProjectTextId, _createdMr);
            Assert(approvals.ApprovedBy, false);

            void Assert(IList<ApprovalUser> approvedBy, bool expected)
            {
                if (expected)
                {
                    approvedBy.Should().HaveCount(1);
                    approvedBy.First().User.Id.Should().Be(GitLabApiHelper.TestUserId);
                }
                else
                {
                    approvedBy.Should().HaveCount(0);
                }
            }
        }

        public int _createdMr;
        public async Task InitializeAsync()
        {
            await DeleteAllMergeRequests();
            var createdMergeRequest = await _mrSuts.CreateAsync(GitLabApiHelper.TestProjectTextId,
                new CreateMergeRequest("sourcebranch1", "master", "Title1"));
            _createdMr = createdMergeRequest.Iid;
        }

        public Task DisposeAsync() => DeleteAllMergeRequests();

        private async Task DeleteAllMergeRequests()
        {
            var mergeRequests = await _mrSuts.GetAsync(GitLabApiHelper.TestProjectId);
            await Task.WhenAll(mergeRequests.Select(
                m => _mrSuts.DeleteAsync(GitLabApiHelper.TestProjectId, m.Iid)));
        }
    }
}
