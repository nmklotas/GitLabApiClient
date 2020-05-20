using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Tags.Requests;
using GitLabApiClient.Models.Tags.Responses;

namespace GitLabApiClient
{
    public sealed class TagClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal TagClient(GitLabHttpFacade httpFacade) => _httpFacade = httpFacade;

        /// <summary>
        /// Retrieves a tag by its name
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The tag, you want to retrieve.</param>
        /// <returns></returns>
        public async Task<Tag> GetAsync(ProjectId projectId, string tagName) =>
            await _httpFacade.Get<Tag>($"projects/{projectId}/repository/tags/{tagName}");

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options for tags <see cref="TagQueryOptions"/></param>
        /// <returns></returns>
        public async Task<IList<Tag>> GetAsync(ProjectId projectId, Action<TagQueryOptions> options = null)
        {
            var queryOptions = new TagQueryOptions();
            options?.Invoke(queryOptions);

            string url = new TagQueryBuilder().Build($"projects/{projectId}/repository/tags", queryOptions);
            return await _httpFacade.GetPagedList<Tag>(url);
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create Tag request.</param>
        /// <returns>newly created Tag</returns>
        public async Task<Tag> CreateAsync(ProjectId projectId, CreateTagRequest request) =>
            await _httpFacade.Post<Tag>($"projects/{projectId}/repository/tags", request);

        /// <summary>
        /// Delete a tag
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The tag name, you want to delete.</param>
        public async Task DeleteAsync(ProjectId projectId, string tagName) =>
            await _httpFacade.Delete($"projects/{projectId}/repository/tags/{tagName}");
    }
}
