using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Models.Issues;
using Xunit;
using static GitLabApiClient.Test.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    public class IssuesClientTest
    {
        private readonly IssuesClient _sut = new IssuesClient(GetFacade());

        [Fact]
        public async Task CreatedIssueCanBeRetrieved()
        {
            //arrange
            string title = Guid.NewGuid().ToString();

            var issue = await _sut.CreateAsync(new CreateIssueRequest
            {
                ProjectId = TestProjectId,
                Assignees = new List<int> { 1 },
                Confidential = true,
                Title = title,
                Description = "Description",
                Labels = "Label1"
            });

            //act
            var issueById = await _sut.GetAsync(TestProjectId, issue.Iid);
            var issueByProjectId = (await _sut.GetAsync(TestProjectId)).FirstOrDefault(i => i.Title == title);
            var ownedIssue = (await _sut.GetOwnedAsync()).FirstOrDefault(i => i.Title == title);

            //assert
            issue.Should().Match<Issue>(i => 
                i.ProjectId == TestProjectId &&
                i.Confidential && i.Title == title && i.Description == "Description" &&
                i.Labels.Contains("Label1"));

            issueById.ShouldBeEquivalentTo(issue, o => o.Excluding(s => s.UpdatedAt));
            issueByProjectId.ShouldBeEquivalentTo(issue, o => o.Excluding(s => s.UpdatedAt));
            ownedIssue.ShouldBeEquivalentTo(issue, o => o.Excluding(s => s.UpdatedAt));
        }
    }
}
