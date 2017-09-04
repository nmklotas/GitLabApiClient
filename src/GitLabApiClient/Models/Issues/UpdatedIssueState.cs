using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Issues
{
    public enum UpdatedIssueState
    {
        [EnumMember(Value = "close")]
        Close,

        [EnumMember(Value = "reopen")]
        Reopen
    }
}