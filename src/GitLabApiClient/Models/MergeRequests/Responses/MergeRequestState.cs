using System.Runtime.Serialization;

namespace GitLabApiClient.Models.MergeRequests.Responses
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
        Reopened,
        [EnumMember(Value = "cannot_be_merged_recheck")]
        CannotBeMerged,
        [EnumMember(Value = "locked")]
        Locked
    }
}
