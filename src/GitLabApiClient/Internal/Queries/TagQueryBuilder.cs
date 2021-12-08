using GitLabApiClient.Models.Tags.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class TagQueryBuilder : QueryBuilder<TagQueryOptions>
    {
        protected override void BuildCore(Query query, TagQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.Search))
                query.Add("search", options.Search);

            query.Add("order_by", options.OrderBy.ToString().ToLower());
            query.Add("sort", options.Sort.ToString().ToLower());
        }
    }
}
