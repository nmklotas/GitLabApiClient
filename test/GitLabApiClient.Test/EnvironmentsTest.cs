using System;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Environments.Requests;
using GitLabApiClient.Models.Environments.Responses;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;
using Environment = GitLabApiClient.Models.Environments.Responses.Environment;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class EnvironmentsTest
    {
        private readonly EnvironmentClient _sut = new EnvironmentClient(GetFacade(), new EnvironmentsQueryBuilder());

        [Fact]
        public async Task CreatedEnvironmentCanBeUpdated()
        {
            //arrange
            const string testEnvironment = "Test Env Name";
            var externalUrl = new Uri("https://dev.testingthis.com");
            var createdEnvironment = await _sut.CreateAsync(TestProjectTextId,
                new CreateEnvironmentRequest(testEnvironment, externalUrl));

            //act
            var updatedExternalUrl = new Uri("https://beta.testingthis.com");
            var updatedEnvironment = await _sut.UpdateAsync(TestProjectTextId, 
                new UpdateEnvironmentRequest(createdEnvironment.Id, updatedExternalUrl));

            //assert
            updatedEnvironment.Should().Match<Environment>(i =>
                i.Name == testEnvironment &&
                i.ExternalUrl == updatedExternalUrl);
        }

        [Fact]
        public async Task CreatedEnvironmentCanBeFetched()
        {
            //arrange
            const string testEnvironment = "Test Env Name";
            var externalUrl = new Uri("https://dev.testingthis.com");
            var createdEnvironment = await _sut.CreateAsync(TestProjectTextId,
                new CreateEnvironmentRequest(testEnvironment, externalUrl));

            //act
            var fetchedEnvironment = await _sut.GetAsync(TestProjectTextId, createdEnvironment.Id);

            //assert
            fetchedEnvironment.Should().Match<Environment>(i =>
                i.Name == testEnvironment &&
                i.ExternalUrl == externalUrl);
        }

        [Fact]
        public async Task CreatedEnvironmentCanBeListed()
        {
            //arrange
            const string testEnvironment = "Test Env Name";
            var externalUrl = new Uri("https://dev.testingthis.com");
            var createdEnvironment = await _sut.CreateAsync(TestProjectTextId,
                new CreateEnvironmentRequest(testEnvironment, externalUrl));

            //act
            var environmentList = await _sut.GetAsync(TestProjectTextId);

            //assert
            environmentList.Should().Contain(i =>
                i.Name == testEnvironment &&
                i.ExternalUrl == externalUrl);
        }

        [Fact]
        public async Task CreatedEnvironmentCanBeStopped()
        {
            //arrange
            const string testEnvironment = "Test Env Name";
            var externalUrl = new Uri("https://dev.testingthis.com");
            var createdEnvironment = await _sut.CreateAsync(TestProjectTextId,
                new CreateEnvironmentRequest(testEnvironment, externalUrl));

            //act
            await _sut.StopAsync(TestProjectTextId, createdEnvironment.Id);

            //assert
            var fetchedEnvironment = await _sut.GetAsync(TestProjectTextId);
            fetchedEnvironment.Should().Contain(i =>
               i.Name == testEnvironment &&
               i.ExternalUrl == externalUrl &&
               i.State == EnvironmentState.Stopped);
        }

        [Fact]
        public async Task CreatedEnvironmentCanBeDeleted()
        {
            //arrange
            const string testEnvironment = "Test Env Name";
            var externalUrl = new Uri("https://dev.testingthis.com");
            var createdEnvironment = await _sut.CreateAsync(TestProjectTextId,
                new CreateEnvironmentRequest(testEnvironment, externalUrl));

            //act
            await _sut.DeleteAsync(TestProjectTextId, createdEnvironment.Id);

            //assert
            var fetchedEnvironment = await _sut.GetAsync(TestProjectTextId);
            fetchedEnvironment.Should().BeEmpty();
        }
    }
}
