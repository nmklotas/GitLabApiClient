using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Commits.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class CommitStatusesQueryBuilder : QueryBuilder<CommitStatusesQueryOptions>
    {
        protected override void BuildCore(Query query, CommitStatusesQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.Ref))
                query.Add("ref", options.Ref);

            if (options.Name.IsNotNullOrEmpty())
                query.Add("name", options.Name);

            if (options.Stage.IsNotNullOrEmpty())
                query.Add("stage", options.Stage);

            if (options.All.HasValue)
                query.Add("all", options.All.Value);
        }
    }
}
