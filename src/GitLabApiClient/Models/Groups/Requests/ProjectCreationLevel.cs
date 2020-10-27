using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Groups.Requests
{
    public enum ProjectCreationLevel
    {
        [EnumMember(Value = "noone")]
        NoOne,

        [EnumMember(Value = "maintainer")]
        Maintainer,

        [EnumMember(Value = "developer")]
        Developer
    }
}
