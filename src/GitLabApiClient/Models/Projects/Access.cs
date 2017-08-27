using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects
{
    public class Access
    {
        [JsonProperty("access_level")]
        public int AccessLevel { get; set; }

        [JsonProperty("notification_level")]
        public int NotificationLevel { get; set; }
    }
}