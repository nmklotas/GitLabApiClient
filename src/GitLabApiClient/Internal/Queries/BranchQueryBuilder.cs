using GitLabApiClient.Models.Branches.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class BranchQueryBuilder : QueryBuilder<BranchQueryOptions>
    {
        protected override void BuildCore(Query query, BranchQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.Search))
                query.Add("search", options.Search);
            if (!string.IsNullOrEmpty(options.Page))
                query.Add("page", options.Page);
            if (!string.IsNullOrEmpty(options.PerPage))
                query.Add("per_page", options.PerPage);
        }
    }
}
