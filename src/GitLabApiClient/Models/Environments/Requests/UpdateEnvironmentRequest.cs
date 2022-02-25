using System;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Environments.Requests
{
    /// <summary>
    /// Used to update an environment in a project
    /// </summary>
    public sealed class UpdateEnvironmentRequest
    {
        [JsonProperty("environment_id")]
        public int EnvironmentId { get; set; }

        [JsonProperty("external_url")]
        public Uri ExternalUrl { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEnvironmentRequest"/> class
        /// </summary>
        /// <param name="environmentId">The ID of the environment</param>
        /// <param name="externalUrl">The new external URL</param>
        public UpdateEnvironmentRequest(int environmentId, Uri externalUrl)
        {
            Guard.NotNullOrDefault(environmentId, nameof(environmentId));
            Guard.NotNullOrDefault(externalUrl, nameof(externalUrl));

            EnvironmentId = environmentId;
            ExternalUrl = externalUrl;
        }
    }
}
