using System.Collections.Generic;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Commits.Requests
{
    /// <summary>
    /// Used to create a commit in a project.
    /// </summary>
    public sealed class CreateCommitRequest
    {
        /// <summary>
        /// Name of the branch to commit into. To create a new branch, also provide either start_branch or start_sha, and optionally start_project.
        /// </summary>
        [JsonProperty("branch")]
        public string Branch { get; set; }

        /// <summary>
        /// Commit message.
        /// </summary>
        [JsonProperty("commit_message")]
        public string CommitMessage { get; set; }

        /// <summary>
        /// Name of the branch to start the new branch from.
        /// </summary>
        [JsonProperty("start_branch")]
        public string StartBranch { get; set; }

        /// <summary>
        /// SHA of the commit to start the new branch from.
        /// </summary>
        [JsonProperty("start_sha")]
        public string StartSha { get; set; }

        /// <summary>
        /// The project ID or URL-encoded path of the project to start the new branch from. Defaults to the value of project id.
        /// </summary>
        [JsonProperty("start_project")]
        public string ReleaseDescription { get; set; }

        /// <summary>
        /// Specify the commit author's email address.
        /// </summary>
        [JsonProperty("author_email")]
        public string AuthorEmail { get; set; }

        /// <summary>
        /// Specify the commit author's name.
        /// </summary>
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// Include commit stats. Default is true.
        /// </summary>
        [JsonProperty("stats")]
        public bool Stats { get; set; }

        /// <summary>
        /// When true overwrites the target branch with a new commit based on the start_branch or start_sha.
        /// </summary>
        [JsonProperty("force")]
        public bool Force { get; set; }
        
        /// <summary>
        /// A list of action hashes to commit as a batch.
        /// </summary>
        [JsonProperty("actions")]
        public IList<CreateCommitActionRequest> Actions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommitRequest"/> class.
        /// </summary>
        /// <param name="branch">Name of the branch to commit into. To create a new branch, also provide either start_branch or start_sha, and optionally start_project.</param>
        /// <param name="commitMessage">Commit message.</param>
        /// <param name="actions">A list of action hashes to commit as a batch.</param>
        public CreateCommitRequest(string branch, string commitMessage, IList<CreateCommitActionRequest> actions)
        {
            Guard.NotEmpty(branch, nameof(branch));

            Branch = branch;
            CommitMessage = commitMessage;
            Actions = actions;
            Stats = true;
        }
    }
}
