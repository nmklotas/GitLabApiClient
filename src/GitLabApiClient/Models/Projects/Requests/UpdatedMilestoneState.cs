using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Projects.Requests
{
    public enum UpdatedMilestoneState
    {
        [EnumMember(Value = "close")]
        Close,

        [EnumMember(Value = "activate")]
        Activate
    }
}
