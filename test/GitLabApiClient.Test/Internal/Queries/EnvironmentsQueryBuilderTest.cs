using System;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Environments.Requests;
using GitLabApiClient.Models.Environments.Responses;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class EnvironmentsQueryBuilderTest
    {
        [Fact]
        public void NameQueryBuilt()
        {
            var sut = new EnvironmentsQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/projects/projectId/environments",
                new EnvironmentsQueryOptions()
                {
                    Name = "Test Env Name",
                    States = EnvironmentState.Available
                });

            query.Should().Be("https://gitlab.com/api/v4/projects/projectId/environments?" +
                              "name=Test%20Env%20Name&" +
                              "states=available");
        }

        [Fact]
        public void SearchQueryBuilt()
        {
            var sut = new EnvironmentsQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/projects/projectId/environments",
                new EnvironmentsQueryOptions()
                {
                    States = EnvironmentState.Available,
                    Search = "filter env"
                });

            query.Should().Be("https://gitlab.com/api/v4/projects/projectId/environments?" +
                              "search=filter%20env&" +
                              "states=available");
        }

        [Fact]
        public void NameAndSearchMutuallyExclusive()
        {
            var sut = new EnvironmentsQueryBuilder();

            Assert.Throws<InvalidOperationException>(()=>
                    sut.Build(
                    "https://gitlab.com/api/v4/projects/projectId/environments",
                    new EnvironmentsQueryOptions()
                    {
                        Name = "Test Env Name",
                        States = EnvironmentState.Available,
                        Search = "filter env"
                    }));
        }
    }
}
