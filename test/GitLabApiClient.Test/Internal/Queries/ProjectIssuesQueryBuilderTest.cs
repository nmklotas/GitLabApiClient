using System;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class ProjectIssuesQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new ProjectIssuesQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/issues",
                new ProjectIssuesQueryOptions()
                {
                    State = IssueState.Opened,
                    Labels = { "label1", "label2" },
                    MilestoneTitle = "milestone1",
                    Scope = Scope.All,
                    AuthorId = 1,
                    AssigneeId = 2,
                    IssueIds = { 3, 4 },
                    Order = IssuesOrder.UpdatedAt,
                    SortOrder = SortOrder.Ascending,
                    Filter = "filter",
                    CreatedAfter = new DateTime(1991, 11, 11, 1, 1, 1),
                    CreatedBefore = new DateTime(1991, 12, 12, 2, 2, 2)
                });

            query.Should().Be("https://gitlab.com/api/v4/issues?" +
                              "state=opened&" +
                              "labels=label1%2clabel2&" +
                              "milestone=milestone1&" +
                              "scope=all&" +
                              "author_id=1&" +
                              "assignee_id=2&" +
                              "iids%5b%5d=3&iids%5b%5d=4&" +
                              "order_by=updated_at&" +
                              "sort=asc&" +
                              "search=filter&" +
                              "created_after=1991-11-11T01%3a01%3a01.0000000&" +
                              "created_before=1991-12-12T02%3a02%3a02.0000000");
        }
    }
}
