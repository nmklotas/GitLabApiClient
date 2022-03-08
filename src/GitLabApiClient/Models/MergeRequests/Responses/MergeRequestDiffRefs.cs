using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequests.Responses;

public class MergeRequestDiffRefs
{
    [JsonProperty("head_sha")]
    public string HeadCommitSha { get; set; }

    [JsonProperty("base_sha")]
    public string BaseCommitSha { get; set; }

    [JsonProperty("start_sha")]
    public string StartCommitSha { get; set; }
}
