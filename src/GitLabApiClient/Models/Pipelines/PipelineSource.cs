using System.Runtime.Serialization;

namespace GitLabApiClient.Models.Pipelines
{
    public enum PipelineSource
    {
        [EnumMember(Value = "push")]
        Push,
        [EnumMember(Value = "web")]
        Web,
        [EnumMember(Value = "trigger")]
        Trigger,
        [EnumMember(Value = "schedule")]
        Schedule,
        [EnumMember(Value = "api")]
        Api,
        [EnumMember(Value = "external")]
        External,
        [EnumMember(Value = "pipeline")]
        Pipeline,
        [EnumMember(Value = "chat")]
        Chat,
        [EnumMember(Value = "webide")]
        Webide,
        [EnumMember(Value = "merge_request_event")]
        MergeRequestEvent,
        [EnumMember(Value = "external_pull_request_event")]
        ExternalPullRequestEvent,
        [EnumMember(Value = "parent_pipeline")]
        ParentPipeline,
        [EnumMember(Value = "ondemand_dast_scan")]
        OndemandDastScan,
        [EnumMember(Value = "ondemand_dast_validation")]
        OndemandDastValidation
    }
}
