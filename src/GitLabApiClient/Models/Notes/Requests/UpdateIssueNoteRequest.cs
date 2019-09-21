using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Notes.Requests
{
    /// <summary>
    /// Used to update issue notes in a project.
    /// </summary>
    public sealed class UpdateIssueNoteRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateIssueNoteRequest"/> class.
        /// </summary>
        /// <param name="projectId">The ID or URL-encoded path of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="noteId">The ID of a note.</param>
        /// <param name="body ">The content of a note.</param>
        public UpdateIssueNoteRequest(string projectId, int issueIid, int noteId, string body)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.NotEmpty(body, nameof(body));
            ProjectId = projectId;
            IssueIid = issueIid;
            NoteId = noteId;
            Body = body;
        }

        /// <summary>
        /// The ID or URL-encoded path of the project.
        /// </summary>
        [JsonProperty("id")]
        public string ProjectId { get; }

        /// <summary>
        ///  The IID of an issue.
        /// </summary>
        [JsonProperty("issue_iid")]
        public int IssueIid { get; }

        /// <summary>
        /// The ID of a note.
        /// </summary>
        [JsonProperty("note_id")]
        public int NoteId { get; }

        /// <summary>
        /// The content of a note.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; }
    }
}
