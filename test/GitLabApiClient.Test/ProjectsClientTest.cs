using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;
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
            var createdMilestone = await _sut.CreateMilestoneAsync(new CreateProjectMilestoneRequest(GitLabApiHelper.TestProjectTextId, "milestone1")
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
            var createdLabel= await _sut.CreateLabelAsync(new CreateProjectLabelRequest(GitLabApiHelper.TestProjectTextId, "Label 1")
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
        }

        private static string GetRandomProjectName() => "test-gitlabapiclient" + Path.GetRandomFileName();
    }
}
