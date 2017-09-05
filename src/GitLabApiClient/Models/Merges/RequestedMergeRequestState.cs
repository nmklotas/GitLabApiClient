using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Merges
{
    public enum RequestedMergeRequestState
    {
        [EnumMember(Value = "close")]
        Close,
        [EnumMember(Value = "reopen")]
        Reopen
    }
}
