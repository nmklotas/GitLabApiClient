using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Releases.Requests;
using GitLabApiClient.Models.Releases.Responses;

namespace GitLabApiClient
{
    public interface IReleaseClient
    {
        /// <summary>
        /// Retrieves a release by its name
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The release, you want to retrieve.</param>
        /// <returns></returns>
        Task<Release> GetAsync(ProjectId projectId, string tagName);

        /// <summary>
        /// Get a list of releases
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options <see cref="ReleaseQueryOptions"/>.</param>
        /// <returns></returns>
        Task<IList<Release>> GetAsync(ProjectId projectId, Action<ReleaseQueryOptions> options = null);

        /// <summary>
        /// Create a release
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create release request.</param>
        /// <returns></returns>
        Task<Release> CreateAsync(ProjectId projectId, CreateReleaseRequest request);

        /// <summary>
        /// Update a release
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The tag name of the release, you want to update.</param>
        /// <param name="request">Update release request</param>
        /// <returns></returns>
        Task<Release> UpdateAsync(ProjectId projectId, string tagName, UpdateReleaseRequest request);

        /// <summary>
        /// Delete a release
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The tag name of the release, you want to delete.</param>
        /// <returns></returns>
        Task DeleteAsync(ProjectId projectId, string tagName);
    }
}
