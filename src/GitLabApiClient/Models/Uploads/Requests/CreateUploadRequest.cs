using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitLabApiClient.Models.Uploads.Requests
{
    /// <summary>
    /// A upload (file) for Gitlab, which can be embedded via markdown
    /// </summary>
    public sealed class CreateUploadRequest
    {
        /// <summary>
        /// Creates a new instance if the create upload request
        /// </summary>
        /// <param name="projectId">The id of the project, the file should be uploaded to</param>
        /// <param name="stream">The stream to be uploaded</param>
        /// <param name="fileName">The name of the file being uploaded</param>
        public CreateUploadRequest(string projectId, Stream stream, string fileName)
        {
            ProjectId = projectId;
            Stream = stream;
            FileName = fileName;
        }

        /// <summary>
        /// The id of the project, the file should be uploaded to
        /// </summary>
        public string ProjectId { get; }

        /// <summary>
        /// The stream to be uploaded
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// The name of the file being uploaded
        /// </summary>
        public string FileName { get; }
    }
}
