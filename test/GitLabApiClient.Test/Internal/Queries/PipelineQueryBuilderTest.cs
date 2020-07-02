using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Pipelines;
using GitLabApiClient.Models.Pipelines.Requests;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class PipelineQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new PipelineQueryBuilder();

            string query = sut.Build(
                "https://https://gitlab.com/api/v4/pipelines",
                new PipelineQueryOptions
                {
                    Ref = "feature/test",
                    YamlErrors = true,
                    Sha = "ff11ff11ff11ff11",
                    Status = PipelineStatus.Failed,
                    Scope = PipelineScope.Pending,
                    Order = PipelineOrder.UserId,
                    SortOrder = SortOrder.Ascending
                });

            query.Should().Be("https://https://gitlab.com/api/v4/pipelines?" +
                              "ref=feature%2Ftest" +
                              "&yaml_errors=true" +
                              "&sha=ff11ff11ff11ff11" +
                              "&status=failed" +
                              "&scope=pending" +
                              "&order_by=user_id" +
                              "&sort=asc");
        }
    }
}
