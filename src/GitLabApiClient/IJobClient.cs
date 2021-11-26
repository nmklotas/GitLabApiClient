using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Job.Responses;

namespace GitLabApiClient
{
    public interface IJobClient
    {
        Task<Job> GetAsync(ProjectId projectId, int jobId);
        Task<Job> RetryAsync(ProjectId projectId, int jobId);
        Task<Job> PlayAsync(ProjectId projectId, int jobId);
        Task<Job> CancelAsync(ProjectId projectId, int jobId);
        Task<Job> EraseAsync(ProjectId projectId, int jobId);
    }
}
