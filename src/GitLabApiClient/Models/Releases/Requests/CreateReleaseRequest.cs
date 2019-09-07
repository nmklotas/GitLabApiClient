using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Releases.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Releases.Requests
{
    /// <summary>
    /// Used to create a release in a project.
    /// </summary>
    public sealed class CreateReleaseRequest : Release
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReleaseRequest"/> class.
        /// </summary>
        /// <param name="projectId">The ID or URL-encoded path of the project.</param>
        /// <param name="releaseName">The name of the release.</param>
        /// <param name="tagName">The name of the tag the release correspons to.</param>
        /// <param name="description">A description of the release</param>
        /// <param name="releasedAt">The date the release will be/was ready.</param>
        public CreateReleaseRequest(string projectId, string releaseName, string tagName, string description, DateTime? releasedAt = null)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.NotEmpty(releaseName, nameof(releaseName));
            Guard.NotEmpty(tagName, nameof(tagName));

            ProjectId = projectId;
            ReleaseName = releaseName;
            TagName = tagName;
            Description = description;
            ReleasedAt = (releasedAt == null) ? DateTime.Now : releasedAt;
        }
    }
}
