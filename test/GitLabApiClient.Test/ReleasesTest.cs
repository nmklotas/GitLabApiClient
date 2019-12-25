using FluentAssertions;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Releases.Requests;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;
using GitLabApiClient.Models.Releases.Responses;
using System;
using System.Collections.Generic;

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
            var createdRelease = await _sut.CreateAsync(TestProjectTextId, new CreateReleaseRequest(TestRelease, TestTagName, TestDescription, DateTime.MinValue));

            //act
            var updatedRelease = await _sut.UpdateAsync(TestProjectTextId, new UpdateReleaseRequest(TestRelease, TestTagName, "Updated Description", DateTime.MinValue));

            //assert
            updatedRelease.Should().Match<Release>(i =>
                i.ReleaseName == TestRelease &&
                i.TagName == TestTagName &&
                i.ReleasedAt == DateTime.MinValue);
        }

        [Fact]
        public async Task CreatedReleaseCanBeFetched()
        {
            //arrange
            var createdRelease = await _sut.CreateAsync(TestProjectTextId, new CreateReleaseRequest(TestRelease, TestTagName, TestDescription, DateTime.MinValue));

            //act
            var fetchedRelease = await _sut.GetAsync(TestProjectTextId, TestTagName);

            //assert
            fetchedRelease.Should().Match<Release>(i =>
                i.ReleaseName == TestRelease &&
                i.TagName == TestTagName &&
                i.ReleasedAt == DateTime.MinValue);
        }

        [Fact]
        public async Task CreatedReleaseCanBeListed()
        {
            //arrange
            var createdRelease = await _sut.CreateAsync(TestProjectTextId, new CreateReleaseRequest(TestRelease, TestTagName, TestDescription, DateTime.MinValue));

            //act
            var releaseList = await _sut.GetAsync(TestProjectTextId);

            //assert
            releaseList[0].Should().Match<Release>(i =>
                i.ReleaseName == TestRelease &&
                i.TagName == TestTagName &&
                i.ReleasedAt == DateTime.MinValue);
        }

        [Fact]
        public async Task CreatedReleaseCanBeDeleted()
        {
            //arrange
            var createdRelease = await _sut.CreateAsync(TestProjectTextId, new CreateReleaseRequest(TestRelease, TestTagName, TestDescription, DateTime.MinValue));

            //act
            await _sut.DeleteAsync(TestProjectTextId, TestTagName);

            //assert
            var fetcheRelease = await _sut.GetAsync(TestProjectTextId, TestTagName);
            fetcheRelease.Should().BeNull();
        }

    }
}
