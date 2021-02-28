using GitLabApiClient.Models.Releases.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Tags.Responses
{
    public sealed class Tag
    {
        [JsonProperty("commit")]
        public Commit Commit { get; set; }

        [JsonProperty("release")]
        public Release Release { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

    }
}
