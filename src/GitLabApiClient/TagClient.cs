﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Tags.Requests;
using GitLabApiClient.Models.Tags.Responses;

namespace GitLabApiClient
{
    public sealed class TagClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly TagQueryBuilder _tagQueryBuilder;

        internal TagClient(
            GitLabHttpFacade httpFacade,
            TagQueryBuilder tagQueryBuilder)
        {
            _httpFacade = httpFacade;
            _tagQueryBuilder = tagQueryBuilder;
        }

        /// <summary>
        /// Retrieves a tag by its name
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The tag, you want to retrieve.</param>
        /// <returns></returns>
        public async Task<Tag> GetAsync(object projectId, string tagName) =>
            await _httpFacade.Get<Tag>($"{TagsBaseUrl(projectId)}/{tagName}");

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Query options for tags <see cref="TagQueryOptions"/></param>
        /// <returns></returns>
        public async Task<IList<Tag>> GetAsync(object projectId, Action<TagQueryOptions> options = null)
        {
            var queryOptions = new TagQueryOptions();
            options?.Invoke(queryOptions);

            string url = _tagQueryBuilder.Build(TagsBaseUrl(projectId), queryOptions);
            return await _httpFacade.GetPagedList<Tag>(url);
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create Tag request.</param>
        /// <returns>newly created Tag</returns>
        public async Task<Tag> CreateAsync(object projectId, CreateTagRequest request) =>
            await _httpFacade.Post<Tag>(TagsBaseUrl(projectId), request);

        /// <summary>
        /// Delete a tag
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="tagName">The tag name, you want to delete.</param>
        public async Task DeleteAsync(object projectId, string tagName) =>
            await _httpFacade.Delete($"{TagsBaseUrl(projectId)}/{tagName}");

        public static string TagsBaseUrl(object projectId) =>
            $"{projectId.ProjectBaseUrl()}/repository/tags";
    }
}
