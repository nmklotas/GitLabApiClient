using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Commits.Requests.CreateCommitRequest
{
    public class CreateCommitRequestAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommitRequestAction"/> class.
        /// </summary>
        /// <param name="action">The action to perform, see <see cref="CreateCommitRequestActionType"/>.</param>
        /// <param name="filePath">Full path to the file.</param>
        public CreateCommitRequestAction(CreateCommitRequestActionType action, string filePath)
        {
            Guard.NotEmpty(filePath, nameof(filePath));

            Action = action;
            FilePath = filePath;
            //Default of GitLab
            Encoding = CreateCommitRequestActionEncoding.Text;
        }

        /// <summary>
        /// The action to perform, see <see cref="CreateCommitRequestActionType"/>.
        /// </summary>
        [JsonProperty("action")]
        public CreateCommitRequestActionType Action { get; set; }

        /// <summary>
        /// Full path to the file. Ex. lib/class.rb
        /// </summary>
        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        /// <summary>
        /// Original full path to the file being moved. Ex. lib/class1.rb. Only considered for move action.
        /// </summary>
        [JsonProperty("previous_path")]
        public string PreviousPath { get; set; }

        /// <summary>
        /// File content, required for all except delete, chmod, and move. Move actions that do not specify content
        /// preserve the existing file content, and any other value of content overwrites the file content.
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// text or base64. text is default. See <see cref="CreateCommitRequestActionEncoding"/>.
        /// </summary>
        [JsonProperty("encoding")]
        public CreateCommitRequestActionEncoding Encoding { get; set; }

        /// <summary>
        /// Last known file commit ID. Only considered in update, move, and delete actions.
        /// </summary>
        [JsonProperty("last_commit_id")]
        public string LastCommitId { get; set; }

        /// <summary>
        /// When true/false enables/disables the execute flag on the file. Only considered for chmod action.
        /// </summary>
        // ReSharper disable once StringLiteralTypo
        [JsonProperty("execute_filemode")]
        public bool? ExecuteFileMode { get; set; }
    }
}
