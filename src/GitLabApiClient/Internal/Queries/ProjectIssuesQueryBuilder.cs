using GitLabApiClient.Models.Issues.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class ProjectIssuesQueryBuilder : IssuesQueryBuilder
    {
        protected override void BuildCore(IssuesQueryOptions options)
        {
            if (!(options is ProjectIssuesQueryOptions projectIssuesQueryOptions))
            {
                base.BuildCore(options);
                return;
            }

            base.BuildCore(options);

            if (projectIssuesQueryOptions.CreatedAfter.HasValue)
                Add("created_after", projectIssuesQueryOptions.CreatedAfter.Value);

            if (projectIssuesQueryOptions.CreatedBefore.HasValue)
                Add("created_before", projectIssuesQueryOptions.CreatedBefore.Value);
        }
    }
}
