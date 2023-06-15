using GitLabApiClient.Models.Branches.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class BranchQueryBuilder : QueryBuilder<BranchQueryOptions>
    {
        protected override void BuildCore(Query query, BranchQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.Search))
                query.Add("search", options.Search);
            if (options.Page != null)
                query.Add("page", options.Page.ToString());
            if (options.PerPage != null)
                query.Add("per_page", options.PerPage.ToString());
        }
    }
}
