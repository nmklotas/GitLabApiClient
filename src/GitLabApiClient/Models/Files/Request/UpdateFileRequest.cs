using Newtonsoft.Json;

namespace GitLabApiClient.Models.Files.Request
{
    public class UpdateFileRequest
    {
        public UpdateFileRequest(string branch) => Branch = branch;

        /// <summary>
        /// Name of the branch
        /// </summary>
        [JsonProperty("branch")]
        public string Branch { get; private set; }

        /// <summary>
        /// Specify the commit author’s email address
        /// </summary>
        [JsonProperty("author_email")]
        public string AuthorEmail { get; set; }

        /// <summary>
        /// Specify the commit author’s name
        /// </summary>
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// New file content
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Commit message
        /// </summary>
        [JsonProperty("commit_message")]
        public string CommitMessage { get; set; }

    }
}
