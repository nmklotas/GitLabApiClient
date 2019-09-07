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
    public sealed class CreateBranch
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBranch"/> class
        /// </summary>
        /// <param name="id">Id or URL-Encoded Path of the project.</param>
        /// <param name="branch">Name of the Branch.</param>
        /// <param name="reference">Branch name or commit SHA to create branch from.</param>
        public CreateBranch(int id, string branch, string reference)
        {
            Guard.NotNullOrDefault(id, nameof(id));
            Guard.NotEmpty(branch, nameof(branch));
            Guard.NotEmpty(reference, nameof(reference));
            Id = id;
            Branch = branch;
            Reference = reference;
        }

        /// <summary>
        /// Project ID.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

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
