using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Pipelines
{
    public enum PipelineStatus
    {
        All,
        [EnumMember(Value = "running")]
        Running,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "success")]
        Success,
        [EnumMember(Value = "failed")]
        Failed,
        [EnumMember(Value = "canceled")]
        Canceled,
        [EnumMember(Value = "skipped")]
        Skipped
    }
}
