using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models
{
    public sealed class Member : Account
    {
        [JsonProperty("access_level")]
        public int AccessLevel { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}
