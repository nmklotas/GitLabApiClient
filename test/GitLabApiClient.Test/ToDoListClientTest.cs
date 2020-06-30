using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.MergeRequests.Requests;
using GitLabApiClient.Models.ToDoList.Requests;
using GitLabApiClient.Models.ToDoList.Responses;
using GitLabApiClient.Test.Utilities;
using Xunit;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class ToDoListClientTest : IAsyncLifetime
    {
        private readonly MergeRequestsClient mergeRequestsClient = new MergeRequestsClient(GitLabApiHelper.GetFacade(), new MergeRequestsQueryBuilder(), new ProjectMergeRequestsQueryBuilder(), new ProjectMergeRequestsNotesQueryBuilder());
        private readonly IssuesClient issuesClient = new IssuesClient(GitLabApiHelper.GetFacade(), new IssuesQueryBuilder(), new ProjectIssueNotesQueryBuilder());


        private readonly ToDoListClient _sut = new ToDoListClient(
            GitLabApiHelper.GetFacade(),
            new ToDoListQueryBuilder());

        [Fact]
        public async Task RetrieveToDoListAndMarkAsDone()
        {
            var mergeRequest = await mergeRequestsClient.CreateAsync(GitLabApiHelper.TestProjectTextId, new CreateMergeRequest("sourcebranch1", "master", "Title")
            {
                AssigneeId = GitLabApiHelper.TestUserId
            });

            var issue = await issuesClient.CreateAsync(GitLabApiHelper.TestProjectTextId, new CreateIssueRequest("IssueTitle")
            {
                Assignees = new List<int> { GitLabApiHelper.TestUserId }
            });

            // todoList is populated
            var todoList = await _sut.GetAsync();
            todoList.Should().Contain(t =>
                t.ActionType == ToDoActionType.Assigned
                && t.Body == "IssueTitle"
                && t.TargetType == ToDoTargetType.Issue
            );
            todoList.Should().Contain(t =>
                t.ActionType == ToDoActionType.Assigned
                && t.Body == "Title"
                && t.TargetType == ToDoTargetType.MergeRequest
            );

            // mark the mergeRequest as done
            var mergeToDo = todoList.Single(t => t.TargetType == ToDoTargetType.MergeRequest);
            mergeToDo.Id.HasValue.Should().BeTrue();
            await _sut.MarkAsDoneAsync(mergeToDo.Id ?? 1);
            todoList = await _sut.GetAsync();
            todoList.Should().NotContain(t => t.TargetType == ToDoTargetType.MergeRequest);

            // mark all toDos as done
            await _sut.MarkAllAsDoneAsync();
            todoList = await _sut.GetAsync();
            todoList.Should().BeEmpty();
        }

        private async Task QuerySetupToDosAsync()
        {
            _ = await mergeRequestsClient.CreateAsync(GitLabApiHelper.TestProjectTextId, new CreateMergeRequest("sourcebranch1", "master", "Title")
            {
                AssigneeId = GitLabApiHelper.TestUserId
            });

            _ = await issuesClient.CreateAsync(GitLabApiHelper.TestProjectTextId, new CreateIssueRequest("IssueTitle1")
            {
                Assignees = new List<int> { GitLabApiHelper.TestUserId }
            });

            _ = await issuesClient.CreateAsync(GitLabApiHelper.TestProjectTextId, new CreateIssueRequest("IssueTitle2")
            {
                Description = $"@{GitLabApiHelper.TestUserName}"
            });
        }

        public static IEnumerable<object[]> QueriesAndAssertions => new[]
        {
            new object[]{
                (Action<ToDoListQueryOptions>) ((x) => x.Type = ToDoTargetType.MergeRequest),
                (Action<IEnumerable<IToDo>>)((x) => x.Should().NotBeEmpty().And.OnlyContain(t => t.TargetType == ToDoTargetType.MergeRequest))
            },
            new object[]{
                (Action<ToDoListQueryOptions>) ((x) => x.Type = ToDoTargetType.Issue),
                (Action<IEnumerable<IToDo>>)((x) => x.Should().NotBeEmpty().And.OnlyContain(t => t.TargetType == ToDoTargetType.Issue))
            },
            new object[]{
                (Action<ToDoListQueryOptions>) ((x) => x.ActionType = ToDoActionType.Assigned),
                (Action<IEnumerable<IToDo>>)((x) => x.Should().NotBeEmpty().And.OnlyContain(t => t.ActionType == ToDoActionType.Assigned))
            },
            new object[]{
                (Action<ToDoListQueryOptions>) ((x) => x.ActionType = ToDoActionType.DirectlyAddressed),
                (Action<IEnumerable<IToDo>>)((x) => x.Should().NotBeEmpty().And.OnlyContain(t => t.ActionType == ToDoActionType.DirectlyAddressed))
            },
            new object[]{
                (Action<ToDoListQueryOptions>) ((x) => {
                    x.ActionType = ToDoActionType.DirectlyAddressed;
                    x.AuthorId = GitLabApiHelper.TestUserId;
                    x.ProjectId = GitLabApiHelper.TestProjectId;
                    x.GroupId = GitLabApiHelper.TestGroupId;
                    x.State = ToDoState.Pending;
                    x.Type = ToDoTargetType.Issue;
                }),
                (Action<IEnumerable<IToDo>>)((x) =>
                    x.Should().NotBeEmpty().And.OnlyContain(t =>
                        t.ActionType == ToDoActionType.DirectlyAddressed
                        && t.Author.Username == GitLabApiHelper.TestUserName
                        && t.Project.Name == GitLabApiHelper.TestProjectName
                        && t.State == ToDoState.Pending
                        && t.TargetType == ToDoTargetType.Issue
                ))
            },
        };

        [Theory]
        [MemberData(nameof(QueriesAndAssertions))]
        public async Task QueryToDoList(Action<ToDoListQueryOptions> queryOptions, Action<IEnumerable<IToDo>> assertion)
        {
            await QuerySetupToDosAsync();

            var todoList = await _sut.GetAsync(queryOptions);
            assertion.Invoke(todoList);

            await DeleteAllMergeRequests();
        }

        public Task InitializeAsync() => DeleteAllMergeRequests();

        public Task DisposeAsync() => DeleteAllMergeRequests();

        private async Task DeleteAllMergeRequests()
        {
            var mergeRequests = await mergeRequestsClient.GetAsync(GitLabApiHelper.TestProjectId);
            await Task.WhenAll(mergeRequests.Select(
                m => mergeRequestsClient.DeleteAsync(GitLabApiHelper.TestProjectId, m.Iid)));
        }
    }
}
