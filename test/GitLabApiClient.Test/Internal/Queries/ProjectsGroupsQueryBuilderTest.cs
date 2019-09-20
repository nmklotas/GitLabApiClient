using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Groups.Requests;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class ProjectsGroupsQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new ProjectsGroupQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/groups",
                new ProjectsGroupQueryOptions()
                {
                    Archived = true,
                    Visibility = GroupsVisibility.Internal,
                    Order = GroupsProjectsOrder.Id,
                    Sort = GroupsSort.Descending,
                    Search = "filter",
                    Simple = true,
                    Owned = true,
                    Starred = true
                });

            query.Should().Be("https://gitlab.com/api/v4/groups?" +
                              "archived=true&" +
                              "visibility=internal&" +
                              "order_by=id&" +
                              "sort=desc&" +
                              "search=filter&" +
                              "simple=true&" +
                              "owned=true&" +
                              "starred=true");
        }
    }
}
