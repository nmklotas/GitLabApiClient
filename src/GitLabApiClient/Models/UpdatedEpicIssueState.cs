using System.Runtime.Serialization;

namespace GitLabApiClient.Models
{
    public enum UpdatedEpicIssueState
    {
        [EnumMember(Value = "close")]
        Close,

        [EnumMember(Value = "reopen")]
        Reopen
    }
}
