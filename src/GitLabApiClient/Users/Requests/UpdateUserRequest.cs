using Newtonsoft.Json;

namespace GitLabApiClient.Users.Requests
{
    /// <summary>
    /// Modifies an existing user. Only administrators can change attributes of a user.
    /// </summary>
    public sealed class UpdateUserRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserRequest"/> class.
        /// <param name="userId">The ID of the user.</param>
        /// </summary>
        public UpdateUserRequest(int userId) => UserId = userId;

        /// <summary>
        /// The ID of the user.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Username.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Skype ID.
        /// </summary>
        [JsonProperty("skype")]
        public string Skype { get; set; }

        /// <summary>
        /// LinkedIn account.
        /// </summary>
        [JsonProperty("linkedin")]
        public string Linkedin { get; set; }

        /// <summary>
        /// Twitter account.
        /// </summary>
        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        /// <summary>
        /// Website URL.
        /// </summary>
        [JsonProperty("website_url")]
        public string WebSiteUrl { get; set; }
        
        /// <summary>
        /// Organization name.
        /// </summary>
        [JsonProperty("organization")]
        public string Organization { get; set; }

        /// <summary>
        /// Limit projects each user can create.
        /// </summary>
        [JsonProperty("projects_limit")]
        public int? ProjectsLimit { get; set; }

        /// <summary>
        /// External UID.
        /// </summary>
        [JsonProperty("extern_uid")]
        public string ExternUid { get; set; }

        /// <summary>
        /// External provider name.
        /// </summary>
        [JsonProperty("provider")]
        public string Provider { get; set; }

        /// <summary>
        /// User's biography.
        /// </summary>
        [JsonProperty("bio")]
        public string Bio { get; set; }

        /// <summary>
        /// User's location.
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// User is admin.
        /// </summary>
        [JsonProperty("admin")]
        public bool? Admin { get; set; }

        /// <summary>
        /// User can create groups.
        /// </summary>
        [JsonProperty("can_create_group")]
        public bool? CanCreateGroup { get; set; }

        /// <summary>
        /// Flags the user as external.
        /// </summary>
        [JsonProperty("skip_confirmation")]
        public bool? SkipConfirmation { get; set; }

        /// <summary>
        /// Flags the user as external.
        /// </summary>
        [JsonProperty("external")]
        public bool? External { get; set; }
    }
}
