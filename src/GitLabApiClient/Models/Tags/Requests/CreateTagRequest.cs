using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Tags.Requests
{
    /// <summary>
    /// Used to create a tag in a project.
    /// </summary>
    public sealed class CreateTagRequest
    {
        /// <summary>
        /// The ID or URL-encoded path of the project.
        /// </summary>
        [JsonProperty("id")]
        public string ProjectId { get; set; }

        /// <summary>
        /// Name for the created tag
        /// </summary>
        [JsonProperty("tag_name")]
        public string TagName { get; set; }

        /// <summary>
        /// Tag reference.
        /// </summary>
        [JsonProperty("ref")]
        public string Reference { get; set; }

        /// <summary>
        /// Annotated message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Release notes.
        /// </summary>
        [JsonProperty("release_description")]
        public string ReleaseDescription { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTagRequest"/> class.
        /// </summary>
        /// <param name="projectId">The ID or URL-encoded path of the project.</param>
        /// <param name="tagName">The name of the tag the release correspons to.</param>
        /// <param name="reference">Commit SHA, another tag name, or branh name.</param>
        /// <param name="message">Annotated message</param>
        /// <param name="releaseDescription">Release notes</param>
        public CreateTagRequest(string projectId, string tagName, string reference, string message, string releaseDescription)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.NotEmpty(tagName, nameof(tagName));
            Guard.NotEmpty(reference, nameof(reference));

            ProjectId = projectId;
            TagName = tagName;
            Reference = reference;
            Message = message;
            ReleaseDescription = releaseDescription;
        }
    }
}
