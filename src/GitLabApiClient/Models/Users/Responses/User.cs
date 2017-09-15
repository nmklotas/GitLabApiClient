using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Users.Responses
{
    public sealed class User
    {
        [JsonProperty("current_sign_in_at")]
        public string CurrentSignInAt { get; set; }

        [JsonProperty("linkedin")]
        public string Linkedin { get; set; }

        [JsonProperty("can_create_project")]
        public bool CanCreateProject { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("can_create_group")]
        public bool CanCreateGroup { get; set; }

        [JsonProperty("confirmed_at")]
        public string ConfirmedAt { get; set; }

        [JsonProperty("color_scheme_id")]
        public int ColorSchemeId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("identities")]
        public List<Identity> Identities { get; } = new List<Identity>();

        [JsonProperty("external")]
        public bool External { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("last_activity_on")]
        public string LastActivityOn { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("last_sign_in_at")]
        public string LastSignInAt { get; set; }

        [JsonProperty("projects_limit")]
        public int ProjectsLimit { get; set; }

        [JsonProperty("two_factor_enabled")]
        public bool TwoFactorEnabled { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("skype")]
        public string Skype { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("website_url")]
        public string WebsiteUrl { get; set; }
    }
}
