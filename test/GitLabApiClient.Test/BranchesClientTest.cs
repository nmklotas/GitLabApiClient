using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Branches.Requests;
using GitLabApiClient.Models.Branches.Responses;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class BranchesClientTest
    {
        private readonly BranchClient _sut = new BranchClient(
            GetFacade(), new BranchQueryBuilder());

        [Fact]
        public async Task CreatedBranchCanBeDeleted()
        {
            //arrange
            const string name = "Title1";
            const string source = "main";
            var createdBranch = await _sut.CreateAsync(TestProjectId, new Models.Branches.Requests.CreateBranchRequest(name, source));

            //act
            await _sut.DeleteBranch(TestProjectTextId, createdBranch.Name);

            //assert
            var deletedBranch = await _sut.GetAsync(TestProjectId, name);
            createdBranch.Should().Match<Branch>(i =>
                i.Name == "Title1");

            deletedBranch.Should().BeNull();
        }

        [Fact]
        public async Task CreatedBranchCanBeProtected()
        {
            //arrange
            const string name = "Title2";
            const string source = "main";
            var createdBranch = await _sut.CreateAsync(TestProjectId, new Models.Branches.Requests.CreateBranchRequest(name, source));

            //act
            var protectedBranch = await _sut.ProtectBranchAsync(TestProjectId, new ProtectBranchRequest(name));

            //assert
            protectedBranch.Should().Match<ProtectedBranch>(i => i.Name == name);
        }

        [Fact]
        public async Task CreatedBranchCanBeUnprotected()
        {
            //arrange
            const string name = "Title3";
            const string source = "main";
            var createdBranch = await _sut.CreateAsync(TestProjectId, new Models.Branches.Requests.CreateBranchRequest(name, source));

            //act
            var protectedBranch = await _sut.ProtectBranchAsync(TestProjectId, new ProtectBranchRequest(name));
            await _sut.UnprotectBranchAsync(TestProjectId, name);

            //assert
            protectedBranch.Should().Match<ProtectedBranch>(i => i.Name == name);
            var unprotectedBranch = await _sut.GetProtectedBranchesAsync(TestProjectId, name);
            unprotectedBranch.Should().BeNull();
        }
    }
}
