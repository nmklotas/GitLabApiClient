using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            new MilestonesQueryBuilder(),
            new JobQueryBuilder());

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
            //arrange
            var createdLabel = await _sut.CreateLabelAsync(GitLabApiHelper.TestProjectId, new CreateProjectLabelRequest("Label 5")
            {
                Color = "#FFFFFF",
                Description = "description5",
                Priority = 1
            });

            var labels = await _sut.GetLabelsAsync(GitLabApiHelper.TestProjectId);
            labels.Should().NotBeEmpty();
            await _sut.DeleteLabelAsync(GitLabApiHelper.TestProjectId, createdLabel.Name);
        }

        [Fact]
        public async Task ProjectMilestonesRetrieved()
        {
            //arrange
            var createdMilestone = await _sut.CreateMilestoneAsync(GitLabApiHelper.TestProjectTextId, new CreateProjectMilestoneRequest("milestone6")
            {
                StartDate = "2018-11-01",
                DueDate = "2018-11-30",
                Description = "description6"
            });
            MilestoneIdsToClean.Add(createdMilestone.Id);

            //act
            var milestones = await _sut.GetMilestonesAsync(GitLabApiHelper.TestProjectId);
            var milestone = await _sut.GetMilestoneAsync(GitLabApiHelper.TestProjectId, createdMilestone.Id);

            //assert
            milestones.Should().NotBeEmpty();
            milestone.Should().Match<Milestone>(m =>
                m.ProjectId == GitLabApiHelper.TestProjectId &&
                m.Title == "milestone6" &&
                m.StartDate == "2018-11-01" &&
                m.DueDate == "2018-11-30" &&
                m.Description == "description6");
        }

        [Fact]
        public async Task ProjectVariablesRetrieved()
        {
            //arrange
            var createdVariable = await _sut.CreateVariableAsync(GitLabApiHelper.TestProjectId, new CreateVariableRequest
            {
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
        public async Task ProjectRunnerCanBeRetrieved()
        {
            //act
            var runners = await _sut.GetRunnersAsync(GitLabApiHelper.TestProjectId);

            //assert
            runners.Count.Should().BeGreaterOrEqualTo(1);
            runners.Should().Contain(r => r.Description == GitLabApiHelper.TestProjectRunnerName && r.Active == true);
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
            var request = new CreateVariableRequest
            {
                VariableType = "env_var",
                Key = "SOME_VAR_KEY_CREATED",
                Value = "VALUE_VAR",
                EnvironmentScope = "*",
                Masked = true,
                Protected = true
            };

            var variable = await _sut.CreateVariableAsync(GitLabApiHelper.TestProjectId, request);

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

            var updatedProject = await _sut.UpdateAsync(createdProject.Id.ToString(), new UpdateProjectRequest(createdProject.Name)
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
            var createdLabel = await _sut.CreateLabelAsync(GitLabApiHelper.TestProjectTextId, new CreateProjectLabelRequest("Label 1")
            {
                Color = "#FFFFFF",
                Description = "description1",
                Priority = 1
            });

            //act
            var updateRequest = UpdateProjectLabelRequest.FromNewName(createdLabel.Name, "Label 11");
            updateRequest.Color = "#000000";
            updateRequest.Description = "description11";
            updateRequest.Priority = 11;

            var updatedLabel = await _sut.UpdateLabelAsync(GitLabApiHelper.TestProjectTextId, updateRequest);
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
            var createdMilestone = await _sut.CreateMilestoneAsync(GitLabApiHelper.TestProjectTextId, new CreateProjectMilestoneRequest("milestone4")
            {
                StartDate = "2018-11-01",
                DueDate = "2018-11-30",
                Description = "description4"
            });
            MilestoneIdsToClean.Add(createdMilestone.Id);

            //act
            var updatedMilestone = await _sut.UpdateMilestoneAsync(GitLabApiHelper.TestProjectTextId, createdMilestone.Id, new UpdateProjectMilestoneRequest()
            {
                Title = "milestone23",
                StartDate = "2018-11-05",
                DueDate = "2018-11-10",
                Description = "description23"
            });

            //assert
            updatedMilestone.Should().Match<Milestone>(m =>
                m.ProjectId == GitLabApiHelper.TestProjectId &&
                m.Title == "milestone23" &&
                m.StartDate == "2018-11-05" &&
                m.DueDate == "2018-11-10" &&
                m.Description == "description23");
        }

        [Fact]
        public async Task ProjectVariableCanBeUpdated()
        {
            var request = new CreateVariableRequest
            {
                VariableType = "env_var",
                Key = "SOME_VAR_KEY_TO_UPDATE",
                Value = "VALUE_VAR",
                EnvironmentScope = "*",
                Masked = true,
                Protected = true
            };

            var variable = await _sut.CreateVariableAsync(GitLabApiHelper.TestProjectId, request);

            VariableIdsToClean.Add(request.Key);

            var updateRequest = new UpdateProjectVariableRequest
            {
                VariableType = "file",
                Key = request.Key,
                Value = "UpdatedValue",
                EnvironmentScope = "*",
                Masked = request.Masked,
                Protected = request.Protected,
            };

            var variableUpdated = await _sut.UpdateVariableAsync(GitLabApiHelper.TestProjectId, updateRequest);

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
            var createdMilestone = await _sut.CreateMilestoneAsync(GitLabApiHelper.TestProjectTextId, new CreateProjectMilestoneRequest("milestone5")
            {
                StartDate = "2018-12-01",
                DueDate = "2018-12-31",
                Description = "description5"
            });
            MilestoneIdsToClean.Add(createdMilestone.Id);

            //act
            var updatedMilestone = await _sut.UpdateMilestoneAsync(GitLabApiHelper.TestProjectTextId, createdMilestone.Id, new UpdateProjectMilestoneRequest()
            {
                State = UpdatedMilestoneState.Close
            });

            //assert
            updatedMilestone.Should().Match<Milestone>(i => i.State == MilestoneState.Closed);
        }

        [Fact]
        public async Task ExportImportProject()
        {
            var status = await _sut.GetExportStatusAsync(GitLabApiHelper.TestProjectId);
            status.Status.Should().Be(ExportStatusEnum.None);

            await _sut.ExportAsync(GitLabApiHelper.TestProjectId);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (status.Status == ExportStatusEnum.None)
            {
                stopwatch.Elapsed.TotalMilliseconds.Should().BeLessThan(new TimeSpan(0, 1, 0).TotalMilliseconds);

                await Task.Delay(5000);
                status = await _sut.GetExportStatusAsync(GitLabApiHelper.TestProjectId);
            }
            while (status.Status != ExportStatusEnum.None && status.Status != ExportStatusEnum.Finished)
            {
                await Task.Delay(5000);
                status = await _sut.GetExportStatusAsync(GitLabApiHelper.TestProjectId);
            }

            status.Status.Should().Be(ExportStatusEnum.Finished);

            var path = System.IO.Path.GetTempFileName();
            await _sut.ExportDownloadAsync(GitLabApiHelper.TestProjectId, path);

            System.IO.File.Exists(path).Should().BeTrue();
            var projectFileInfo = new System.IO.FileInfo(path);
            projectFileInfo.Length.Should().BeGreaterThan(0);

            var req = ImportProjectRequest.FromFile("project_import_test", path);
            var importProject = await _sut.ImportAsync(req);
            importProject.Path.Should().Be("project_import_test");

            var importStatus = await _sut.GetImportStatusAsync(importProject.Id);
            importStatus.Status.Should().NotBe(ImportStatusEnum.None);

            System.IO.File.Delete(path);
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
