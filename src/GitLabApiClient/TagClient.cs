using System;
using System.Collections.Generic;
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
            await _httpFacade.Get<Tag>($"{TagsBaseUrl(projectId)}/{tagName}");

        public async Task<IList<Tag>> GetAsync(string projectId, Action<TagQueryOptions> options)
        {
            var queryOptions = new TagQueryOptions();
            options?.Invoke(queryOptions);

            string url = _tagQueryBuilder.Build(TagsBaseUrl(projectId), queryOptions);
            return await _httpFacade.GetPagedList<Tag>(url);
        }

        public async Task<Tag> CreateAsync(CreateTagRequest request) =>
            await _httpFacade.Post<Tag>(TagsBaseUrl(request.ProjectId), request);

        public async Task DeleteAsync(DeleteTagRequest request) =>
            await _httpFacade.Delete($"{TagsBaseUrl(request.ProjectId)}/{request.TagName}");

        public static string TagsBaseUrl(string projectId)
        {
            return $"projects/{projectId}/repository/tags";
        }
    }
}
