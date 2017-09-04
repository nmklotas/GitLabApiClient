using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Merges
{
    public enum MergeStatus
    {
        [EnumMember(Value = "unchecked")]
        Unchecked,
        [EnumMember(Value = "can_be_merged")]
        CanBeMerged,
        [EnumMember(Value = "cannot_be_merged")]
        CannotBeMerged
    }
}