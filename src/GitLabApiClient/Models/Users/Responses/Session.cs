using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Users.Responses
{
    public sealed class Session
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("can_create_project")]
        public bool CanCreateProject { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("can_create_group")]
        public bool CanCreateGroup { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("color_scheme_id")]
        public int ColorSchemeId { get; set; }

        [JsonProperty("current_sign_in_at")]
        public string CurrentSignInAt { get; set; }

        [JsonProperty("linkedin")]
        public string Linkedin { get; set; }

        [JsonProperty("skype")]
        public string Skype { get; set; }

        [JsonProperty("identities")]
        public List<Identity> Identities { get; } = new List<Identity>();

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("private_token")]
        public string PrivateToken { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("projects_limit")]
        public int ProjectsLimit { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("two_factor_enabled")]
        public bool TwoFactorEnabled { get; set; }

        [JsonProperty("website_url")]
        public string WebsiteUrl { get; set; }
    }
}
