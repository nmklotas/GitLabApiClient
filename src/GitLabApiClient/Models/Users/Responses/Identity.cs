using Newtonsoft.Json;

namespace GitLabApiClient.Models.Users.Responses
{
    public sealed class Identity
    {
        [JsonProperty("extern_uid")]
        public string ExternUid { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }
    }
}
