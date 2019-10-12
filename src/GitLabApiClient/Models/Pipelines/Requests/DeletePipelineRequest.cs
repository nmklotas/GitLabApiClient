namespace GitLabApiClient.Models.Pipelines.Requests
{
    public class DeletePipelineRequest : BasePipelineRequest
    {
        public DeletePipelineRequest(string projectId, int pipelineId) : base(projectId, pipelineId)
        {
        }
    }
}
