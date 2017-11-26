using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Groups.Requests;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class GroupsClientTest
    {
        private readonly List<int> _groupIdsToClean = new List<int>();

        private readonly GroupsClient _sut = new GroupsClient(
            GetFacade(),
            new GroupsQueryBuilder(),
            new ProjectsGroupQueryBuilder());

        [Fact]
        public async Task GroupCanBeRetrievedByGroupId()
        {
            var group = await _sut.GetAsync(TestGroupName);
            group.FullName.Should().Be(TestGroupName);
            group.FullPath.Should().Be(TestGroupName);
            group.Name.Should().Be(TestGroupName);
            group.Path.Should().Be(TestGroupName);
            group.Visibility.Should().Be(GroupsVisibility.Private);
            group.Description.Should().BeEmpty();
        }

        // [Fact]
        // public async Task ProjectsCanBeRetrievedFromGroup()
        // {
        //     var project = await _sut.GetProjectsAsync(TestGroupName);
        //     project.Should().ContainSingle(s => s.Name == TestProjectName);
        // }

        [Fact]
        public async Task GroupsCanBeRetrievedFromSearch()
        {
            var group = await _sut.SearchAsync("gitlab");
            group.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GroupsCanBeRetrievedFromQuery()
        {
            var group = await _sut.GetAsync(o =>
            {
                o.Search = "gitlab";
                o.Order = GroupsOrder.Name;
                o.Sort = GroupsSort.Descending;
                o.AllAvailable = true;
            });

            group.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GroupCanBeCreated()
        {
            string groupName = GetRandomGroupName();

            var request = new CreateGroupRequest(groupName, groupName)
            {
                Description = "description1",
                Visibility = GroupsVisibility.Public,
                LfsEnabled = true,
                RequestAccessEnabled = true
            };

            var response = await _sut.CreateAsync(request);
            _groupIdsToClean.Add(response.Id);

            response.Name.Should().Be(groupName);
            response.Description.Should().Be("description1");
            response.Visibility.Should().Be(GroupsVisibility.Public);
            response.LfsEnabled.Should().BeTrue();
            response.RequestAccessEnabled.Should().BeTrue();
        }

        [Fact]
        public async Task CreatedGroupCanBeUpdated()
        {
            string groupName = GetRandomGroupName();

            var createGroupRequest = new CreateGroupRequest(groupName, groupName)
            {
                Description = "description1",
                Visibility = GroupsVisibility.Public,
                LfsEnabled = true,
                RequestAccessEnabled = true
            };

            var createGroupResponse = await _sut.CreateAsync(createGroupRequest);
            _groupIdsToClean.Add(createGroupResponse.Id);

            string updateGroupName = GetRandomGroupName();

            var updateRequest = new UpdateGroupRequest(createGroupResponse.Id)
            {
                Name = updateGroupName,
                Description = "description2",
                Visibility = GroupsVisibility.Internal,
                LfsEnabled = false,
                RequestAccessEnabled = false
            };

            var updateGroupResponse = await _sut.UpdateAsync(updateRequest);
            updateGroupResponse.Name.Should().Be(updateGroupName);
            updateGroupResponse.Description.Should().Be("description2");
            updateGroupResponse.Visibility.Should().Be(GroupsVisibility.Internal);
            updateGroupResponse.LfsEnabled.Should().BeFalse();
            updateGroupResponse.RequestAccessEnabled.Should().BeFalse();
        }

        public Task InitializeAsync() 
            => CleanupGroups();

        public Task DisposeAsync() 
            => CleanupGroups();

        private async Task CleanupGroups()
        {
            foreach (int groupId in _groupIdsToClean)
                await _sut.DeleteAsync(groupId.ToString());
        }

        private static string GetRandomGroupName() 
            => "test-gitlabapiclient" + Path.GetRandomFileName();
    }
}
