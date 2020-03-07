using System.Runtime.Serialization;

namespace GitLabApiClient.Models.ToDoList.Responses
{
    public enum ToDoState
    {
        [EnumMember(Value = "pending")]
        Pending,

        [EnumMember(Value = "done")]
        Done,
    }
}
