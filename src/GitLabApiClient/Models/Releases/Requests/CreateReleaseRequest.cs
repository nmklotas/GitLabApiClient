using System;
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
        /// <param name="releaseName">The name of the release.</param>
        /// <param name="tagName">The name of the tag the release correspons to.</param>
        /// <param name="description">A description of the release</param>
        /// <param name="releasedAt">The date the release will be/was ready.</param>
        public CreateReleaseRequest(string releaseName, string tagName, string description, DateTime? releasedAt = null)
        {
            Guard.NotEmpty(releaseName, nameof(releaseName));
            Guard.NotEmpty(tagName, nameof(tagName));

            ReleaseName = releaseName;
            TagName = tagName;
            Description = description;
            ReleasedAt = (releasedAt == null) ? DateTime.Now : releasedAt;
        }

        [JsonProperty("ref")]
        public string Ref { get; set; }
    }
}
