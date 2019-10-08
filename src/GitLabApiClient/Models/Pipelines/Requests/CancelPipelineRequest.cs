namespace GitLabApiClient.Models.Pipelines.Requests
{
    public sealed class CancelPipelineRequest : BasePipelineRequest
    {
        public CancelPipelineRequest(string projectId, int pipelineId) : base(projectId, pipelineId)
        {
        }
    }
}
