using Newtonsoft.Json;

namespace GitLabApiClient.Models.Issues.Responses
{
    public sealed class ClosedBy : ModifiableObject
    {
        [JsonProperty("active")]
        public string State;

        [JsonProperty("web_url")]
        public string WebUrl;

        [JsonProperty("avatar_url")]
        public string AvatarUrl;

        [JsonProperty("username")]
        public string Username;

        [JsonProperty("name")]
        public string Name;
    }
}
