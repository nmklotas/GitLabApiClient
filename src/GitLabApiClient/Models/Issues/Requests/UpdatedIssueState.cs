using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Issues.Requests
{
    public enum UpdatedIssueState
    {
        [EnumMember(Value = "close")]
        Close,

        [EnumMember(Value = "reopen")]
        Reopen
    }
}
