using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Models.Merges;
using Xunit;
using static GitLabApiClient.Test.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    public class MergeRequestClientTest : IAsyncLifetime
    {
        private readonly MergeRequestsClient _sut = new MergeRequestsClient(
            GetFacade(), new MergeRequestsQueryBuilder(), new ProjectMergeRequestsQueryBuilder());

        [Fact]
        public async Task CreatedMergeRequestCanBeRetrieved()
        {
            var mergeRequest = await _sut.CreateAsync(new CreateMergeRequest(TestProjectId, "sourceBranch", "master", "Title")
            {
                Description = "Description",
                Labels = "Label1"
            });

            mergeRequest.Should().Match(Assert());

            var retrievedMergeRequest = (await _sut.GetAsync(TestProjectId)).Single();

            retrievedMergeRequest.Should().Match(Assert());

            Expression<Func<MergeRequest, bool>> Assert()
            {
                return m =>
                    m.Title == "Title" &&
                    m.Description == "Description" &&
                    m.Labels.Contains("Label1") &&
                    m.ProjectId == TestProjectId &&
                    m.SourceBranch == "sourceBranch" &&
                    m.State == "opened" &&
                    m.TargetBranch == "master";
            }
        }

        public Task InitializeAsync() => DeleteAllMergeRequests();

        public Task DisposeAsync() => DeleteAllMergeRequests();

        private async Task DeleteAllMergeRequests()
        {
            var mergeRequests = await _sut.GetAsync(TestProjectId);
            await Task.WhenAll(mergeRequests.Select(
                m => _sut.DeleteAsync(TestProjectId, m.Iid)));
        }
    }
}
