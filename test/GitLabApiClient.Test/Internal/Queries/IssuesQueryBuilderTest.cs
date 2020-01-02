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
                    Filter = "filter"
                });

            query.Should().Be("https://gitlab.com/api/v4/issues?" +
                              "state=opened&" +
                              "labels=label1%2Clabel2&" +
                              "milestone=milestone1&" +
                              "scope=all&" +
                              "author_id=1&" +
                              "assignee_id=2&" +
                              "iids%5B%5D=3&iids%5B%5D=4&" +
                              "order_by=updated_at&" +
                              "sort=asc&" +
                              "search=filter");
        }
    }
}
