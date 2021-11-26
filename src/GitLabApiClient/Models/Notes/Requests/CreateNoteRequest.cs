using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Notes.Requests
{
    /// <summary>
    /// Used to create issue notes in a project.
    /// </summary>
    public sealed class CreateNoteRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNoteRequest"/> class.
        /// </summary>
        /// <param name="body">The content of a note.</param>
        public CreateNoteRequest(string body) => Body = body;

        public CreateNoteRequest()
        {
        }

        /// <summary>
        /// The content of a note.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// Date time string, ISO 8601 formatted, e.g. 2016-03-11T03:45:40Z (requires admin or project/group owner rights)
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}
