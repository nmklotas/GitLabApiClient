using System;
using System.Diagnostics;
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
                throw new Exception("Failed to start container, timeout hit.");
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
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.Environment["COMPUTERNAME"] = Environment.MachineName;

            var process = new Process
            {
                StartInfo = processStartInfo
            };

            process.OutputDataReceived += LogOutputData;
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            Assert.Equal(0, process.ExitCode);

            void LogOutputData(object sender, DataReceivedEventArgs e)
            //TODO: find out how to log data in XUnit fixtures
                => Trace.WriteLine(e.Data);
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
                        Trace.WriteLine("GitLab started to respond!");
                        return true;
                    }
                }
                catch (HttpRequestException)
                {
                }
                catch (OperationCanceledException)
                {
                }

                await Task.Delay(15000);
            }
 
            return false;
        }
    }
}
