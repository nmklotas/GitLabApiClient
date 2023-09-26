using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Users.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.LabelEvents.Responses
{
    public class LabelEvent : Resource
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("label")]
        public Label Label { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }
    }
}
