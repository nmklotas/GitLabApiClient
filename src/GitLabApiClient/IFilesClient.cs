using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Files.Request;
using GitLabApiClient.Models.Files.Responses;

namespace GitLabApiClient
{
    public interface IFilesClient
    {
        Task<File> GetAsync(ProjectId projectId, string filePath, string reference = "master");

        Task<UpdateFileResponse> UpdateAsync(ProjectId projectId, string filePath, UpdateFileRequest request);

    }
}
