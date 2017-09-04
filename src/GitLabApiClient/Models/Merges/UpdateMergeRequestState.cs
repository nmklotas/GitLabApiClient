using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Merges
{
    public enum UpdateMergeRequestState
    {
        [EnumMember(Value = "close")]
        Close,
        [EnumMember(Value = "reopen")]
        Reopen
    }
}
