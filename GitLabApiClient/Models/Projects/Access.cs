using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects
{
    public class Access
    {
        [JsonProperty("access_level")]
        public long AccessLevel { get; set; }

        [JsonProperty("notification_level")]
        public long NotificationLevel { get; set; }
    }
}