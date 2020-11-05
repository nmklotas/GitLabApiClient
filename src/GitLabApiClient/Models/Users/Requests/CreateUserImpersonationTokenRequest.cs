using System;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Users.Requests
{
    /// <summary>
    /// Creates a new impersonation token for a user. 
    /// Note only administrators can create new impersonation tokens for users. 
    /// </summary>
    public sealed class CreateUserImpersonationTokenRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserImpersonationTokenRequest"/> class.
        /// <param name="name">Name of the impersonation token.</param>
        /// <param name="scopes">Scopes for the token.</param>
        /// </summary>
        public CreateUserImpersonationTokenRequest(string name, ApiScope[] scopes)
        {
            Guard.NotEmpty(name, nameof(name));
            Guard.NotEmpty(scopes, nameof(scopes));
            Name = name;
            Scopes = scopes;
        }

        /// <summary>
        /// Name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }

        /// <summary>
        /// Scopes.
        /// </summary>
        [JsonProperty("scopes")]
        public ApiScope[] Scopes { get; }

        /// <summary>
        /// Optional date the token expires
        /// </summary>
        [JsonProperty("expires_at")]
        public DateTime? ExpiresAt { get; set; }
    }
}
