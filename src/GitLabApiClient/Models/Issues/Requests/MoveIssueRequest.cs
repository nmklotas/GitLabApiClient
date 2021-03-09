using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Issues.Requests
{
    /// <summary>
    /// Used to move issues to another project.
    /// </summary>
    public sealed class MoveIssueRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveIssueRequest"/> class.
        /// </summary>
        /// <param name="title">Title of the issue.</param>
        public MoveIssueRequest(string toProjectId)
        {
            Guard.NotEmpty(toProjectId, nameof(toProjectId));
            ToProjectId = toProjectId;
        }

        /// <summary>
        /// The projectId of the project where the issue should be moved to
        /// </summary>
        [JsonProperty("to_project_id")]
        public string ToProjectId { get; }
    }
}
