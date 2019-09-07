using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Utilities;

namespace GitLabApiClient.Models.Branches.Requests
{
    /// <summary>
    /// Deletes a Branh from a project.
    /// </summary>
    public sealed class DeleteBranchRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteBranchRequest"/> class.
        /// </summary>
        /// <param name="id">Id or URL-Encoded path of the project</param>
        /// <param name="branch">Name of the branch</param>
        public DeleteBranchRequest(string id, string branch)
        {
            Guard.NotNullOrDefault(id, nameof(id));
            Guard.NotEmpty(branch, nameof(branch));
            Id = id;
            Branch = branch;
        }

        /// <summary>
        /// Project Id.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Branch Name.
        /// </summary>
        public string Branch { get; set; }
    }
}
