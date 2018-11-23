using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class ProjectMilestonesQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new ProjectMilestonesQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/projects/projectId/milestones",
                new ProjectMilestonesQueryOptions()
                {
                    MilestoneIds = { 3, 4 },
                    State = MilestoneState.Active,
                    Search ="filter"
                });

            query.Should().Be("https://gitlab.com/api/v4/projects/projectId/milestones?" +
                              "iids%5b%5d=3&iids%5b%5d=4&" +
                              "state=active&" +
                              "search=filter");
        }
    }
}
