using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Projects
{
    public enum ProjectVisibilityLevel
    {
        [EnumMember(Value = "private")]
        Private,
        [EnumMember(Value = "internal")]
        Internal,
        [EnumMember(Value = "public")]
        Public
    }
}
