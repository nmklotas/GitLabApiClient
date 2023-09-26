using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.LabelEvents.Responses;
using GitLabApiClient.Test.Utilities;
using Xunit;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class ResourceLabelEventsClientTest : IAsyncLifetime
    {
        private readonly MergeRequestsClient _mergeRequestsClient = new MergeRequestsClient(
            GitLabApiHelper.GetFacade(), new MergeRequestsQueryBuilder(), new ProjectMergeRequestsQueryBuilder(),
            new ProjectMergeRequestsNotesQueryBuilder());

        private readonly IssuesClient _issuesClient = new IssuesClient(
            GitLabApiHelper.GetFacade(), new IssuesQueryBuilder(), new ProjectIssueNotesQueryBuilder());

        private readonly ResourceLabelEventsClient _sut = new ResourceLabelEventsClient(GitLabApiHelper.GetFacade());

        [Fact]
        public async Task IssuesLabelEventsCanBeRetrieved()
        {
            var createdIssue = await _issuesClient.CreateAsync(
                GitLabApiHelper.TestProjectTextId, new CreateIssueRequest("Title1")
            {
                Assignees = new List<int> { 1 },
                Confidential = true,
                Description = "Description1",
                Labels = new[] { "Label1" },
                MilestoneId = 2,
                DiscussionToResolveId = 3,
                MergeRequestIdToResolveDiscussions = 4
            });

            var labelEvents = await _sut.GetAllAsync(
                GitLabApiHelper.TestProjectTextId,
                  EventResourceType.Issues,
                  createdIssue.Iid
                );

            labelEvents.Single().Should().Match<LabelEvent>(actual =>
                actual.Label.Name == "Label1");
        }

        public Task InitializeAsync() => throw new System.NotImplementedException();
        public Task DisposeAsync() => throw new System.NotImplementedException();
    }
}
