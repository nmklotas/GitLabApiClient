using System;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Environments.Responses;
using Newtonsoft.Json;
using Environment = GitLabApiClient.Models.Environments.Responses.Environment;

namespace GitLabApiClient.Models.Environments.Requests
{
    /// <summary>
    /// Used to create an environment in a project.
    /// </summary>
    public sealed class CreateEnvironmentRequest : Environment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEnvironmentRequest"/> class.
        /// </summary>
        /// <param name="name">The name of the environment.</param>
        /// <param name="externalUrl">The external URL for the environment.</param>
        public CreateEnvironmentRequest(string name, Uri externalUrl)
        {
            Guard.NotEmpty(name, nameof(name));
            Guard.NotNullOrDefault(externalUrl, nameof(externalUrl));

            Name = name;
            ExternalUrl = externalUrl;
        }
    }
}
