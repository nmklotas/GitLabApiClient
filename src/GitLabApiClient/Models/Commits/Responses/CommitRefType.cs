using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Commits.Responses
{
    public enum CommitRefType
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "branch")]
        Branch,
        [EnumMember(Value = "tag")]
        Tag
    }
}
