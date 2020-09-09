using System.Collections.Generic;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Job.Requests;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class JobQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new JobQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/projects/1/jobs",
                new JobQueryOptions
                {
                    Scope = JobScope.Canceled,
                    Scopes = new List<JobScope>() { JobScope.Failed }
                });

            query.Should().BeEquivalentTo("https://gitlab.com/api/v4/projects/1/jobs?" +
                                          "scope%5B%5D=failed&" +
                                          "scope%5B%5D=canceled"
            );
        }

    }
}
