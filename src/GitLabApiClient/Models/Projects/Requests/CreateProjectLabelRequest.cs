using System.Collections.Generic;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Requests
{
    /// <summary>
    /// Used to create labels in a project.
    /// </summary>
    public sealed class CreateProjectLabelRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProjectLabelRequest"/> class.
        /// </summary>
        /// <param name="projectId">The ID or URL-encoded path of the project owned by the authenticated user.</param>
        /// <param name="name">Name of the label.</param>
        public CreateProjectLabelRequest(string projectId, string name)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.NotEmpty(name, nameof(name));
            ProjectId = projectId;
            Name = name;
        }

        /// <summary>
        /// The ID or URL-encoded path of the project owned by the authenticated user.
        /// </summary>
        [JsonProperty("id")]
        public string ProjectId { get; private set; }

        /// <summary>
        /// The name of the label.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// The color of the label given in 6-digit hex notation with leading ‘#’ sign (e.g. #FFAABB) or one of the CSS color names.
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// The description of the label.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The priority of the label. Must be greater or equal than zero or null to remove the priority.
        /// </summary>
        [JsonProperty("priority")]
        public int? Priority { get; set; }
    }
}
