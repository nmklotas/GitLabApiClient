using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GitLabApiClient.Test.Utilities
{
    public class GitLabContainerFixture : IAsyncLifetime
    {
        private const string GitLabContainerPath = "../../../../docker";
 
        private const string GitLabApiPath = "http://localhost:9190/";
 
        private static readonly TimeSpan TestTimeout = TimeSpan.FromMinutes(10);

        private HttpClient _gitLabPingClient;

        public async Task InitializeAsync()
        {
            _gitLabPingClient = new HttpClient
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
                    var response = await _gitLabPingClient.GetAsync(GitLabApiPath);
                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("GitLab started to respond!");
                        return true;
                    }
                }
                catch
                {
                }

                await Task.Delay(5000);
            }
 
            return false;
        }
    }
}
