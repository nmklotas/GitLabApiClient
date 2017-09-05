using FluentAssertions;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Projects;
using Xunit;

namespace GitLabApiClient.Test
{
    public class ProjectsQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new ProjectsQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/projects", 
                new ProjectQueryOptions
            {
                UserId = "1",
                Archived = true,
                Visibility = QueryProjectVisibilityLevel.Internal,
                Order = ProjectsOrder.UpdatedAt,
                SortOrder = SortOrder.Ascending,
                Filter = "filter",
                Simple = true,
                Owned = true,
                IsMemberOf = true,
                Starred = true,
                IncludeStatistics = true,
                WithIssuesEnabled = true,
                WithMergeRequestsEnabled = true
            });

            query.Should().Be("https://gitlab.com/api/v4/projects?" +
                              "user_id=1&" +
                              "archived=true&" +
                              "visibility=internal&" +
                              "order_by=updated_at&" +
                              "sort=asc&" +
                              "search=filter&" +
                              "simple=true&" +
                              "owned=true&" +
                              "membership=true&" +
                              "starred=true&" +
                              "statistics=true&" +
                              "with_issues_enabled=true&" +
                              "with_merge_requests_enabled=true");
        }
    }
}
