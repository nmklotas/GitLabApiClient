using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
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

        private readonly ProjectsClient _sut = new ProjectsClient(
            GitLabApiHelper.GetFacade(),
            new ProjectsQueryBuilder());

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

        public Task InitializeAsync() => CleanupProjects();

        public Task DisposeAsync() => CleanupProjects();

        private async Task CleanupProjects()
        {
            foreach (int projectId in ProjectIdsToClean)
                await _sut.DeleteAsync(projectId);
        }

        private static string GetRandomProjectName() => "test-gitlabapiclient" + Path.GetRandomFileName();
    }
}
