using System;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Releases.Requests;
using GitLabApiClient.Models.Releases.Responses;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class ReleasesTest
    {
        private readonly ReleaseClient _sut = new ReleaseClient(GetFacade(), new ReleaseQueryBuilder());

        [Fact]
        public async Task CreatedReleaseCanBeUpdated()
        {
            //arrange
            const string testRelease = "v1";
            const string testTagName = "v1.0.0";
            const string description = "Updated Description";
            var createdRelease = await _sut.CreateAsync(TestProjectTextId,
                new CreateReleaseRequest(testRelease, testTagName, TestDescription, DateTime.MinValue) { Ref = "master" });

            //act
            var updatedRelease = await _sut.UpdateAsync(TestProjectTextId, testTagName,
                new UpdateReleaseRequest(testRelease, description, DateTime.MinValue));

            //assert
            updatedRelease.Should().Match<Release>(i =>
                i.ReleaseName == testRelease &&
                i.TagName == testTagName &&
                i.ReleasedAt == DateTime.MinValue &&
                i.Description == description);
        }

        [Fact]
        public async Task CreatedReleaseCanBeFetched()
        {
            //arrange
            const string testRelease = "v2";
            const string testTagName = "v2.0.0";
            var createdRelease = await _sut.CreateAsync(TestProjectTextId,
                new CreateReleaseRequest(testRelease, testTagName, TestDescription, DateTime.MinValue) { Ref = "master" });

            //act
            var fetchedRelease = await _sut.GetAsync(TestProjectTextId, testTagName);

            //assert
            fetchedRelease.Should().Match<Release>(i =>
                i.ReleaseName == testRelease &&
                i.TagName == testTagName &&
                i.ReleasedAt == DateTime.MinValue);
        }

        [Fact]
        public async Task CreatedReleaseCanBeListed()
        {
            //arrange
            const string testRelease = "v3";
            const string testTagName = "v3.0.0";

            var createdRelease = await _sut.CreateAsync(TestProjectTextId,
                new CreateReleaseRequest(testRelease, testTagName, TestDescription, DateTime.MinValue) { Ref = "master" });

            //act
            var releaseList = await _sut.GetAsync(TestProjectTextId);

            //assert
            releaseList.Should().Contain(i =>
                i.ReleaseName == testRelease &&
                i.TagName == testTagName &&
                i.ReleasedAt == DateTime.MinValue);
        }

        [Fact]
        public async Task CreatedReleaseCanBeDeleted()
        {
            //arrange
            const string testRelease = "v4";
            const string testTagName = "v4.0.0";
            var createdRelease = await _sut.CreateAsync(TestProjectTextId,
                new CreateReleaseRequest(testRelease, testTagName, TestDescription, DateTime.MinValue) { Ref = "master" });

            //act
            await _sut.DeleteAsync(TestProjectTextId, testTagName);

            //assert
            var fetchedReleases = await _sut.GetAsync(TestProjectTextId);
            fetchedReleases.Should().BeEmpty();
        }
    }
}
