using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using static GitLabApiClient.Test.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    public class ProjectsClientTest
    {
        private readonly ProjectsClient _sut = new ProjectsClient(GetFacade());

        [Fact]
        public async Task ProjectRetrieved()
        {
            var project = await _sut.GetAsync(TestProjectId);
            project.Id.Should().Be(TestProjectId);
        }

        [Fact]
        public async Task ProjectUsersRetrieved()
        {
            var users = await _sut.GetUsers(TestProjectId);
            users.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ProjectRetrievedByName()
        {
            var project = (await _sut.GetAsync("test-gitlabapiclient")).Single();
            project.Id.Should().Be(TestProjectId);
        }
    }
}
