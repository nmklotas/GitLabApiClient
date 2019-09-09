using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Utilities;

namespace GitLabApiClient.Models.Tags.Requests
{
    /// <summary>
    /// Used to delete a tag in a project
    /// </summary>
    public sealed class DeleteTagRequest
    {
        /// <summary>
        /// ID or URL-encoded path of the project
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Name of the Tag
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Initializes a instance of the <see cref="DeleteTagRequest"/> class.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="tagName"></param>
        public DeleteTagRequest(string projectId, string tagName)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.NotEmpty(tagName, nameof(tagName));

            ProjectId = projectId;
            TagName = tagName;
        }
    }
}
