using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Groups.Requests;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class GroupLabelsQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new GroupLabelsQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/groups/123/labels",
                new GroupLabelsQueryOptions
                {
                    WithCounts = true,
                    IncludeAncestorGroups = false
                });

            query.Should().Be("https://gitlab.com/api/v4/groups/123/labels?" +
                              "with_counts=true&" +
                              "include_ancestor_groups=false");
        }
    }
}
