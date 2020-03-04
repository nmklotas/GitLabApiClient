using System;
using System.Collections.Generic;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class IssuesQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new IssuesQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/issues",
                new IssuesQueryOptions
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
                    CreatedBefore = new DateTime(1991, 12, 12, 2, 2, 2),
                    UpdatedAfter = new DateTime(1991, 4, 4, 4, 4, 4),
                    UpdatedBefore = new DateTime(1991, 5, 5, 5, 5, 5),
                    IsConfidential = true,
                    AuthorUsername = "user_1",
                    AssigneeUsername = new List<string> { "user_1", "user_2" }
                });

            query.Should().BeEquivalentTo("https://gitlab.com/api/v4/issues?" +
                              "state=opened&" +
                              "labels=label1%2Clabel2&" +
                              "milestone=milestone1&" +
                              "scope=all&" +
                              "author_id=1&" +
                              "assignee_id=2&" +
                              "iids%5B%5D=3&iids%5B%5D=4&" +
                              "order_by=updated_at&" +
                              "sort=asc&" +
                              "search=filter&" +
                              "confidential=true&" +
                              "created_before=1991-12-12T02%3a02%3a02.0000000&" +
                              "created_after=1991-11-11T01%3a01%3a01.0000000&" +
                              "updated_before=1991-05-05T05%3a05%3a05.0000000&" +
                              "updated_after=1991-04-04T04%3a04%3a04.0000000");
        }

        [Fact]
        public void NonDefaultQueryBuiltWithUserNames()
        {
            var sut = new IssuesQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/issues",
                new IssuesQueryOptions
                {
                    State = IssueState.Opened,
                    Labels = { "label1", "label2" },
                    MilestoneTitle = "milestone1",
                    Scope = Scope.All,
                    AuthorId = null,
                    AssigneeId = null,
                    IssueIds = { 3, 4 },
                    Order = IssuesOrder.UpdatedAt,
                    SortOrder = SortOrder.Ascending,
                    Filter = "filter",
                    CreatedAfter = new DateTime(1991, 11, 11, 1, 1, 1),
                    CreatedBefore = new DateTime(1991, 12, 12, 2, 2, 2),
                    UpdatedAfter = new DateTime(1991, 4, 4, 4, 4, 4),
                    UpdatedBefore = new DateTime(1991, 5, 5, 5, 5, 5),
                    IsConfidential = true,
                    AuthorUsername = "user_1",
                    AssigneeUsername = new List<string> { "user_1", "user_2" }
                });

            query.Should().BeEquivalentTo("https://gitlab.com/api/v4/issues?" +
                              "state=opened&" +
                              "labels=label1%2Clabel2&" +
                              "milestone=milestone1&" +
                              "scope=all&" +
                              "author_username=user_1&" +
                              "assignee_username=user_1%2Cuser_2&" +
                              "iids%5B%5D=3&iids%5B%5D=4&" +
                              "order_by=updated_at&" +
                              "sort=asc&" +
                              "search=filter&" +
                              "confidential=true&" +
                              "created_before=1991-12-12T02%3a02%3a02.0000000&" +
                              "created_after=1991-11-11T01%3a01%3a01.0000000&" +
                              "updated_before=1991-05-05T05%3a05%3a05.0000000&" +
                              "updated_after=1991-04-04T04%3a04%3a04.0000000");
        }
    }
}
