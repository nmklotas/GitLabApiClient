using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
                MergeRequestIdToResolveDiscussions = 4
            }, CancellationToken.None);

            //act
            var updatedIssue = await _sut.UpdateAsync(new UpdateIssueRequest(TestProjectTextId, createdIssue.Iid)
            {
                Assignees = new List<int> { 11 },
                Confidential = false,
                Description = "Description11",
                Labels = new[] { "Label11" },
                Title = "Title11",
                MilestoneId = 22
            }, CancellationToken.None);

            //assert
            updatedIssue.Should().Match<Issue>(i =>
                i.ProjectId == TestProjectTextId &&
                i.Confidential == false &&
                i.Description == "Description11" &&
                i.Labels.SequenceEqual(new[] { "Label11" }) &&
                i.Title == "Title11");
        }

        [Fact]
        public async Task CreatedIssueCanBeClosed()
        {
            //arrange
            var createdIssue = await _sut.CreateAsync(new CreateIssueRequest(TestProjectTextId, "Title1"), CancellationToken.None);

            //act
            var updatedIssue = await _sut.UpdateAsync(new UpdateIssueRequest(TestProjectTextId, createdIssue.Iid)
            {
                State = UpdatedIssueState.Close
            }, CancellationToken.None);

            //assert
            updatedIssue.Should().Match<Issue>(i => i.State == IssueState.Closed);
        }

        [Fact]
        public async Task CreatedIssueCanBeListedFromProject()
        {
            //arrange
            string title = Guid.NewGuid().ToString();
            await _sut.CreateAsync(new CreateIssueRequest(TestProjectTextId, title), CancellationToken.None);

            //act
            var listedIssues = await _sut.GetProjectIssuesAsync(TestProjectTextId, o => o.Filter = title, CancellationToken.None);

            //assert
            listedIssues.Single().Title.Should().Be(title);
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
            }, CancellationToken.None);

            //act
            var issueById = await _sut.GetAsync(TestProjectId, issue.Iid, CancellationToken.None);
            var issueByProjectId = (await _sut.GetAsync(o => o.IssueIds = new[] { issue.Iid }, CancellationToken.None)).FirstOrDefault(i => i.Title == title);
            var ownedIssue = (await _sut.GetAsync(o => o.Scope = Scope.CreatedByMe, CancellationToken.None)).FirstOrDefault(i => i.Title == title);

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
