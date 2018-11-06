using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class IssuesClientTest
    {
        private readonly IssuesClient _sut = new IssuesClient(
            GetFacade(), new IssuesQueryBuilder(), new ProjectIssuesQueryBuilder());

        [Fact]
        public async Task CreatedIssueCanBeUpdated()
        {
            //arrange
            var createdIssue = await _sut.CreateAsync(new CreateIssueRequest(TestProjectTextId, "Title1")
            {
                Assignees = new List<int> { 1 },
                Confidential = true,
                Description = "Description1",
                Labels = new[] { "Label1" },
                MilestoneId = 2,
                DiscussionToResolveId = 3,
                MergeRequestIdToResolveDiscussions = 4,
                Weight = 3
            });

            //act
            var updatedIssue = await _sut.UpdateAsync(new UpdateIssueRequest(TestProjectTextId, createdIssue.Iid)
            {
                Assignees = new List<int> { 11 },
                Confidential = false,
                Description = "Description11",
                Labels = new[] { "Label11" },
                Title = "Title11",
                MilestoneId = 22,
                Weight = 33
            });

            //assert
            updatedIssue.Should().Match<Issue>(i =>
                i.ProjectId == TestProjectTextId &&
                i.Confidential == false &&
                i.Description == "Description11" &&
                i.Labels.SequenceEqual(new[] { "Label11" }) &&
                i.Title == "Title11" &&
                i.Weight == 33);
        }

        [Fact]
        public async Task CreatedIssueCanBeClosed()
        {
            //arrange
            var createdIssue = await _sut.CreateAsync(new CreateIssueRequest(TestProjectTextId, "Title1"));

            //act
            var updatedIssue = await _sut.UpdateAsync(new UpdateIssueRequest(TestProjectTextId, createdIssue.Iid)
            {
                State = UpdatedIssueState.Close
            });

            //assert
            updatedIssue.Should().Match<Issue>(i => i.State == IssueState.Closed);
        }

        [Fact]
        public async Task CreatedIssueCanBeListedFromProject()
        {
            //arrange
            string title = Guid.NewGuid().ToString();
            await _sut.CreateAsync(new CreateIssueRequest(TestProjectTextId, title));

            //act
            var listedIssues = await _sut.GetAsync(TestProjectTextId, o => o.Filter = title);

            //assert
            listedIssues.Single().Should().Match<Issue>(i =>
                i.ProjectId == TestProjectTextId &&
                i.Title == title &&
                i.TimeStats != null);
        }

        [Fact]
        public async Task CreatedIssueCanBeRetrieved()
        {
            //arrange
            string title = Guid.NewGuid().ToString();

            var issue = await _sut.CreateAsync(new CreateIssueRequest(TestProjectTextId, title)
            {
                Assignees = new List<int> { 1 },
                Confidential = true,
                Description = "Description",
                Labels = new List<string> { "Label1" }
            });

            //act
            var issueById = await _sut.GetAsync(TestProjectId, issue.Iid);
            var issueByProjectId = (await _sut.GetAsync(o => o.IssueIds = new[] { issue.Iid })).FirstOrDefault(i => i.Title == title);
            var ownedIssue = (await _sut.GetAsync(o => o.Scope = Scope.CreatedByMe)).FirstOrDefault(i => i.Title == title);

            //assert
            issue.Should().Match<Issue>(i =>
                i.ProjectId == TestProjectTextId &&
                i.Confidential && i.Title == title && i.Description == "Description" &&
                i.Labels.SequenceEqual(new[] { "Label1" }));

            issueById.ShouldBeEquivalentTo(issue, o => o.Excluding(s => s.UpdatedAt));
            issueByProjectId.ShouldBeEquivalentTo(issue, o => o.Excluding(s => s.UpdatedAt));
            ownedIssue.ShouldBeEquivalentTo(issue, o => o.Excluding(s => s.UpdatedAt));
        }

    }
}
