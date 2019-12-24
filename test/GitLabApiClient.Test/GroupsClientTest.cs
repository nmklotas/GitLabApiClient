using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class GroupsClientTest
    {
        private readonly List<int> _groupIdsToClean = new List<int>();
        private List<int> _milestoneIdsToClean { get; } = new List<int>();

        private readonly GroupsClient _sut = new GroupsClient(
            GetFacade(),
            new GroupsQueryBuilder(),
            new ProjectsGroupQueryBuilder(),
            new MilestonesQueryBuilder());

        [Fact]
        public async Task GroupCanBeRetrievedByGroupId()
        {
            var group = await _sut.GetAsync(TestGroupName);
            group.FullName.Should().Be(TestGroupName);
            group.FullPath.Should().Be(TestGroupName);
            group.Name.Should().Be(TestGroupName);
            group.Path.Should().Be(TestGroupName);
            group.Visibility.Should().Be(GroupsVisibility.Public);
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
            var group = await _sut.SearchAsync(TestGroupName);
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
        public async Task GroupMilestonesCanBeRetrieved()
        {
            //arrange
            var createdMilestone = await _sut.CreateMilestoneAsync(TestGroupTextId, new CreateGroupMilestoneRequest("milestone1")
            {
                StartDate = "2018-11-01",
                DueDate = "2018-11-30",
                Description = "description1"
            });
            _milestoneIdsToClean.Add(createdMilestone.Id);

            //act
            var milestones = await _sut.GetMilestonesAsync(TestGroupId);
            var milestone = await _sut.GetMilestoneAsync(TestGroupId, createdMilestone.Id);

            //assert
            milestones.Should().NotBeEmpty();
            milestone.Should().Match<Milestone>(m =>
                m.GroupId == TestGroupId &&
                m.Title == "milestone1" &&
                m.StartDate == "2018-11-01" &&
                m.DueDate == "2018-11-30" &&
                m.Description == "description1");
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

            var updateRequest = new UpdateGroupRequest()
            {
                Name = updateGroupName,
                Description = "description2",
                Visibility = GroupsVisibility.Internal,
                LfsEnabled = false,
                RequestAccessEnabled = false
            };

            var updateGroupResponse = await _sut.UpdateAsync(createGroupResponse.Id, updateRequest);
            updateGroupResponse.Name.Should().Be(updateGroupName);
            updateGroupResponse.Description.Should().Be("description2");
            updateGroupResponse.Visibility.Should().Be(GroupsVisibility.Internal);
            updateGroupResponse.LfsEnabled.Should().BeFalse();
            updateGroupResponse.RequestAccessEnabled.Should().BeFalse();
        }

        [Fact]
        public async Task CreatedGroupMilestoneCanBeUpdated()
        {
            //arrange
            var createdMilestone = await _sut.CreateMilestoneAsync(TestGroupTextId, new CreateGroupMilestoneRequest("milestone2")
            {
                StartDate = "2018-11-01",
                DueDate = "2018-11-30",
                Description = "description2"
            });
            _milestoneIdsToClean.Add(createdMilestone.Id);

            //act
            var updatedMilestone = await _sut.UpdateMilestoneAsync(TestGroupTextId, createdMilestone.Id, new UpdateGroupMilestoneRequest()
            {
                Title = "milestone22",
                StartDate = "2018-11-05",
                DueDate = "2018-11-10",
                Description = "description22"
            });

            //assert
            updatedMilestone.Should().Match<Milestone>(m =>
                m.GroupId == TestGroupId &&
                m.Title == "milestone22" &&
                m.StartDate == "2018-11-05" &&
                m.DueDate == "2018-11-10" &&
                m.Description == "description22");
        }

        [Fact]
        public async Task CreatedGroupMilestoneCanBeClosed()
        {
            //arrange
            var createdMilestone = await _sut.CreateMilestoneAsync(TestGroupTextId, new CreateGroupMilestoneRequest("milestone3")
            {
                StartDate = "2018-12-01",
                DueDate = "2018-12-31",
                Description = "description3"
            });
            _milestoneIdsToClean.Add(createdMilestone.Id);

            //act
            var updatedMilestone = await _sut.UpdateMilestoneAsync(TestGroupTextId, createdMilestone.Id, new UpdateGroupMilestoneRequest()
            {
                State = UpdatedMilestoneState.Close
            });

            //assert
            updatedMilestone.Should().Match<Milestone>(i => i.State == MilestoneState.Closed);
        }

        [Fact]
        public Task InitializeAsync()
            => CleanupGroups();

        [Fact]
        public Task DisposeAsync()
            => CleanupGroups();

        private async Task CleanupGroups()
        {
            foreach (int milestoneId in _milestoneIdsToClean)
                await _sut.DeleteMilestoneAsync(TestGroupId, milestoneId);

            foreach (int groupId in _groupIdsToClean)
                await _sut.DeleteAsync(groupId.ToString());
        }

        private static string GetRandomGroupName()
            => "test-gitlabapiclient" + Path.GetRandomFileName();
    }
}
