using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Commits.Requests;
using GitLabApiClient.Models.Commits.Responses;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class CommitRefsQueryBuilder : QueryBuilder<CommitRefsQueryOptions>
    {
        protected override void BuildCore(Query query, CommitRefsQueryOptions options)
        {
            if (options.Type != CommitRefType.All)
            {
                query.Add("type", options.Type.ToLowerCaseString());
            }
        }
    }
}
