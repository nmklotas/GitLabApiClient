using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Utilities;

namespace GitLabApiClient.Models.Branches.Requests
{
    /// <summary>
    /// Deletes all merged Branches of a project.
    /// </summary>
    public sealed class DeleteMergedBranchesRequest
    {
        /// <summary>
        /// Initializes a new instnce of the <see cref="DeleteMergedBranchesRequest"/> class.
        /// </summary>
        /// <param name="id"></param>
        public DeleteMergedBranchesRequest(string id)
        {
            Guard.NotEmpty(id, nameof(id));
            ProjectId = id;
        }

        /// <summary>
        /// Id or URL-Encoded path of the project.
        /// </summary>
        public string ProjectId { get; set; }

    }
}
