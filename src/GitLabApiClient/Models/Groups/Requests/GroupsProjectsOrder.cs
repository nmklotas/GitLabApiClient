using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Groups.Requests
{
    public enum GroupsProjectsOrder
    {
        [EnumMember(Value = "created_at")]
        CreatedAt,
        [EnumMember(Value = "id")]
        Id,
        [EnumMember(Value = "name")]
        Name,
        [EnumMember(Value = "path")]
        Path,
        [EnumMember(Value = "updated_at")]
        UpdatedAt,
        [EnumMember(Value = "last_activity_at")]
        LastiActivityAt
    }
}
