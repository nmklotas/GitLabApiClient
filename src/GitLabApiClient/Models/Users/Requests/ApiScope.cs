using System.Runtime.Serialization;
namespace GitLabApiClient.Models.Users.Requests
{
    public enum ApiScope
    {
        [EnumMember(Value = "api")]
        Api,
        [EnumMember(Value = "read_user")]
        ReadUser
    }
}
