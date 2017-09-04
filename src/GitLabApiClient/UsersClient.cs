using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Users;
using GitLabApiClient.Utilities;

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
        public async Task<IList<User>> GetAsync() => 
            await _httpFacade.GetPagedList<User>("users");

        /// <summary>
        /// Retrieves an user matched by name.
        /// </summary>
        /// <param name="name">Username of the user.</param>
        /// <returns>User or NULL if it was not found.</returns>
        public async Task<User> GetAsync(string name)
        {
            Guard.NotEmpty(name, nameof(name));
            return (await _httpFacade.Get<IList<User>>($"users?username={name}")).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves users by filter.
        /// </summary>
        /// <param name="filter">Filter used for usernames and emails.</param>
        /// <returns>Users list satisfying the filter.</returns>
        public async Task<IList<User>> GetByFilter(string filter)
        {
            Guard.NotEmpty(filter, nameof(filter));
            return await _httpFacade.GetPagedList<User>($"users?search={filter}");
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="request">Request to create user.</param>
        /// <returns>Newly created user.</returns>
        public async Task<User> CreateAsync(CreateUserRequest request) => 
            await _httpFacade.Post<User>("users", request);

        /// <summary>
        /// Updates existing user
        /// </summary>
        /// <param name="request">Request to update user.</param>
        /// <returns>Newly modified user.</returns>
        public async Task<User> UpdateAsync(UpdateUserRequest request) => 
            await _httpFacade.Put<User>($"users/{request.UserId}", request);

        /// <summary>
        /// Retrieves current, authenticated user session.
        /// </summary>
        /// <returns>Session of authenticated user.</returns>
        public async Task<Session> GetCurrentSessionAsync() =>
            await _httpFacade.Get<Session>("user");

        /// <summary>
        /// Deletes user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        public async Task DeleteAsync(int userId) => 
            await _httpFacade.Delete($"users/{userId}");
    }
}
