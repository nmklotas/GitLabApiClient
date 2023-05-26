using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Commits.Requests
{
    /// <summary>
    /// Used to create a commit in a project.
    /// </summary>
    public sealed class CreateCommitActionRequest
    {
        /// <summary>
        /// The action to perform, create, delete, move, update, chmod.
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        /// <summary>
        /// Full path to the file. Ex. lib/class.rb.
        /// </summary>
        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        /// <summary>
        /// Original full path to the file being moved. Ex. lib/class1.rb. Only considered for move action.
        /// </summary>
        [JsonProperty("previous_path")]
        public string PreviousPath { get; set; }

        /// <summary>
        /// File content, required for all except delete, chmod, and move. Move actions that do not specify content preserve the existing file content,
        /// and any other value of content overwrites the file content.
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// text or base64. text is default.
        /// </summary>
        [JsonProperty("encoding")]
        public string Encoding { get; set; }

        /// <summary>
        /// Last known file commit ID. Only considered in update, move, and delete actions.
        /// </summary>
        [JsonProperty("last_commit_id")]
        public string LastCommitId { get; set; }

        /// <summary>
        /// When true/false enables/disables the execute flag on the file. Only considered for chmod action.
        /// </summary>
        [JsonProperty("execute_filemode")]
        public bool ExecuteFilemode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommitActionRequest"/> class.
        /// </summary>
        /// <param name="action">Name of the branch to commit into. To create a new branch, also provide either start_branch or start_sha, and optionally start_project.</param>
        /// <param name="filePath">Commit message.</param>
        public CreateCommitActionRequest(string action, string filePath)
        {
            Guard.NotEmpty(action, nameof(action));
            Guard.NotEmpty(filePath, nameof(filePath));

            Action = action;
            FilePath = filePath;
        }
    }
}
