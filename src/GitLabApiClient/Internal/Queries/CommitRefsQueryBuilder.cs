using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Commits.Requests;
using GitLabApiClient.Models.Commits.Responses;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class CommitRefsQueryBuilder : QueryBuilder<CommitRefsQueryOptions>
    {
        protected override void BuildCore(CommitRefsQueryOptions options)
        {
            if (options.Type != CommitRefType.All)
            {
                Add("type", options.Type.ToLowerCaseString());
            }
        }
    }
}
