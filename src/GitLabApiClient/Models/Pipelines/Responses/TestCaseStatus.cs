using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Pipelines.Responses {
    public enum TestCaseStatus
    {
        Unknown,
        [EnumMember(Value = "success")]
        Success
    }
}
