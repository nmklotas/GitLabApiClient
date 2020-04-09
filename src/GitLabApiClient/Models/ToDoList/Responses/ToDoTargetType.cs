using System.Runtime.Serialization;

namespace GitLabApiClient.Models.ToDoList.Responses
{
    public enum ToDoTargetType
    {
        [EnumMember(Value = "Issue")]
        Issue,

        [EnumMember(Value = "MergeRequest")]
        MergeRequest,
    }
}
