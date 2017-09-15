using System.Runtime.Serialization;

namespace GitLabApiClient.Models.MergeRequests.Requests
{
    public enum RequestedMergeRequestState
    {
        [EnumMember(Value = "close")]
        Close,
        [EnumMember(Value = "reopen")]
        Reopen
    }
}
