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
        /// <param name="body">The content of a note.</param>
        public UpdateIssueNoteRequest(string body)
        {
            Guard.NotEmpty(body, nameof(body));
            Body = body;
        }

        public UpdateIssueNoteRequest()
        {
        }

        /// <summary>
        /// The content of a note.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
