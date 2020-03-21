using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Models.Runners.Responses;
using GitLabApiClient.Models.Users.Requests;
using GitLabApiClient.Models.Users.Responses;

namespace GitLabApiClient
{
    public interface IRunnersClient
    {
        /// <summary>
        /// Retrieves registered runners.
        /// </summary>
        Task<IList<Runner>> GetAsync();

        /// <summary>
        /// Retrieves an user matched by name.
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        /// <returns>Runner or NULL if it was not found.</returns>
        Task<Runner> GetAsync(int runnerId);

        /// <summary>
        /// Updates existing user
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        /// <param name="request">Request to update user.</param>
        /// <returns>Newly modified user.</returns>
        Task<User> UpdateAsync(int runnerId, UpdateUserRequest request);

        /// <summary>
        /// Deletes user.
        /// </summary>
        /// <param name="runnerId">Id of the runner.</param>
        Task DeleteAsync(int runnerId);
    }
}
