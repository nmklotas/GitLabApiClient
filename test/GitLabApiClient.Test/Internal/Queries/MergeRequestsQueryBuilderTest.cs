using System;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models;
using GitLabApiClient.Models.MergeRequests.Requests;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class MergeRequestsQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new MergeRequestsQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/merge_requests",
                new MergeRequestsQueryOptions
                {
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
                              "state=opened&" +
                              "order_by=updated_at&" +
                              "sort=asc&" +
                              "milestone=milestone1&" +
                              "view=simple&" +
                              "labels=label1%2Clabel2&" +
                              "created_after=1991-11-11T01%3A01%3A01.0000000&" +
                              "created_before=1991-12-12T02%3A02%3A02.0000000&" +
                              "scope=all&" +
                              "author_id=1&" +
                              "assignee_id=2");
        }
    }
}
