using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Models.Commits.Requests;
using GitLabApiClient.Models.Tags.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class CommitQueryBuilder : QueryBuilder<CommitQueryOptions>
    {
        protected override void BuildCore(CommitQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.ProjectId))
                Add("id", options.ProjectId);

            if (!string.IsNullOrEmpty(options.RefName))
                Add("ref_name", options.RefName);
        }
    }
}
