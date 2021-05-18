using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Model.Common;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Extensions;
using FluentAssertions;
using Xunit;

namespace GitLabApiClient.Test.Utilities
{
    public class GitLabContainerFixture : IAsyncLifetime
    {
        private const string InitRb = "/tmp/init.rb";
        private const string GitlabRb = "/tmp/gitlab.rb";
        private static readonly TemplateString SolutionRootFolder = "${PWD}/../../../../..";

        public static string Token { get; private set; }
        public static string RunnerRegistrationToken { get; private set; }
        public static string GitlabHost { get; private set; }

        private IContainerService _gitlabContainer;
        private readonly string _gitlabDockerImage = "gitlab/gitlab-ce:12.10.14-ce.0";

        public async Task InitializeAsync()
        {
            await StartContainer();
            Token = InitializeData();
            string hostAndPort = GetContainerHostPort(_gitlabContainer, "80/tcp");
            GitlabHost = $"http://{hostAndPort}/api/v4/";
        }

        public async Task DisposeAsync()
        {
            await StopContainer();
        }

        private Task StartContainer()
        {
            _gitlabContainer = new Builder()
                .UseContainer()
                .UseImage(_gitlabDockerImage)
                .WithEnvironment(
                    $"GITLAB_OMNIBUS_CONFIG=from_file('{GitlabRb}')"
                )
                .ExposePort(80)
                .CopyOnStart($"{SolutionRootFolder}/docker/gitlab.rb", GitlabRb)
                .CopyOnStart($"{SolutionRootFolder}/docker/init.rb", InitRb)
                .WaitForHealthy()
                .Build()
                .Start();

            return Task.CompletedTask;
        }

        private Task StopContainer()
        {
            _gitlabContainer?.Stop();
            _gitlabContainer?.Dispose();

            return Task.CompletedTask;
        }

        private string InitializeData()
        {
            string command = $"/opt/gitlab/bin/gitlab-rails r {InitRb}";
            var output = ExecuteCommandAgainstDockerWithOutput(_gitlabContainer, command);

            output.Count().Should().BeGreaterOrEqualTo(2);

            string token = output.FirstOrDefault();
            token.Should().NotBeNullOrEmpty();

            RunnerRegistrationToken = output.ElementAt(1);
            RunnerRegistrationToken.Should().NotBeNullOrEmpty();

            return token;
        }

        private static IEnumerable<string> ExecuteCommandAgainstDockerWithOutput(IContainerService container, string command)
        {
            var output = container.Execute(command);
            string error = output.Error;
            error.Should().BeEmpty();
            output.Success.Should().BeTrue();
            output.Data.Should().NotBeNullOrEmpty();

            return output.Data;
        }

        private static string GetContainerHostPort(IContainerService containerService, string portAndProto)
        {
            var ep = containerService.ToHostExposedEndpoint(portAndProto);
            string hostAndPort = $"localhost:{ep.Port}";
            return hostAndPort;
        }
    }
}
