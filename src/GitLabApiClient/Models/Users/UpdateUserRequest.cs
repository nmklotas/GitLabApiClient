using Newtonsoft.Json;

namespace GitLabApiClient.Models.Users
{
    public class UpdateUserRequest
    {
        public UpdateUserRequest(int userId) => UserId = userId;

        public int UserId { get; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("reset_password")]
        public bool ResetPassword { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("skype")]
        public string Skype { get; set; }

        [JsonProperty("linkedin")]
        public string Linkedin { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("website_url")]
        public string WebSiteUrl { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("projects_limit")]
        public int ProjectsLimit { get; set; }

        [JsonProperty("extern_uid")]
        public string ExternUid { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("admin")]
        public bool Admin { get; set; }

        [JsonProperty("can_create_group")]
        public bool CanCreateGroup { get; set; }

        [JsonProperty("skip_confirmation")]
        public bool SkipConfirmation { get; set; }

        [JsonProperty("external")]
        public bool External { get; set; }
    }
}
