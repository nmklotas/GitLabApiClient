using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Commits.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class CommitStatusesQueryBuilder : QueryBuilder<CommitStatusesQueryOptions>
    {
        protected override void BuildCore(CommitStatusesQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.Ref))
                Add("ref", options.Ref);

            if (options.Name.IsNotNullOrEmpty())
                Add("name", options.Name);

            if (options.Stage.IsNotNullOrEmpty())
                Add("stage", options.Stage);

            if (options.All.HasValue)
                Add("all", options.All.Value);

        }
    }
}
