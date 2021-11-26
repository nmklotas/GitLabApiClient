using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Commits.Requests.CreateCommitRequest
{
    public enum CreateCommitRequestActionType
    {
        [EnumMember(Value = "create")]
        Create,
        [EnumMember(Value = "delete")]
        Delete,
        [EnumMember(Value = "move")]
        Move,
        [EnumMember(Value = "update")]
        Update,
        [EnumMember(Value = "chmod")]
        Chmod
    }
}
