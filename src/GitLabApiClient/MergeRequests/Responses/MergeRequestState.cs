using System.Runtime.Serialization;

namespace GitLabApiClient.MergeRequests.Responses
{
    public enum MergeRequestState
    {
        [EnumMember(Value = "opened")]
        Opened,
        [EnumMember(Value = "active")]
        Active,
        [EnumMember(Value = "merged")]
        Merged,
        [EnumMember(Value = "closed")]
        Closed,
        [EnumMember(Value = "reopened")]
        Reopened
    }
}