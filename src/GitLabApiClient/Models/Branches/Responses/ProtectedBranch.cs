using Newtonsoft.Json;

namespace GitLabApiClient.Models.Branches.Responses
{
    public sealed class ProtectedBranch
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("push_access_levels")]
        public PushAccessLevel[] PushAccessLevels { get; set; }

        [JsonProperty("merge_access_levels")]
        public PushAccessLevel[] MergeAccessLevels { get; set; }
    }
}
