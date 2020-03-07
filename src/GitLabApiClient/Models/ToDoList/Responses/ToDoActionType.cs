using System.Runtime.Serialization;

namespace GitLabApiClient.Models.ToDoList.Responses
{
    public enum ToDoActionType
    {
        [EnumMember(Value = "assigned")]
        Assigned,

        [EnumMember(Value = "mentioned")]
        Mentioned,

        [EnumMember(Value = "build_failed")]
        BuildFailed,

        [EnumMember(Value = "marked")]
        Marked,

        [EnumMember(Value = "approval_required")]
        ApprovalRequired,

        [EnumMember(Value = "unmergeable")]
        Unmergeable,

        [EnumMember(Value = "directly_addressed")]
        DirectlyAddressed,
    }
}
