using System;
using System.Diagnostics.CodeAnalysis;
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
    public class CommitsClientMockedTest
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
            using (var client = new HttpClient(handler) { BaseAddress = new Uri(gitlabServer) })
            {
                var gitlabHttpFacade = new GitLabHttpFacade(new RequestsJsonSerializer(), client);
                var commitsClient = new CommitsClient(gitlabHttpFacade, new CommitQueryBuilder(), new CommitRefsQueryBuilder(), new CommitStatusesQueryBuilder());

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
            string url = $"/projects/id/repository/commits?ref_name={refName}&per_page=100&page=1";

            var handler = A.Fake<MockHandler>(opt => opt.CallsBaseMethods());
            A.CallTo(() => handler.SendAsync(HttpMethod.Get, url))
                .ReturnsLazily(() => HttpResponseMessageProducer.Success(
                    $"[  {{ \"id\": \"id1\",}},\n  {{\"id\": \"id2\",}}]"));
            using (var client = new HttpClient(handler) { BaseAddress = new Uri(gitlabServer) })
            {
                var gitlabHttpFacade = new GitLabHttpFacade(new RequestsJsonSerializer(), client);
                var commitsClient = new CommitsClient(gitlabHttpFacade, new CommitQueryBuilder(), new CommitRefsQueryBuilder(), new CommitStatusesQueryBuilder());

                var commitsFromClient = await commitsClient.GetAsync(projectId, o => o.RefName = refName);
                commitsFromClient[0].Id.Should().BeEquivalentTo("id1");
                commitsFromClient[1].Id.Should().BeEquivalentTo("id2");
            }

        }

        [Fact]
        public async void GetDiffsForCommit()
        {
            string gitlabServer = "http://fake-gitlab.com/";
            string projectId = "id";
            string sha = "6104942438c14ec7bd21c6cd5bd995272b3faff6";
            string url = $"/projects/id/repository/commits/{sha}/diff?per_page=100&page=1";

            var handler = A.Fake<MockHandler>(opt => opt.CallsBaseMethods());
            A.CallTo(() => handler.SendAsync(HttpMethod.Get, url))
                .ReturnsLazily(() => HttpResponseMessageProducer.Success(
                    $"[  {{ \"diff\": \"diff1\", \"new_path\": \"new_path1\", \"old_path\": \"old_path1\", \"a_mode\": \"a_mode1\", \"b_mode\": \"b_mode1\", \"new_file\": \"true\", \"renamed_file\": \"false\", \"deleted_file\": \"false\" }},\n  {{\"diff\": \"diff2\", \"new_path\": \"new_path2\", \"old_path\": \"old_path2\", \"a_mode\": \"a_mode2\", \"b_mode\": \"b_mode2\", \"new_file\": \"false\", \"renamed_file\": \"true\", \"deleted_file\": \"true\"}}]"));
            using (var client = new HttpClient(handler) { BaseAddress = new Uri(gitlabServer) })
            {
                var gitlabHttpFacade = new GitLabHttpFacade(new RequestsJsonSerializer(), client);
                var commitsClient = new CommitsClient(gitlabHttpFacade, new CommitQueryBuilder(), new CommitRefsQueryBuilder(), new CommitStatusesQueryBuilder());

                var diffsFromClient = await commitsClient.GetDiffsAsync(projectId, sha);
                diffsFromClient[0].DiffText.Should().BeEquivalentTo("diff1");
                diffsFromClient[0].NewPath.Should().BeEquivalentTo("new_path1");
                diffsFromClient[0].OldPath.Should().BeEquivalentTo("old_path1");
                diffsFromClient[0].AMode.Should().BeEquivalentTo("a_mode1");
                diffsFromClient[0].BMode.Should().BeEquivalentTo("b_mode1");
                diffsFromClient[0].IsNewFile.Should().BeTrue();
                diffsFromClient[0].IsRenamedFile.Should().BeFalse();
                diffsFromClient[0].IsDeletedFile.Should().BeFalse();

                diffsFromClient[1].DiffText.Should().BeEquivalentTo("diff2");
                diffsFromClient[1].NewPath.Should().BeEquivalentTo("new_path2");
                diffsFromClient[1].OldPath.Should().BeEquivalentTo("old_path2");
                diffsFromClient[1].AMode.Should().BeEquivalentTo("a_mode2");
                diffsFromClient[1].BMode.Should().BeEquivalentTo("b_mode2");
                diffsFromClient[1].IsNewFile.Should().BeFalse();
                diffsFromClient[1].IsRenamedFile.Should().BeTrue();
                diffsFromClient[1].IsDeletedFile.Should().BeTrue();

            }
        }

        [Fact]
        public async void GetStatusesForCommit()
        {
            string gitlabServer = "http://fake-gitlab.com/";
            string projectId = "id";
            string sha = "6104942438c14ec7bd21c6cd5bd995272b3faff6";
            string Name = "name1";
            string url = $"/projects/id/repository/commits/{sha}/statuses?name={Name}&per_page=100&page=1";

            var handler = A.Fake<MockHandler>(opt => opt.CallsBaseMethods());
            A.CallTo(() => handler.SendAsync(HttpMethod.Get, url))
                .ReturnsLazily(() => HttpResponseMessageProducer.Success(
                    $"[  {{\"id\":1,\"sha\":\"{sha}\",\"ref \":\"\",\"status\":\"success\",\"name\":\"name1\",\"target_url\":\"target_url1\",\"description\":\"success\",\"created_at\":\"2020-04-08T11:57:49.136+05:30\",\"started_at\":\"2020-04-08T11:58:00.362+05:30\",\"finished_at\":\"2020-04-08T11:58:06.121+05:30\",\"allow_failure\":false,\"coverage\":null,\"author\":{{\"id\":1,\"name\":\"name\",\"username\":\"username\",\"state\":\"active\",\"avatar_url\":\"avatar_url1\",\"web_url\":\"web_url1\"}} }},{{\"id\":2,\"sha\":\"{sha}\",\"ref \":\"\",\"status\":\"success\",\"name\":\"name2\",\"target_url\":\"target_url2\",\"description\":\"success\",\"created_at\":\"2020-04-08T11:57:49.136+05:30\",\"started_at\":\"2020-04-08T11:58:00.362+05:30\",\"finished_at\":\"2020-04-08T11:58:06.121+05:30\",\"allow_failure\":false,\"coverage\":null,\"author\":{{\"id\":2,\"name\":\"name2\",\"username\":\"username2\",\"state\":\"activ2\",\"avatar_url2\":\"avatar_url2\",\"web_url\":\"web_url2\"}} }}]"));
            using (var client = new HttpClient(handler) { BaseAddress = new Uri(gitlabServer) })
            {
                var gitlabHttpFacade = new GitLabHttpFacade(new RequestsJsonSerializer(), client);
                var commitsClient = new CommitsClient(gitlabHttpFacade, new CommitQueryBuilder(), new CommitRefsQueryBuilder(), new CommitStatusesQueryBuilder());

                var statusesFromClient = await commitsClient.GetStatusesAsync(projectId, sha, o => o.Name = Name);
                statusesFromClient[0].Status.Should().BeEquivalentTo("success");
                statusesFromClient[0].Name.Should().BeEquivalentTo("name1");
                statusesFromClient[0].TargetUrl.Should().BeEquivalentTo("target_url1");
                statusesFromClient[0].Id.Should().BeEquivalentTo("1");

                statusesFromClient[1].Status.Should().BeEquivalentTo("success");
                statusesFromClient[1].Name.Should().BeEquivalentTo("name2");
                statusesFromClient[1].TargetUrl.Should().BeEquivalentTo("target_url2");
                statusesFromClient[1].Id.Should().BeEquivalentTo("2");

            }
        }
    }
}
