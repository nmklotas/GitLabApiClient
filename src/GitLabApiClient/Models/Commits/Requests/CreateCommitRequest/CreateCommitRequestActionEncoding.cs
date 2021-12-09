using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Commits.Requests.CreateCommitRequest
{
    public enum CreateCommitRequestActionEncoding
    {
        [EnumMember(Value = "text")]
        Text,
        [EnumMember(Value = "base64")]
        Base64
    }
}
