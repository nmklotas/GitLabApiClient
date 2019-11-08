using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Groups.Requests
{
    public enum GroupsOrder
    {
        [EnumMember(Value = "name")]
        Name,
        [EnumMember(Value = "path")]
        Path
    }
}
