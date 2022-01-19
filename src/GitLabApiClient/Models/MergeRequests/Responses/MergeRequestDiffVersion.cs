using System;
using GitLabApiClient.Models.Users.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequests.Responses;

public class MergeRequestDiffVersion
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("head_commit_sha")]
    public string HeadCommitSha { get; set; }

    [JsonProperty("base_commit_sha")]
    public string BaseCommitSha { get; set; }

    [JsonProperty("start_commit_sha")]
    public string StartCommitSha { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("merge_request_id")]
    public int MergeRequestId { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("real_size")]
    public int RealSize { get; set; }
}
