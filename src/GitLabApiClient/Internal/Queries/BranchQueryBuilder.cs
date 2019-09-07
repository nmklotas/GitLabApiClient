using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Models.Branches.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class BranchQueryBuilder : QueryBuilder<BranchQueryOptions>
    {
        protected override void BuildCore(BranchQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.ProjectId))
                Add("id", options.ProjectId);

            if (!string.IsNullOrEmpty(options.BranchName))
                Add("branch", options.BranchName);
        }
    }
}
