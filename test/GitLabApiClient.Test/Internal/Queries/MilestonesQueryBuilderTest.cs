using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class MilestonesQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new MilestonesQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/projects/projectId/milestones",
                new MilestonesQueryOptions()
                {
                    MilestoneIds = { 3, 4 },
                    State = MilestoneState.Active,
                    Search = "filter"
                });

            query.Should().Be("https://gitlab.com/api/v4/projects/projectId/milestones?" +
                              "iids%5B%5D=3&iids%5B%5D=4&" +
                              "state=active&" +
                              "search=filter");
        }
    }
}
