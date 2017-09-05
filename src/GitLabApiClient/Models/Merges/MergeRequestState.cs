using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Merges
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