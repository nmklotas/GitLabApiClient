using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using FakeItEasy;
using FluentAssertions;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Http.Serialization;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Test.TestUtilities;
using Xunit;

namespace GitLabApiClient.Test
{
    [ExcludeFromCodeCoverage]
    public class CommitsClientTest
    {
        [Fact]
        public async void GetCommitBySha()
        {
            string gitlabServer = "http://fake-gitlab.com/";
            string projectId = "id";
            string sha = "6104942438c14ec7bd21c6cd5bd995272b3faff6";
            string url = $"/projects/{projectId}/repository/commits/{sha}";

            var handler = A.Fake<MockHandler>(opt => opt.CallsBaseMethods());
            A.CallTo(() => handler.SendAsync(HttpMethod.Get, url))
                .ReturnsLazily(() => HttpResponseMessageProducer.Success(
                    $"{{\"id\": \"{sha}\", }}"));
            using (var client = new HttpClient(handler) {BaseAddress = new Uri(gitlabServer)})
            {
                var gitlabHttpFacade = new GitLabHttpFacade(new RequestsJsonSerializer(), client);
                var commitsClient = new CommitsClient(gitlabHttpFacade, new CommitQueryBuilder());
                
                var commitFromClient = await commitsClient.GetAsync(projectId, sha);
                commitFromClient.Id.Should().BeEquivalentTo(sha);
            }
        }
        
        [Fact]
        public async void GetCommitsByRefName()
        {
            string gitlabServer = "http://fake-gitlab.com/";
            string projectId = "id";
            string refName = "6104942438c14ec7bd21c6cd5bd995272b3faff6";
            string url = $"/projects/id/repository/commits?id=id&ref_name={refName}&per_page=100&page=1";

            var handler = A.Fake<MockHandler>(opt => opt.CallsBaseMethods());
            A.CallTo(() => handler.SendAsync(HttpMethod.Get, url))
                .ReturnsLazily(() => HttpResponseMessageProducer.Success(
                    $"[  {{ \"id\": \"id1\",}},\n  {{\"id\": \"id2\",}}]"));
            using (var client = new HttpClient(handler) {BaseAddress = new Uri(gitlabServer)})
            {
                var gitlabHttpFacade = new GitLabHttpFacade(new RequestsJsonSerializer(), client);
                var commitsClient = new CommitsClient(gitlabHttpFacade, new CommitQueryBuilder());
                
                var commitsFromClient = await commitsClient.GetAsync(projectId, o => o.RefName = refName);
                commitsFromClient[0].Id.Should().BeEquivalentTo("id1");
                commitsFromClient[1].Id.Should().BeEquivalentTo("id2");
            }
            
        }
    }
}
