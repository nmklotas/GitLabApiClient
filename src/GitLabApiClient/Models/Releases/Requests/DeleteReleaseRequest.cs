using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Releases.Requests
{
    /// <summary>
    /// Used to delete a release in a project.
    /// </summary>
    public sealed class DeleteReleaseRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteReleaseRequest"/> class
        /// </summary>
        /// <param name="projectId">The ID or URL-encoded path of the project.</param>
        /// <param name="tagName">The name of the tag which corresponds to the release</param>
        public DeleteReleaseRequest(string projectId, string tagName)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.NotEmpty(tagName, tagName);

            ProjectId = projectId;
            TagName = tagName;
        }

        [JsonProperty("id")]
        public string ProjectId { get; set; }

        [JsonProperty("tag_name")]
        public string TagName { get; set; }
    }
}
