using System;
using GitLabApiClient.Models.Users.Requests;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Users.Responses
{
    public sealed class ImpersonationToken
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("revoked")]
        public bool Revoked { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("scopes")]
        public ApiScope[] Scopes { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("impersonation")]
        public bool Impersonation { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}
