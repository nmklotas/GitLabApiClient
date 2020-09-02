using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Models.Runners.Requests;
using GitLabApiClient.Models.Runners.Responses;

namespace GitLabApiClient
{
    public interface IRunnersClient
    {
        /// <summary>
        /// Retrieves registered runners.
        /// </summary>
        Task<IList<Runner>> GetAsync();

        /// <summary>
        /// Retrieves all registered runners.
        /// </summary>
        Task<IList<Runner>> GetAllAsync();

        /// <summary>
        /// Retrieves a runner matched by id.
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        /// <returns>Runner or NULL if it was not found.</returns>
        Task<RunnerDetails> GetAsync(int runnerId);

        /// <summary>
        /// Updates existing runner
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        /// <param name="request">Request to update runner.</param>
        /// <returns>Newly modified runner.</returns>
        Task<Runner> UpdateAsync(int runnerId, UpdateRunnerRequest request);

        /// <summary>
        /// Creates a new runner registration.
        /// </summary>
        /// <returns>The newly created runner.</returns>
        /// <param name="request">Create runner request.</param>
        Task<RunnerToken> CreateAsync(CreateRunnerRequest request);

        /// <summary>
        /// Deletes a runner.
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        Task DeleteAsync(int runnerId);

        /// <summary>
        /// Deletes a runner.
        /// </summary>
        /// <param name="runnerToken">Token of the runner.</param>
        Task DeleteAsync(string runnerToken);

        /// <summary>
        /// Checks if a runner token can authenticate with GitLab
        /// </summary>
        /// <returns>True is token is valid, false if not valid.</returns>
        /// <param name="token">Token to check.</param>
        Task<bool> VerifyAuthenticationAsync(string token);
    }
}
