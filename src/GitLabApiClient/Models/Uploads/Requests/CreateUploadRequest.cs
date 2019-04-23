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
        public CreateUploadRequest(string projectId, Stream stream, string fileName)
        {
            projectId = projectId;
            Stream = stream;
            FileName = fileName;
        }

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
