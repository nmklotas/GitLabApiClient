using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Users.Requests
{
    /// <summary>
    /// Creates a new user. 
    /// Note only administrators can create new users. 
    /// Either <see cref="Password"/> or <see cref="ResetPassword"/> should be specified (<see cref="ResetPassword"/> takes priority).
    /// </summary>
    public sealed class CreateUserRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserRequest"/> class.
        /// <param name="name">Name.</param>
        /// <param name="username">Username.</param>
        /// <param name="email">User email.</param>
        /// </summary>
        public CreateUserRequest(string name, string username, string email)
        {
            Guard.NotEmpty(name, nameof(name));
            Guard.NotEmpty(username, nameof(username));
            Guard.NotEmpty(email, nameof(email));
            Name = name;
            Username = username;
            Email = email;
        }

        /// <summary>
        /// User email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; }

        /// <summary>
        /// Username.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; }

        /// <summary>
        /// Name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }

        /// <summary>
        /// Password.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Send user password reset link.
        /// </summary>
        [JsonProperty("reset_password")]
        public bool? ResetPassword { get; set; } = true;

        /// <summary>
        /// Generate random password on the server.
        /// </summary>
        [JsonProperty("force_random_password")]
        public bool? ForceRandomPassword { get; set; } = true;

        /// <summary>
        /// Skype ID
        /// </summary>
        [JsonProperty("skype")]
        public string Skype { get; set; }

        /// <summary>
        /// LinkedIn account
        /// </summary>
        [JsonProperty("linkedin")]
        public string Linkedin { get; set; }

        /// <summary>
        /// Twitter account
        /// </summary>
        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        /// <summary>
        /// Website URL
        /// </summary>
        [JsonProperty("website_url")]
        public string WebSiteUrl { get; set; }

        /// <summary>
        /// Organization name
        /// </summary>
        [JsonProperty("organization")]
        public string Organization { get; set; }

        /// <summary>
        /// umber of projects user can create
        /// </summary>
        [JsonProperty("projects_limit")]
        public int? ProjectsLimit { get; set; }

        /// <summary>
        /// External UID
        /// </summary>
        [JsonProperty("extern_uid")]
        public string ExternUid { get; set; }

        /// <summary>
        /// External provider name
        /// </summary>
        [JsonProperty("provider")]
        public string Provider { get; set; }

        /// <summary>
        /// User's biography
        /// </summary>
        [JsonProperty("bio")]
        public string Bio { get; set; }

        /// <summary>
        /// User's location
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// User is admin
        /// </summary>
        [JsonProperty("admin")]
        public bool? Admin { get; set; }

        /// <summary>
        /// User can create groups
        /// </summary>
        [JsonProperty("can_create_group")]
        public bool? CanCreateGroup { get; set; }

        /// <summary>
        /// Skip confirmation
        /// </summary>
        [JsonProperty("skip_confirmation")]
        public bool? SkipConfirmation { get; set; }

        /// <summary>
        /// Flags the user as external
        /// </summary>
        [JsonProperty("external")]
        public bool? External { get; set; }
    }
}
