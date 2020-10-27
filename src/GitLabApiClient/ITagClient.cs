using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Tags.Requests;
using GitLabApiClient.Models.Tags.Responses;

namespace GitLabApiClient
{
    public interface ITagClient
    {
        /// <summary>
        /// Retrieves a tag by its name
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The tag, you want to retrieve.</param>
        /// <returns></returns>
        Task<Tag> GetAsync(ProjectId projectId, string tagName);

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options for tags <see cref="TagQueryOptions"/></param>
        /// <returns></returns>
        Task<IList<Tag>> GetAsync(ProjectId projectId, Action<TagQueryOptions> options = null);

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create Tag request.</param>
        /// <returns>newly created Tag</returns>
        Task<Tag> CreateAsync(ProjectId projectId, CreateTagRequest request);

        /// <summary>
        /// Delete a tag
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The tag name, you want to delete.</param>
        Task DeleteAsync(ProjectId projectId, string tagName);
    }
}
