using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;
using GitLabApiClient.Models.Notes.Requests;
using GitLabApiClient.Models.Notes.Responses;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class IssuesClientTest
    {
        private readonly IssuesClient _sut = new IssuesClient(
            GetFacade(), new IssuesQueryBuilder(), new ProjectIssueNotesQueryBuilder());

        [Fact]
        public async Task CreatedIssueCanBeUpdated()
        {
            //arrange
            var createdIssue = await _sut.CreateAsync(TestProjectTextId, new CreateIssueRequest("Title1")
            {
                Assignees = new List<int> { 1 },
                Confidential = true,
                Description = "Description1",
                Labels = new[] { "Label1" },
                MilestoneId = 2,
                DiscussionToResolveId = 3,
                MergeRequestIdToResolveDiscussions = 4
            });

            //act
            var updatedIssue = await _sut.UpdateAsync(TestProjectTextId, createdIssue.Iid, new UpdateIssueRequest()
            {
                Assignees = new List<int> { 11 },
                Confidential = false,
                Description = "Description11",
                Labels = new[] { "Label11" },
                Title = "Title11",
                MilestoneId = 22
            });

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
            var createdIssue = await _sut.CreateAsync(TestProjectTextId, new CreateIssueRequest("Title1"));

            //act
            var updatedIssue = await _sut.UpdateAsync(TestProjectTextId, createdIssue.Iid, new UpdateIssueRequest()
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
            await _sut.CreateAsync(TestProjectTextId, new CreateIssueRequest(title));

            //act
            var listedIssues = await _sut.GetAllAsync(projectId: TestProjectTextId, options: o => o.Filter = title);

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

            var issue = await _sut.CreateAsync(TestProjectTextId, new CreateIssueRequest(title)
            {
                Assignees = new List<int> { 1 },
                Confidential = true,
                Description = "Description",
                Labels = new List<string> { "Label1" }
            });

            //act
            var issueById = await _sut.GetAsync(TestProjectId, issue.Iid);
            var issueByProjectId = (await _sut.GetAllAsync(options: o => o.IssueIds = new[] { issue.Iid })).FirstOrDefault(i => i.Title == title);
            var ownedIssue = (await _sut.GetAllAsync(options: o => o.Scope = Scope.CreatedByMe)).FirstOrDefault(i => i.Title == title);

            //assert
            issue.Should().Match<Issue>(i =>
                i.ProjectId == TestProjectTextId &&
                i.Confidential && i.Title == title && i.Description == "Description" &&
                i.Labels.SequenceEqual(new[] { "Label1" }));

            issueById.Should().BeEquivalentTo(issue, o => o.Excluding(s => s.UpdatedAt));
            issueByProjectId.Should().BeEquivalentTo(issue, o => o.Excluding(s => s.UpdatedAt));
            ownedIssue.Should().BeEquivalentTo(issue, o => o.Excluding(s => s.UpdatedAt));
        }

        [Fact]
        public async Task CreatedIssueNoteCanBeRetrieved()
        {
            //arrange
            string body = "comment1";
            var issue = await _sut.CreateAsync(TestProjectTextId, new CreateIssueRequest(Guid.NewGuid().ToString())
            {
                Description = "Description"
            });

            //act
            var note = await _sut.CreateNoteAsync(TestProjectTextId, issue.Iid, new CreateIssueNoteRequest
            {
                Body = body,
                CreatedAt = DateTime.Now
            });
            var issueNotes = (await _sut.GetNotesAsync(TestProjectId, issue.Iid)).FirstOrDefault(i => i.Body == body);
            var issueNote = await _sut.GetNoteAsync(TestProjectId, issue.Iid, note.Id);

            //assert
            note.Should().Match<Note>(n =>
                n.Body == body);

            issueNotes.Should().BeEquivalentTo(note, o => o.Excluding(s => s.UpdatedAt));
            issueNote.Should().BeEquivalentTo(note, o => o.Excluding(s => s.UpdatedAt));
        }

        [Fact]
        public async Task CreatedIssueNoteCanBeUpdated()
        {
            //arrange
            var createdIssue = await _sut.CreateAsync(TestProjectTextId, new CreateIssueRequest(Guid.NewGuid().ToString())
            {
                Description = "Description1"
            });
            var createdIssueNote = await _sut.CreateNoteAsync(TestProjectTextId, createdIssue.Iid, new CreateIssueNoteRequest("comment2"));

            //act
            var updatedIssueNote = await _sut.UpdateNoteAsync(TestProjectTextId, createdIssue.Iid, createdIssueNote.Id, new UpdateIssueNoteRequest("comment22"));

            //assert
            updatedIssueNote.Should().Match<Note>(n =>
                n.Body == "comment22");
        }

        [Fact]
        public async Task CreateIssueWithTasks()
        {
            //arrange
            string title = Guid.NewGuid().ToString();
            await _sut.CreateAsync(TestProjectTextId, new CreateIssueRequest(title)
            {
                Description = @"Description1
- [ ] Task 1
- [ ] Task 2
- [x] Task 3
"
            });

            //act
            var listedIssues = await _sut.GetAllAsync(projectId: TestProjectTextId, options: o => o.Filter = title);

            //assert
            listedIssues.Single().Should().Match<Issue>(i =>
                i.ProjectId == TestProjectTextId &&
                i.Title == title &&
                i.TaskCompletionStatus != null &&
                i.TaskCompletionStatus.Count == 3 &&
                i.TaskCompletionStatus.Completed == 1 &&
                i.TimeStats != null);
        }
    }
}
