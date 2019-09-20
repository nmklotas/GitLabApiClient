using System;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models;
using GitLabApiClient.Models.MergeRequests.Requests;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class ProjectMergeRequestsQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new ProjectMergeRequestsQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/merge_requests",
                new ProjectMergeRequestsQueryOptions()
                {
                    MergeRequestsIds = new[] { 4, 5 },
                    State = QueryMergeRequestState.Opened,
                    Order = MergeRequestsOrder.UpdatedAt,
                    SortOrder = SortOrder.Ascending,
                    MilestoneTitle = "milestone1",
                    Simple = true,
                    Labels = { "label1", "label2" },
                    CreatedAfter = new DateTime(1991, 11, 11, 1, 1, 1),
                    CreatedBefore = new DateTime(1991, 12, 12, 2, 2, 2),
                    Scope = Scope.All,
                    AuthorId = 1,
                    AssigneeId = 2
                });

            query.Should().Be("https://gitlab.com/api/v4/merge_requests?" +
                              "iids%5b%5d=4&iids%5b%5d=5&" +
                              "state=opened&" +
                              "order_by=updated_at&" +
                              "sort=asc&" +
                              "milestone=milestone1&" +
                              "view=simple&" +
                              "labels=label1%2clabel2&" +
                              "created_after=1991-11-11T01%3a01%3a01.0000000&" +
                              "created_before=1991-12-12T02%3a02%3a02.0000000&" +
                              "scope=all&" +
                              "author_id=1&" +
                              "assignee_id=2");
        }
    }
}
