using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Groups.Requests;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class GroupsQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new GroupsQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/groups",
                new GroupsQueryOptions
                {
                    SkipGroups = new[] { 1, 2 },
                    AllAvailable = true,
                    Search = "filter",
                    Order = GroupsOrder.Path,
                    Sort = GroupsSort.Descending,
                    Statistics = true,
                    Owned = true
                });

            query.Should().Be("https://gitlab.com/api/v4/groups?" +
                              "skip_groups%5b%5d=1&skip_groups%5b%5d=2&" +
                              "all_available=true&" +
                              "search=filter&" +
                              "order_by=path&" +
                              "sort=desc&" +
                              "statistics=true&" +
                              "owned=true");
        }
    }
}
