using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Variables.Request;
using GitLabApiClient.Models.Variables.Response;
using GitLabApiClient.Test.Utilities;
using Xunit;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class ProjectsClientTest : IAsyncLifetime
    {
        private List<int> ProjectIdsToClean { get; } = new List<int>();
        private List<int> MilestoneIdsToClean { get; } = new List<int>();
        private List<string> VariableIdsToClean { get; } = new List<string>();

        private readonly ProjectsClient _sut = new ProjectsClient(
            GitLabApiHelper.GetFacade(),
            new ProjectsQueryBuilder(),
            new MilestonesQueryBuilder());

        [Fact]
        public async Task ProjectRetrieved()
        {
            var project = await _sut.GetAsync(GitLabApiHelper.TestProjectId);
            project.Id.Should().Be(GitLabApiHelper.TestProjectId);
        }

        [Fact]
        public async Task ProjectUsersRetrieved()
        {
            var users = await _sut.GetUsersAsync(GitLabApiHelper.TestProjectId);
            users.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ProjectLabelsRetrieved()
        {
            var labels = await _sut.GetLabelsAsync(GitLabApiHelper.TestProjectId);
            labels.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ProjectMilestonesRetrieved()
        {
            //arrange
            var createdMilestone = await _sut.CreateMilestoneAsync(
                new CreateProjectMilestoneRequest(GitLabApiHelper.TestProjectTextId, "milestone1")
                {
                    StartDate = "2018-11-01",
                    DueDate = "2018-11-30",
                    Description = "description1"
                });
            MilestoneIdsToClean.Add(createdMilestone.Id);

            //act
            var milestones = await _sut.GetMilestonesAsync(GitLabApiHelper.TestProjectId);
            var milestone = await _sut.GetMilestoneAsync(GitLabApiHelper.TestProjectId, createdMilestone.Id);

            //assert
            milestones.Should().NotBeEmpty();
            milestone.Should().Match<Milestone>(m =>
                m.ProjectId == GitLabApiHelper.TestProjectId &&
                m.Title == "milestone1" &&
                m.StartDate == "2018-11-05" &&
                m.DueDate == "2018-11-10" &&
                m.Description == "description1");
        }

        [Fact]
        public async Task ProjectVariablesRetrieved()
        {
            //arrange
            var createdVariable = await _sut.CreateVariableAsync(new CreateVariableRequest
            {
                ProjectId = GitLabApiHelper.TestProjectId.ToString(),
                VariableType = "env_var",
                Key = "SOME_VAR_KEY_RETRIEVE",
                Value = "VALUE_VAR",
                EnvironmentScope = "*",
                Masked = true,
                Protected = true
            });

            VariableIdsToClean.Add(createdVariable.Key);

            //act
            var variables = await _sut.GetVariablesAsync(GitLabApiHelper.TestProjectId);
            var variable = variables.First(v => v.Key == createdVariable.Key);

            //assert
            variables.Should().NotBeEmpty();
            variable.Should().Match<Variable>(v =>
                v.VariableType == createdVariable.VariableType &&
                v.Key == createdVariable.Key &&
                v.Value == createdVariable.Value &&
                v.EnvironmentScope == createdVariable.EnvironmentScope &&
                v.Masked == createdVariable.Masked &&
                v.Protected == createdVariable.Protected);
        }

        [Fact]
        public async Task ProjectRetrievedByName()
        {
            var projects = await _sut.GetAsync(
                o => o.Filter = GitLabApiHelper.TestProjectName);
            projects.Should().ContainSingle().Which.Id.Should().Be(GitLabApiHelper.TestProjectId);
        }


        [Fact]
        public async Task ProjectCreated()
        {
            var createRequest = CreateProjectRequest.FromName(GetRandomProjectName());
            createRequest.Description = "description1";
            createRequest.EnableContainerRegistry = true;
            createRequest.EnableIssues = true;
            createRequest.EnableJobs = true;
            createRequest.EnableMergeRequests = true;
            createRequest.PublicJobs = true;
            createRequest.EnableWiki = true;
            createRequest.EnableLfs = true;
            createRequest.EnablePrintingMergeRequestLink = true;
            createRequest.OnlyAllowMergeIfAllDiscussionsAreResolved = true;
            createRequest.OnlyAllowMergeIfPipelineSucceeds = true;
            createRequest.Visibility = ProjectVisibilityLevel.Internal;

            var project = await _sut.CreateAsync(createRequest);
            project.Should().Match<Project>(
                p => p.Description == "description1" &&
                     p.ContainerRegistryEnabled &&
                     p.IssuesEnabled &&
                     p.JobsEnabled &&
                     p.MergeRequestsEnabled &&
                     p.PublicJobs &&
                     p.WikiEnabled &&
                     p.OnlyAllowMergeIfAllDiscussionsAreResolved == true &&
                     p.OnlyAllowMergeIfPipelineSucceeds == true &&
                     p.Visibility == ProjectVisibilityLevel.Internal);

            ProjectIdsToClean.Add(project.Id);
        }

        [Fact]
        public async Task ProjectVariablesCreated()
        {
            var request = new CreateVariableRequest(){};
            request.ProjectId = GitLabApiHelper.TestProjectId.ToString();
            request.VariableType = "env_var";
            request.Key = "SOME_VAR_KEY_CREATED";
            request.Value = "VALUE_VAR";
            request.EnvironmentScope = "*";
            request.Masked = true;
            request.Protected = true;
            

            var variable = await _sut.CreateVariableAsync(request);

            variable.Should().Match<Variable>(v => v.VariableType == request.VariableType
                                                   && v.Key == request.Key
                                                   && v.Value == request.Value
                                                   && v.EnvironmentScope == request.EnvironmentScope
                                                   && v.Masked == request.Masked
                                                   && v.Protected == request.Protected);

            VariableIdsToClean.Add(request.Key);
        }

        [Fact]
        public async Task CreatedProjectCanBeUpdated()
        {
            var createRequest = CreateProjectRequest.FromName(GetRandomProjectName());
            createRequest.Description = "description1";
            createRequest.EnableContainerRegistry = true;
            createRequest.EnableIssues = true;
            createRequest.EnableJobs = true;
            createRequest.EnableMergeRequests = true;
            createRequest.PublicJobs = true;
            createRequest.EnableWiki = true;
            createRequest.EnableLfs = true;
            createRequest.EnablePrintingMergeRequestLink = true;
            createRequest.OnlyAllowMergeIfAllDiscussionsAreResolved = true;
            createRequest.OnlyAllowMergeIfPipelineSucceeds = true;
            createRequest.Visibility = ProjectVisibilityLevel.Internal;

            var createdProject = await _sut.CreateAsync(createRequest);
            ProjectIdsToClean.Add(createdProject.Id);

            var updatedProject = await _sut.UpdateAsync(new UpdateProjectRequest(createdProject.Id.ToString(), createdProject.Name)
            {
                Description = "description11",
                EnableContainerRegistry = false,
                EnableIssues = false,
                EnableJobs = false,
                EnableMergeRequests = false,
                PublicJobs = false,
                EnableWiki = false,
                EnableLfs = false,
                OnlyAllowMergeIfAllDiscussionsAreResolved = false,
                OnlyAllowMergeIfPipelineSucceeds = false,
                Visibility = ProjectVisibilityLevel.Public
            });

            updatedProject.Should().Match<Project>(
                p => p.Description == "description11" &&
                     !p.ContainerRegistryEnabled &&
                     !p.IssuesEnabled &&
                     !p.JobsEnabled &&
                     !p.MergeRequestsEnabled &&
                     p.PublicJobs &&
                     !p.WikiEnabled &&
                     p.OnlyAllowMergeIfAllDiscussionsAreResolved == false &&
                     p.OnlyAllowMergeIfPipelineSucceeds == false &&
                     p.Visibility == ProjectVisibilityLevel.Public);
        }

        [Fact]
        public async Task CreatedProjectLabelCanBeUpdated()
        {
            //arrange
            var createdLabel = await _sut.CreateLabelAsync(new CreateProjectLabelRequest(GitLabApiHelper.TestProjectTextId, "Label 1")
            {
                Color = "#FFFFFF",
                Description = "description1",
                Priority = 1
            });

            //act
            var updateRequest = UpdateProjectLabelRequest.FromNewName(GitLabApiHelper.TestProjectTextId, createdLabel.Name, "Label 11");
            updateRequest.Color = "#000000";
            updateRequest.Description = "description11";
            updateRequest.Priority = 11;

            var updatedLabel = await _sut.UpdateLabelAsync(updateRequest);
            await _sut.DeleteLabelAsync(GitLabApiHelper.TestProjectId, updatedLabel.Name);

            //assert
            updatedLabel.Should().Match<Label>(l =>
                l.Name == "Label 11" &&
                l.Color == "#000000" &&
                l.Description == "description11" &&
                l.Priority == 11);
        }

        [Fact]
        public async Task CreatedProjectMilestoneCanBeUpdated()
        {
            //arrange
            var createdMilestone = await _sut.CreateMilestoneAsync(new CreateProjectMilestoneRequest(GitLabApiHelper.TestProjectTextId, "milestone2")
            {
                StartDate = "2018-11-01",
                DueDate = "2018-11-30",
                Description = "description2"
            });
            MilestoneIdsToClean.Add(createdMilestone.Id);

            //act
            var updatedMilestone = await _sut.UpdateMilestoneAsync(new UpdateProjectMilestoneRequest(GitLabApiHelper.TestProjectTextId, createdMilestone.Id)
            {
                Title = "milestone22",
                StartDate = "2018-11-05",
                DueDate = "2018-11-10",
                Description = "description22"
            });

            //assert
            updatedMilestone.Should().Match<Milestone>(m =>
                m.ProjectId == GitLabApiHelper.TestProjectId &&
                m.Title == "milestone22" &&
                m.StartDate == "2018-11-05" &&
                m.DueDate == "2018-11-10" &&
                m.Description == "description22");
        }

        [Fact]
        public async Task ProjectVariableCanBeUpdated()
        {
            var request = new CreateVariableRequest
            {
                ProjectId = GitLabApiHelper.TestProjectId.ToString(),
                VariableType = "env_var",
                Key = "SOME_VAR_KEY_TO_UPDATE",
                Value = "VALUE_VAR",
                EnvironmentScope = "*",
                Masked = true,
                Protected = true
            };

            var variable = await _sut.CreateVariableAsync(request);

            VariableIdsToClean.Add(request.Key);

            var updateRequest = new UpdateProjectVariableRequest
            {
                ProjectId = request.ProjectId,
                VariableType = "file",
                Key = request.Key,
                Value = "UpdatedValue",
                EnvironmentScope = "*",
                Masked = request.Masked,
                Protected = request.Protected,
            };

            var variableUpdated = await _sut.UpdateVariableAsync(updateRequest);

            variableUpdated.Should().Match<Variable>(v => v.VariableType == updateRequest.VariableType
                                                          && v.Key == updateRequest.Key
                                                          && v.Value == updateRequest.Value
                                                          && v.EnvironmentScope == updateRequest.EnvironmentScope
                                                          && v.Masked == updateRequest.Masked
                                                          && v.Protected == updateRequest.Protected);
        }

        [Fact]
        public async Task CreatedProjectMilestoneCanBeClosed()
        {
            //arrange
            var createdMilestone = await _sut.CreateMilestoneAsync(new CreateProjectMilestoneRequest(GitLabApiHelper.TestProjectTextId, "milestone3")
            {
                StartDate = "2018-12-01",
                DueDate = "2018-12-31",
                Description = "description3"
            });
            MilestoneIdsToClean.Add(createdMilestone.Id);

            //act
            var updatedMilestone = await _sut.UpdateMilestoneAsync(new UpdateProjectMilestoneRequest(GitLabApiHelper.TestProjectTextId, createdMilestone.Id)
            {
                State = UpdatedMilestoneState.Close
            });

            //assert
            updatedMilestone.Should().Match<Milestone>(i => i.State == MilestoneState.Closed);
        }

        public Task InitializeAsync() => CleanupProjects();

        public Task DisposeAsync() => CleanupProjects();

        private async Task CleanupProjects()
        {

            foreach (int milestoneId in MilestoneIdsToClean)
                await _sut.DeleteMilestoneAsync(GitLabApiHelper.TestProjectId, milestoneId);

            foreach (int projectId in ProjectIdsToClean)
                await _sut.DeleteAsync(projectId);

            foreach (string variableId in VariableIdsToClean)
                await _sut.DeleteVariableAsync(GitLabApiHelper.TestProjectId, variableId);
        }

        private static string GetRandomProjectName() => "test-gitlabapiclient" + Path.GetRandomFileName();
    }
}
