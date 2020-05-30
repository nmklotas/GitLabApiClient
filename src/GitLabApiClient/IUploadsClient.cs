using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Uploads.Requests;
using GitLabApiClient.Models.Uploads.Responses;

namespace GitLabApiClient
{
    public interface IUploadsClient
    {
        /// <summary>
        /// Uploads a file for the provided project.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="uploadRequest">The upload request containing the filename and stream to be uploaded</param>
        /// <returns>A <see cref="Upload"/> object.
        /// Use the <see cref="Upload.Markdown"/> property to place the image in your markdown text.
        /// </returns>
        Task<Upload> UploadFile(ProjectId projectId, CreateUploadRequest uploadRequest);
    }
}
