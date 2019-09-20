using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Models.Uploads.Requests;
using GitLabApiClient.Models.Uploads.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// Uploads a file to a project. This file can later be used in issues or everywhere else, where you can write markdown.
    /// After uploading the file, use the <see cref="Upload.Markdown"/> property.
    /// </summary>
    public sealed class UploadsClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal UploadsClient(GitLabHttpFacade httpFacade) =>
            _httpFacade = httpFacade;

        /// <summary>
        /// Uploads a file for the provided project. 
        /// </summary>
        /// <param name="uploadRequest">The upload request containing the filename and stream to be uploaded</param>
        /// <returns>A <see cref="Upload"/> object.
        /// Use the <see cref="Upload.Markdown"/> property to place the image in your markdown text.
        /// </returns>
        public async Task<Upload> UploadFile(CreateUploadRequest uploadRequest)
        {
            return await _httpFacade.PostFile($"projects/{uploadRequest.ProjectId}/uploads", uploadRequest);
        }
    }
}
