using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Models.Runners.Requests;
using GitLabApiClient.Models.Runners.Responses;
using GitLabApiClient.Test.Utilities;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class RunnersClientTest
    {
        private readonly RunnersClient _sut = new RunnersClient(GetFacade());

        [Fact]
        public async Task RunnersCanBeRetrieved()
        {
            var runners = await _sut.GetAsync();

            runners.Count.Should().BeGreaterOrEqualTo(1);
            runners.Should().Contain(r => r.Description == TestProjectRunnerName);
        }

        [Fact]
        public async Task RunnersCanAllBeRetrieved()
        {
            var runners = await _sut.GetAllAsync();

            runners.Count.Should().BeGreaterOrEqualTo(3);
            runners.Should().Contain(r => r.Description == TestRunnerName)
                .And.Contain(r => r.Description == TestProjectRunnerName)
                .And.Contain(r => r.Description == TestGroupRunnerName);
        }

        [Fact]
        public async Task RunnersCanBeRetrievedById()
        {
            var runners = await _sut.GetAsync();
            runners.Count.Should().BeGreaterOrEqualTo(1);
            runners[0].Should().NotBeNull();
            var runner = await _sut.GetAsync(runners[0].Id);

            runner.Should().BeEquivalentTo(runners[0]);
            runner.Projects.Should().Contain(p =>
                p.Id == TestProjectId &&
                p.Path == TestProjectName &&
                p.PathWithNamespace == TestGroupName + "/" + TestProjectName
                );
        }

        [Fact]
        public async Task RunnersCanBeUpdated()
        {
            var request = new CreateRunnerRequest()
            {
                Token = GitLabContainerFixture.RunnerRegistrationToken,
                Description = "RunnersCanBeUpdated",
                Active = false
            };

            var runnerToken = await _sut.CreateAsync(request);
            runnerToken.Should().NotBeNull();

            var updateRequest = new UpdateRunnerRequest()
            {
                Description = request.Description + "Updated",
                TagList = new List<string>() { "updated_tags" },
                Active = true
            };
            await _sut.UpdateAsync(runnerToken.Id, updateRequest);

            var runner = await _sut.GetAsync(runnerToken.Id);
            runner.Should().NotBeNull();

            runner.Should().Match<RunnerDetails>(r =>
                r.Description == updateRequest.Description &&
                r.Active == updateRequest.Active
            );
            runner.TagList.Should().BeEquivalentTo(updateRequest.TagList);
        }

        [Fact]
        public async Task RunnersCanBeCreatedAndRetrievedById()
        {
            var request = new CreateRunnerRequest()
            {
                Token = GitLabContainerFixture.RunnerRegistrationToken,
                Description = "RunnerCanBeCreated",
                Active = false,
                TagList = new List<string> { "tag1", "tag2" }
            };

            var runnerToken = await _sut.CreateAsync(request);
            runnerToken.Should().NotBeNull();

            var runner = await _sut.GetAsync(runnerToken.Id);
            runner.Should().Match<RunnerDetails>(r =>
                r.Description == request.Description &&
                r.Active == request.Active
                );
            runner.TagList.Should().BeEquivalentTo(request.TagList);

        }

        [Fact]
        public async Task RunnersCanBeDeleted()
        {
            var request = new CreateRunnerRequest()
            {
                Token = GitLabContainerFixture.RunnerRegistrationToken,
                Description = "RunnersCanBeDeleted"
            };

            var runnerToken = await _sut.CreateAsync(request);
            runnerToken.Should().NotBeNull();

            await _sut.DeleteAsync(runnerToken.Token);

            try
            {
                var runner = await _sut.GetAsync(runnerToken.Id);
                runner.Should().BeNull();
            }
            catch (GitLabException e)
            {
                e.HttpStatusCode.Should().Be(404);
            }
        }

        [Fact]
        public async Task RunnersCanBeDeletedById()
        {
            var request = new CreateRunnerRequest()
            {
                Token = GitLabContainerFixture.RunnerRegistrationToken,
                Description = "RunnersCanBeDeleted"
            };

            var runnerToken = await _sut.CreateAsync(request);
            runnerToken.Should().NotBeNull();

            await _sut.DeleteAsync(runnerToken.Id);

            try
            {
                var runner = await _sut.GetAsync(runnerToken.Id);
                runner.Should().BeNull();
            }
            catch (GitLabException e)
            {
                e.HttpStatusCode.Should().Be(404);
            }
        }

        [Fact]
        public async Task RunnerCanCheckAuthentication()
        {
            var request = new CreateRunnerRequest()
            {
                Token = GitLabContainerFixture.RunnerRegistrationToken,
                Description = "RunnerCanCheckAuthentication"
            };

            var runnerToken = await _sut.CreateAsync(request);
            runnerToken.Should().NotBeNull();

            bool result = await _sut.VerifyAuthenticationAsync(runnerToken.Token);
            result.Should().BeTrue();

            result = await _sut.VerifyAuthenticationAsync(runnerToken.Token + "fail");
            result.Should().BeFalse();
        }

    }
}
