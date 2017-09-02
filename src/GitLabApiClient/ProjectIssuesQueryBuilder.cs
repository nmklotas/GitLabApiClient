using System.Collections.Specialized;
using GitLabApiClient.Models.Issues;

namespace GitLabApiClient
{
    internal sealed class ProjectIssuesQueryBuilder : IssuesQueryBuilder
    {
        protected override void BuildCore(NameValueCollection nameValues, IssuesQueryOptions options)
        {
            if (!(options is ProjectIssuesQueryOptions projectIssuesQueryOptions))
            {
                base.BuildCore(nameValues, options);
                return;
            }

            nameValues.Add("id", projectIssuesQueryOptions.ProjectId);
            base.BuildCore(nameValues, options);

            if (projectIssuesQueryOptions.CreatedAfter.HasValue)
                nameValues.Add("created_after", GetDateValue(projectIssuesQueryOptions.CreatedAfter.Value));

            if (projectIssuesQueryOptions.CreatedBefore.HasValue)
                nameValues.Add("created_before", GetDateValue(projectIssuesQueryOptions.CreatedBefore.Value));
        }
    }
}
