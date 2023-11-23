using Newtonsoft.Json;

namespace GitLabApiClient.Models.Branches.Responses
{
    public sealed class ProtectedBranch
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("allow_force_push")]
        public bool AllowForcePush { get; set; }

        [JsonProperty("push_access_levels")]
        public PushAccessLevel[] PushAccessLevels { get; set; }

        [JsonProperty("merge_access_levels")]
        public PushAccessLevel[] MergeAccessLevels { get; set; }
    }
}
