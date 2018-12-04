using System;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Notes.Requests
{
    /// <summary>
    /// Used to create issue notes in a project.
    /// </summary>
    public sealed class CreateIssueNoteRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateIssueNoteRequest"/> class.
        /// </summary>
        /// <param name="projectId">The ID or URL-encoded path of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="body ">The content of a note.</param>
        public CreateIssueNoteRequest(string projectId, int issueIid, string body)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.NotEmpty(body, nameof(body));
            ProjectId = projectId;
            IssueIid = issueIid;
            Body = body;
        }

        /// <summary>
        /// The ID or URL-encoded path of the project.
        /// </summary>
        [JsonProperty("id")]
        public string ProjectId { get; }

        /// <summary>
        /// The IID of an issue.
        /// </summary>
        [JsonProperty("issue_iid ")]
        public int IssueIid { get; }

        /// <summary>
        /// The content of a note.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; }

        /// <summary>
        /// Date time string, ISO 8601 formatted, e.g. 2016-03-11T03:45:40Z (requires admin or project/group owner rights)
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}
