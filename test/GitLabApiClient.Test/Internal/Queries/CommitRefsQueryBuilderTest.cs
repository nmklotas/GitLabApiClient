using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Commits.Requests;
using GitLabApiClient.Models.Commits.Responses;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class CommitRefsQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new CommitRefsQueryBuilder();

            string query = sut.Build(
                "https://https://gitlab.com/api/v4/projects/1/repository/commits/abc/refs",
                new CommitRefsQueryOptions {Type = CommitRefType.Branch});

            query.Should().Be("https://https://gitlab.com/api/v4/projects/1/repository/commits/abc/refs?" +
                              "type=branch");
        }
    }
}
