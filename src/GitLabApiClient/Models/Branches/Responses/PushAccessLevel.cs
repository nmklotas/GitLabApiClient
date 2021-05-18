using Newtonsoft.Json;

namespace GitLabApiClient.Models.Branches.Responses
{
    public sealed class PushAccessLevel
    {
        [JsonProperty("access_level")]
        public ProtectedRefAccessLevels AccessLevel { get; set; }

        [JsonProperty("access_level_description")]
        public string AccessLevelDescription { get; set; }
    }
}
