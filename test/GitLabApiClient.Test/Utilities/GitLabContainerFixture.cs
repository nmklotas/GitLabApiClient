using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GitLabApiClient.Test.Utilities
{
    public class FixtureTest
    {
        [Fact]
        public async Task Test()
        {
            var fixture = new GitLabContainerFixture();
            await fixture.InitializeAsync();
        }
    }

    public class GitLabContainerFixture : IAsyncLifetime
    {
        private const string GitLabContainerPath = "../../../docker";
 
        private const string GitLabApiPath = "http://localhost:9190/";
 
        private static readonly TimeSpan TestTimeout = TimeSpan.FromSeconds(60);

        private HttpClient _client;

        public async Task InitializeAsync()
        {
            _client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(1)
            };

            StartContainer();
            if (!await WaitForService())
                throw new Exception($"Failed to start container, timeout hit.");
        }

        public Task DisposeAsync()
        {
            StopContainer();
            return Task.CompletedTask;
        }

        private void StartContainer()
        {
            // Now start the docker containers
            StartProcessAndWaitForExit(new ProcessStartInfo
            {
                FileName = "docker-compose",
                Arguments =
                    $"-f {GitLabContainerPath}/docker-compose.yml up -d"
            });
        }
 
        private void StopContainer()
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "docker-compose",
                Arguments =
                    $"-f {GitLabContainerPath}/docker-compose.yml down"
            };

            StartProcessAndWaitForExit(processStartInfo);
        }

        private void StartProcessAndWaitForExit(ProcessStartInfo processStartInfo)
        {
            processStartInfo.Environment["COMPUTERNAME"] = Environment.MachineName;

            var process = Process.Start(processStartInfo);
            process.WaitForExit();
            Assert.Equal(0, process.ExitCode);
        }
 
        private async Task<bool> WaitForService()
        {
            var startTime = DateTime.Now;

            while (DateTime.Now - startTime < TestTimeout)
            {
                try
                {
                    var response = await _client.GetAsync(GitLabApiPath);
                    return response.IsSuccessStatusCode;
                }
                catch
                {
                    // Ignore exceptions, just retry
                }

                await Task.Delay(1000);
            }
 
            return false;
        }
    }
}