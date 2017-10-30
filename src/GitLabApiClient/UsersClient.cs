using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Users.Requests;
using GitLabApiClient.Models.Users.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to retrieve, modify, create users.
    /// <exception cref="GitLabException">Thrown if request to GitLab API fails</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class UsersClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal UsersClient(GitLabHttpFacade httpFacade) => 
            _httpFacade = httpFacade;

        /// <summary>
        /// Retrieves registered users.
        /// </summary>
        /// <param name="cancellationToken">Request CancellationToken</param>
        public async Task<IList<User>> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _httpFacade.GetPagedList<User>("users", cancellationToken);
        }
            

        /// <summary>
        /// Retrieves an user matched by name.
        /// </summary>
        /// <param name="name">Username of the user.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>User or NULL if it was not found.</returns>
        public async Task<User> GetAsync(string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Guard.NotEmpty(name, nameof(name));
            return (await _httpFacade.Get<IList<User>>($"users?username={name}", cancellationToken)).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves users by filter.
        /// </summary>
        /// <param name="filter">Filter used for usernames and emails.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Users list satisfying the filter.</returns>
        public async Task<IList<User>> GetByFilterAsync(string filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Guard.NotEmpty(filter, nameof(filter));
            return await _httpFacade.GetPagedList<User>($"users?search={filter}", cancellationToken);
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="request">Request to create user.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Newly created user.</returns>
        public async Task<User> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _httpFacade.Post<User>("users", request, cancellationToken);
        }


        /// <summary>
        /// Updates existing user
        /// </summary>
        /// <param name="request">Request to update user.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Newly modified user.</returns>
        public async Task<User> UpdateAsync(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _httpFacade.Put<User>($"users/{request.UserId}", request, cancellationToken);
        }

        /// <summary>
        /// Retrieves current, authenticated user session.
        /// </summary>
        /// <returns>Session of authenticated user.</returns>
        public async Task<Session> GetCurrentSessionAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _httpFacade.Get<Session>("user", cancellationToken);
        }

        /// <summary>
        /// Deletes user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        public async Task DeleteAsync(int userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _httpFacade.Delete($"users/{userId}", cancellationToken);
        } 
            
    }
}
