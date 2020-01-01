using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class TreesClientTest
    {
        private readonly TreesClient _sut = new TreesClient(GetFacade(), new TreeQueryBuilder());

        [Fact]
        public async Task GetTrees()
        {
            var trees = await _sut.GetAsync(TestProjectId);
            trees.Should().SatisfyRespectively(
                first =>
                {
                    first.Id.Should().HaveLength(40);
                    first.Mode.Should().Be("040000");
                    first.Type.Should().Be("tree");
                    first.Name.Should().Be("newdir");
                    first.Path.Should().Be("newdir");
                },
                second =>
                {
                    second.Id.Should().HaveLength(40);
                    second.Mode.Should().Be("100644");
                    second.Type.Should().Be("blob");
                    second.Name.Should().Be("README.md");
                    second.Path.Should().Be("README.md");
                });

            var otherTrees = await _sut.GetAsync(TestProjectId, options => options.Reference = "master");
            otherTrees.Should().BeEquivalentTo(trees);
        }

        [Fact]
        public async Task GetTreesInFolder()
        {
            var trees = await _sut.GetAsync(TestProjectId, options => options.Path = "newdir/");
            trees.Should().SatisfyRespectively(
                first =>
                {
                    first.Id.Should().HaveLength(40);
                    first.Mode.Should().Be("100644");
                    first.Type.Should().Be("blob");
                    first.Name.Should().Be(".gitkeep");
                    first.Path.Should().Be("newdir/.gitkeep");
                });
        }

        [Fact]
        public async Task GetTreesRecursively()
        {
            var trees = await _sut.GetAsync(TestProjectId, options =>
            {
                options.Recursive = true;
                options.Reference = "master";
            });
            trees.Should().SatisfyRespectively(
                first =>
                {
                    first.Id.Should().HaveLength(40);
                    first.Mode.Should().Be("040000");
                    first.Type.Should().Be("tree");
                    first.Name.Should().Be("newdir");
                    first.Path.Should().Be("newdir");
                },
                second =>
                {
                    second.Id.Should().HaveLength(40);
                    second.Mode.Should().Be("100644");
                    second.Type.Should().Be("blob");
                    second.Name.Should().Be("README.md");
                    second.Path.Should().Be("README.md");
                },
                third =>
                {
                    third.Id.Should().HaveLength(40);
                    third.Mode.Should().Be("100644");
                    third.Type.Should().Be("blob");
                    third.Name.Should().Be(".gitkeep");
                    third.Path.Should().Be("newdir/.gitkeep");
                });
        }
    }
}
