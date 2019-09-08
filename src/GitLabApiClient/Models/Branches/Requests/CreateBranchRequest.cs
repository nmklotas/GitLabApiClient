using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Branches.Requests
{
    /// <summary>
    /// Creates a new branch on a project.
    /// </summary>
    public sealed class CreateBranchRequest
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBranch"/> class
        /// </summary>
        /// <param name="projectId">Id or URL-Encoded Path of the project.</param>
        /// <param name="branch">Name of the Branch.</param>
        /// <param name="reference">Branch name or commit SHA to create branch from.</param>
        public CreateBranchRequest(string projectId, string branch, string reference)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.NotEmpty(branch, nameof(branch));
            Guard.NotEmpty(reference, nameof(reference));
            ProjectId = projectId;
            Branch = branch;
            Reference = reference;
        }

        /// <summary>
        /// Project ID.
        /// </summary>
        [JsonProperty("id")]
        public string ProjectId { get; set; }

        /// <summary>
        /// Branch Name.
        /// </summary>
        [JsonProperty("branch")]
        public string Branch { get; set; }

        /// <summary>
        /// Branch Reference.
        /// </summary>
        [JsonProperty("ref")]
        public string Reference { get; set; }

    }
}
