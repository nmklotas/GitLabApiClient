using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Groups.Responses;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
using GitLabApiClient.Models.Runners.Responses;
using GitLabApiClient.Test.Utilities;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class GroupsClientTest
    {
        private readonly List<int> _groupIdsToClean = new List<int>();
        private List<int> MilestoneIdsToClean { get; } = new List<int>();
        private List<string> VariableIdsToClean { get; } = new List<string>();

        private readonly GroupsClient _sut = new GroupsClient(
            GetFacade(),
            new GroupsQueryBuilder(),
            new ProjectsGroupQueryBuilder(),
            new MilestonesQueryBuilder(),
            new GroupLabelsQueryBuilder());

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
                o.Search = "txxxest";
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
            MilestoneIdsToClean.Add(createdMilestone.Id);

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
        public async Task GroupRunnerCanBeRetrieved()
        {
            //act
            var runners = await _sut.GetRunnersAsync(TestGroupId);

            //assert
            runners.Count.Should().Be(1);
            runners[0].Should().Match<Runner>(r =>
                r.Description == TestGroupRunnerName &&
                r.Active == true);
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
        public async Task CreatedGroupLabelCanBeUpdated()
        {
            //arrange
            var createdLabel = await _sut.CreateLabelAsync(GitLabApiHelper.TestGroupId, new CreateGroupLabelRequest("Label 1")
            {
                Color = "#FFFFFF",
                Description = "description1"
            });

            //act
            var updateRequest = UpdateGroupLabelRequest.FromNewName(createdLabel.Name, "Label 11");
            updateRequest.Color = "#000000";
            updateRequest.Description = "description11";

            var updatedLabel = await _sut.UpdateLabelAsync(GitLabApiHelper.TestGroupId, updateRequest);
            await _sut.DeleteLabelAsync(GitLabApiHelper.TestGroupId, updatedLabel.Name);

            //assert
            updatedLabel.Should().Match<GroupLabel>(l =>
                l.Name == "Label 11" &&
                l.Color == "#000000" &&
                l.Description == "description11");
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
            MilestoneIdsToClean.Add(createdMilestone.Id);

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
            MilestoneIdsToClean.Add(createdMilestone.Id);

            //act
            var updatedMilestone = await _sut.UpdateMilestoneAsync(TestGroupTextId, createdMilestone.Id, new UpdateGroupMilestoneRequest()
            {
                State = UpdatedMilestoneState.Close
            });

            //assert
            updatedMilestone.Should().Match<Milestone>(i => i.State == MilestoneState.Closed);
        }

        [Fact]
        public async Task CreatedGroupCanHaveMemberAdded()
        {
            //act
            var member = await _sut.AddMemberAsync(TestGroupTextId, new AddGroupMemberRequest(AccessLevel.Developer, TestExtraUserId));

            //assert
            member.Should().NotBeNull();
            member.Id.Should().Be(TestExtraUserId);
        }

        [Fact]
        public async Task GroupVariablesRetrieved()
        {
            //arrange
            var createdVariable = await _sut.CreateVariableAsync(GitLabApiHelper.TestGroupId, new CreateGroupVariableRequest
            {
                VariableType = "env_var",
                Key = "SOME_VAR_KEY_RETRIEVE",
                Value = "VALUE_VAR",
                Masked = true,
                Protected = true
            });

            VariableIdsToClean.Add(createdVariable.Key);

            //act
            var variables = await _sut.GetVariablesAsync(GitLabApiHelper.TestGroupId);
            var variable = variables.First(v => v.Key == createdVariable.Key);

            //assert
            variables.Should().NotBeEmpty();
            variable.Should().Match<Variable>(v =>
                v.VariableType == createdVariable.VariableType &&
                v.Key == createdVariable.Key &&
                v.Value == createdVariable.Value &&
                v.Masked == createdVariable.Masked &&
                v.Protected == createdVariable.Protected);
        }

        [Fact]
        public async Task GroupVariablesCreated()
        {
            var request = new CreateGroupVariableRequest
            {
                VariableType = "env_var",
                Key = "SOME_VAR_KEY_CREATED",
                Value = "VALUE_VAR",
                Masked = true,
                Protected = true
            };

            var variable = await _sut.CreateVariableAsync(GitLabApiHelper.TestGroupId, request);

            variable.Should().Match<Variable>(v => v.VariableType == request.VariableType
                                                   && v.Key == request.Key
                                                   && v.Value == request.Value
                                                   && v.Masked == request.Masked
                                                   && v.Protected == request.Protected);

            VariableIdsToClean.Add(request.Key);
        }

        [Fact]
        public async Task GroupVariableCanBeUpdated()
        {
            var request = new CreateGroupVariableRequest
            {
                VariableType = "env_var",
                Key = "SOME_VAR_KEY_TO_UPDATE",
                Value = "VALUE_VAR",
                Masked = true,
                Protected = true
            };

            var variable = await _sut.CreateVariableAsync(GitLabApiHelper.TestGroupId, request);

            VariableIdsToClean.Add(request.Key);

            var updateRequest = new UpdateGroupVariableRequest
            {
                VariableType = "file",
                Key = request.Key,
                Value = "UpdatedValue",
                Masked = request.Masked,
                Protected = request.Protected,
            };

            var variableUpdated = await _sut.UpdateVariableAsync(GitLabApiHelper.TestGroupId, updateRequest);

            variableUpdated.Should().Match<Variable>(v => v.VariableType == updateRequest.VariableType
                                                          && v.Key == updateRequest.Key
                                                          && v.Value == updateRequest.Value
                                                          && v.Masked == updateRequest.Masked
                                                          && v.Protected == updateRequest.Protected);
        }

        [Fact]
        public Task InitializeAsync()
            => CleanupGroups();

        [Fact]
        public Task DisposeAsync()
            => CleanupGroups();

        private async Task CleanupGroups()
        {
            foreach (int milestoneId in MilestoneIdsToClean)
                await _sut.DeleteMilestoneAsync(TestGroupId, milestoneId);

            foreach (int groupId in _groupIdsToClean)
                await _sut.DeleteAsync(groupId.ToString());

            foreach (string variableId in VariableIdsToClean)
                await _sut.DeleteVariableAsync(GitLabApiHelper.TestGroupId, variableId);
        }

        private static string GetRandomGroupName()
            => "test-gitlabapiclient" + Path.GetRandomFileName();
    }
}
