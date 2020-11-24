using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Groups.Requests
{
    public enum GroupsProjectCreationLevel
    {
        [EnumMember(Value = "noone")]
        NoOne,

        [EnumMember(Value = "maintainer")]
        Maintainers,

        [EnumMember(Value = "developer")]
        Developers
    }
}