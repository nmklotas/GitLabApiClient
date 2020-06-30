using System;
using GitLabApiClient.Models.Users.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.AwardEmojis.Responses
{
    public sealed class AwardEmoji
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("awardable_id")]
        public int AwardableId { get; set; }

        [JsonProperty("awardable_type")]
        public AwardableType AwardableType { get; set; }

    }
}
