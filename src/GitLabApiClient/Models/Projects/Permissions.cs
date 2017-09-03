using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects
{
    public sealed class Permissions
    {
        [JsonProperty("group_access")]
        public Access GroupAccess { get; set; }

        [JsonProperty("project_access")]
        public Access ProjectAccess { get; set; }
    }
}