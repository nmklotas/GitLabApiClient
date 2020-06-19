using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Wikis.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class WikisQueryBuilder : QueryBuilder<WikisQueryOptions>
    {
        protected override void BuildCore(WikisQueryOptions options)
        {
            if (!options.Slug.IsNullOrEmpty())
                Add("slug", options.Slug);
            if (options.WithContent)
                Add("with_content", options.WithContent);
        }
    }
}
