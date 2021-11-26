using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Groups.Requests
{
    public enum SubgroupCreationLevel
    {
        [EnumMember(Value = "owner")]
        Owner,

        [EnumMember(Value = "maintainer")]
        Maintainer
    }
}
