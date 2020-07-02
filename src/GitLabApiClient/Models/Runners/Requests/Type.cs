using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Runners.Requests
{
    public enum RunnerType
    {
        All,
        [EnumMember(Value = "instance_type")]
        InstanceType,
        [EnumMember(Value = "group_type")]
        GroupType,
        [EnumMember(Value = "project_type")]
        ProjectType,
    }
}
