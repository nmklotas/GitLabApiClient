using System.Runtime.Serialization;

namespace GitLabApiClient.Models.MergeRequests.Responses
{
    public enum MergeStatus
    {
        [EnumMember(Value = "unchecked")]
        Unchecked,
        [EnumMember(Value = "can_be_merged")]
        CanBeMerged,
        [EnumMember(Value = "cannot_be_merged")]
        CannotBeMerged,
        [EnumMember(Value = "cannot_be_merged_recheck")]
        CannotBeMergedRecheck,
        [EnumMember(Value = "checking")]
        Checking
    }
}
