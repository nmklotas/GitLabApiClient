using System.Runtime.Serialization;

namespace GitLabApiClient.MergeRequests.Requests
{
    public enum RequestedMergeRequestState
    {
        [EnumMember(Value = "close")]
        Close,
        [EnumMember(Value = "reopen")]
        Reopen
    }
}
