using System.Runtime.Serialization;

namespace GitLabApiClient.Issues.Requests
{
    public enum UpdatedIssueState
    {
        [EnumMember(Value = "close")]
        Close,

        [EnumMember(Value = "reopen")]
        Reopen
    }
}