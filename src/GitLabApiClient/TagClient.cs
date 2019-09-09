﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
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

        public async Task<Tag> GetAsync(string projectId, string tagName) =>
            await _httpFacade.Get<Tag>($"projects/{projectId}/tags/{tagName}");

        public async Task<IList<Tag>> GetAsync(string projectId, Action<TagQueryOptions> options)
        {
            var queryOptions = new TagQueryOptions(projectId);
            options?.Invoke(queryOptions);

            string url = _tagQueryBuilder.Build($"projects/{projectId}/tags", queryOptions);
            return await _httpFacade.GetPagedList<Tag>(url);
        }

        public async Task<Tag> CreateAsync(CreateTagRequest request) =>
            await _httpFacade.Post<Tag>($"projects/{request.ProjectId}/repository/tags", request);

        public async Task DeleteAsync(DeleteTagRequest request) =>
            await _httpFacade.Delete($"projects/{request.ProjectId}/repository/tags/{request.TagName}");

    }
}
