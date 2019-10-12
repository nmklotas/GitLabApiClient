namespace GitLabApiClient.Models.Pipelines.Requests
{
    public sealed class RetryPipelineRequest : BasePipelineRequest
    {
        public RetryPipelineRequest(string projectId, int pipelineId) : base(projectId, pipelineId)
        {
        }
    }
}
