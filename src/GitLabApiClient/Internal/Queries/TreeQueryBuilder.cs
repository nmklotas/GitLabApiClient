using GitLabApiClient.Models.Trees.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class TreeQueryBuilder : QueryBuilder<TreeQueryOptions>
    {
        protected override void BuildCore(Query query, TreeQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.Reference))
                query.Add("ref", options.Reference);

            if (!string.IsNullOrEmpty(options.Path))
                query.Add("path", options.Path);

            if (options.Recursive)
                query.Add("recursive", options.Recursive);
        }
    }
}
