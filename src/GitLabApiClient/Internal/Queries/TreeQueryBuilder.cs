using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Models.Trees.Requests;
using GitLabApiClient.Models.Tags.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class TreeQueryBuilder : QueryBuilder<TreeQueryOptions>
    {
        protected override void BuildCore(TreeQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.Reference))
                Add("ref", options.Reference);

            if (!string.IsNullOrEmpty(options.Path))
                Add("path", options.Path);

            if (options.Recursive)
                Add("recursive", options.Recursive);

        }
    }
}
