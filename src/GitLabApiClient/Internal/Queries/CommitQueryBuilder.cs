using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Commits.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class CommitQueryBuilder : QueryBuilder<CommitQueryOptions>
    {
        protected override void BuildCore(Query query, CommitQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.RefName))
                query.Add("ref_name", options.RefName);

            if (options.Path.IsNotNullOrEmpty())
                query.Add("path", options.Path);

            if (options.Since.HasValue)
                query.Add("since", options.Since.Value);

            if (options.Until.HasValue)
                query.Add("until", options.Until.Value);

            if (options.All.HasValue)
                query.Add("all", options.All.Value);

            if (options.WithStats.HasValue)
                query.Add("with_stats", options.WithStats.Value);

            if (options.FirstParent.HasValue)
                query.Add("first_parent", options.FirstParent.Value);
        }
    }
}
