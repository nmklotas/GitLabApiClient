using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Commits.Requests;
using GitLabApiClient.Models.Tags.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class CommitQueryBuilder : QueryBuilder<CommitQueryOptions>
    {
        protected override void BuildCore(CommitQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.RefName))
                Add("ref_name", options.RefName);

            if (options.Path.IsNotNullOrEmpty())
                Add("path", options.Path);

            if (options.Since.HasValue)
                Add("since", options.Since.Value);

            if (options.Until.HasValue)
                Add("until", options.Until.Value);

            if (options.All.HasValue)
                Add("all", options.All.Value);

            if (options.WithStats.HasValue)
                Add("with_stats", options.WithStats.Value);

            if (options.FirstParent.HasValue)
                Add("first_parent", options.FirstParent.Value);

        }
    }
}
