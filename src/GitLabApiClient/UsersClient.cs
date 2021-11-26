using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
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
    public sealed class UsersClient : IUsersClient
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
        public async Task<IList<User>> GetByFilterAsync(string filter)
        {
            Guard.NotEmpty(filter, nameof(filter));
            return await _httpFacade.GetPagedList<User>($"users?search={filter}");
        }

        /// <summary>
        /// Retrieves users by properties.
        /// </summary>
        /// <param name="properties">Filter used for user properties.</param>
        /// <returns>Users list satisfying the selected properties.</returns>
        public async Task<IList<User>> GetByPropertiesAsync(string properties)
        {
            Guard.NotEmpty(properties, nameof(properties));
            return await _httpFacade.GetPagedList<User>($"users?{properties}");
        }

        /// <summary>
        /// Retrieves users by properties.
        /// </summary>
        /// <param name="properties">Filter used for user properties.</param>
        /// <returns>Users list satisfying the selected properties.</returns>
        public async Task<IList<User>> GetByPropertiesAsync(UpdateUserRequest properties)
        {
            string propstring = string.Empty;
            foreach (System.Reflection.PropertyInfo pi in typeof(UpdateUserRequest).GetProperties())
            {
                Newtonsoft.Json.JsonPropertyAttribute attr = (Newtonsoft.Json.JsonPropertyAttribute)System.Attribute.GetCustomAttribute(pi, typeof(Newtonsoft.Json.JsonPropertyAttribute));
                if (attr == null)
                    continue;
                object value = pi.GetValue(properties);
                if (value == null)
                    continue;
                if (value.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string))
                        continue;
                }
                else if (value.GetType() == typeof(int?))
                {
                    if (!((int?)value).HasValue)
                        continue;
                    value = ((int?)value).Value;
                }
                else if (value.GetType() == typeof(bool?))
                {
                    if (!((bool?)value).HasValue)
                        continue;
                    value = ((bool?)value).Value;
                }
                else
                    continue;
                if (propstring.Length > 0)
                    propstring += "&";
                propstring += attr.PropertyName + "=" + value.ToString();
            }
            Guard.NotEmpty(propstring, nameof(propstring));
            return await _httpFacade.GetPagedList<User>($"users?{propstring}");
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
        /// <param name="userId">Id of the user.</param>
        /// <param name="request">Request to update user.</param>
        /// <returns>Newly modified user.</returns>
        public async Task<User> UpdateAsync(UserId userId, UpdateUserRequest request) =>
            await _httpFacade.Put<User>($"users/{userId}", request);

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
        /// <param name="hard_delete">(optional) - If true, contributions that would usually be moved to the ghost user will be deleted instead, as well as groups owned solely by this user.</param>
        public async Task DeleteAsync(UserId userId, bool hard_delete = false) =>
            await _httpFacade.Delete($"users/{userId}" + (hard_delete ? "?hard_delete=true" : ""));
    }
}
