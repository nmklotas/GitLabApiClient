using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Requests
{
    /// <summary>
    /// Import a previously exported project
    /// </summary>
    public sealed class ImportProjectRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportProjectRequest"/> class.
        /// <param name="path">Path slug for imported project.</param>
        /// <param name="exportFile">Path to the file containing the exported project.</param>
        /// </summary>
        public static ImportProjectRequest FromFile(string path, string exportFile)
        {
            Guard.NotEmpty(path, nameof(path));
            Guard.NotEmpty(exportFile, nameof(exportFile));
            return new ImportProjectRequest
            {
                Path = path,
                File = exportFile
            };
        }

        private ImportProjectRequest() { }

        /// <summary>
        /// The name of the new project. if not set it will be the same as the path
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The name of the new project. if not set it will be the same as the path
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; private set; }

        /// <summary>
        /// Namespace the project should be imported in if not provided the users personal namespace will be used
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// File that contains the project data to upload.
        /// </summary>
        [JsonProperty("file")]
        public string File { get; private set; }

        /// <summary>
        /// Overwrite an existing project when exists
        /// </summary>
        [JsonProperty("overwrite")]
        public bool? Overwrite { get; set; }

    }
}
