using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Models.Runners.Requests;
using GitLabApiClient.Models.Runners.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to retrieve, modify, delete runners.
    /// <exception cref="GitLabException">Thrown if request to GitLab API fails</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class RunnersClient : IRunnersClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal RunnersClient(GitLabHttpFacade httpFacade) =>
            _httpFacade = httpFacade;

        /// <summary>
        /// Retrieves registered project runners.
        /// </summary>
        public async Task<IList<Runner>> GetAsync() =>
            await _httpFacade.GetPagedList<Runner>("runners");

        /// <summary>
        /// Retrieves all registered runners.
        /// </summary>
        public async Task<IList<Runner>> GetAllAsync() =>
            await _httpFacade.GetPagedList<Runner>("runners/all");

        /// <summary>
        /// Retrieves a runner matched by it's id.
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        /// <returns>Runner or NULL if it was not found.</returns>
        public async Task<RunnerDetails> GetAsync(int runnerId)
        {
            return (await _httpFacade.Get<RunnerDetails>($"runners/{runnerId}"));
        }

        /// <summary>
        /// Updates existing runner
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        /// <param name="request">Request to update runner.</param>
        /// <returns>Newly modified user.</returns>
        public async Task<Runner> UpdateAsync(int runnerId, UpdateRunnerRequest request) =>
            await _httpFacade.Put<Runner>($"runners/{runnerId}", request);

        /// <summary>
        /// Creates a new runner registration.
        /// </summary>
        /// <returns>The newly created runner.</returns>
        /// <param name="request">Create runner request.</param>
        public async Task<RunnerToken> CreateAsync(CreateRunnerRequest request) =>
            await _httpFacade.Post<RunnerToken>($"runners", request);

        /// <summary>
        /// Deletes a runner.
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        public async Task DeleteAsync(int runnerId) =>
            await _httpFacade.Delete($"runners/{runnerId}");

        /// <summary>
        /// Deletes a runner.
        /// </summary>
        /// <param name="runnerToken">Token of the runner.</param>
        public async Task DeleteAsync(string runnerToken) =>
            await _httpFacade.Delete($"runners", new Dictionary<string, string>() { { "token", runnerToken } });

        /// <summary>
        /// Checks if a runner token can authenticate with GitLab
        /// </summary>
        /// <returns>True is token is valid, false if not valid.</returns>
        /// <param name="token">Token to check.</param>
        public async Task<bool> VerifyAuthenticationAsync(string token)
        {
            try
            {
                await _httpFacade.Post("runners/verify", new Dictionary<string, string>() { { "token", token } });
                return true;
            }
            catch (GitLabException e)
            {
                if (e.HttpStatusCode == System.Net.HttpStatusCode.Forbidden)
                    return false;
                else
                    throw;
            }
        }
    }
}
