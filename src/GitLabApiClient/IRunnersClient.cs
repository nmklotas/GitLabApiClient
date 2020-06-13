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
        /// Retrieves a runner matched by id.
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        /// <returns>Runner or NULL if it was not found.</returns>
        Task<Runner> GetAsync(int runnerId);

        /// <summary>
        /// Updates existing runner
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        /// <param name="request">Request to update runner.</param>
        /// <returns>Newly modified runner.</returns>
        Task<Runner> UpdateAsync(int runnerId, UpdateRunnerRequest request);

        /// <summary>
        /// Deletes a runner.
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        Task DeleteAsync(int runnerId);
    }
}
