using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Users.Requests;
using GitLabApiClient.Models.Users.Responses;

namespace GitLabApiClient
{
    public interface IUsersClient
    {
        /// <summary>
        /// Retrieves registered users.
        /// </summary>
        Task<IList<User>> GetAsync();

        /// <summary>
        /// Retrieves an user matched by name.
        /// </summary>
        /// <param name="name">Username of the user.</param>
        /// <returns>User or NULL if it was not found.</returns>
        Task<User> GetAsync(string name);

        /// <summary>
        /// Retrieves users by filter.
        /// </summary>
        /// <param name="filter">Filter used for usernames and emails.</param>
        /// <returns>Users list satisfying the filter.</returns>
        Task<IList<User>> GetByFilterAsync(string filter);

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="request">Request to create user.</param>
        /// <returns>Newly created user.</returns>
        Task<User> CreateAsync(CreateUserRequest request);

        /// <summary>
        /// Updates existing user
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="request">Request to update user.</param>
        /// <returns>Newly modified user.</returns>
        Task<User> UpdateAsync(UserId userId, UpdateUserRequest request);

        /// <summary>
        /// Retrieves current, authenticated user session.
        /// </summary>
        /// <returns>Session of authenticated user.</returns>
        Task<Session> GetCurrentSessionAsync();

        /// <summary>
        /// Deletes user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        Task DeleteAsync(UserId userId);
        /// <summary>
        /// Activates the specified user. Available only for admin.
        /// Introduced in GitLab 12.4.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        Task ActivatAsync(UserId userId);

        /// <summary>
        /// Deactivates the specified user. Available only for admin.
        /// Introduced in GitLab 12.4.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        Task DeactivatAsync(UserId userId);

        /// <summary>
        /// Block the specified user. Available only for admin.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        Task BlockAsync(UserId userId);

        /// <summary>
        /// Unblocks the specified user. Available only for admin.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        Task UnblockAsync(UserId userId);

        /// <summary>
        /// Approves the specified user. Available only for administrators.
        /// Introduced in GitLab 13.7.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        Task ApproveAsync(UserId userId);        
    }
}
