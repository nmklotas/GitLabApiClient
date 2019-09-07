using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Models.Releases.Requests;

namespace GitLabApiClient.Internal.Queries
{
    class ReleaseQueryBuilder : QueryBuilder<ReleaseQueryOptions>
    {
        protected override void BuildCore(ReleaseQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.ProjectId))
                Add("id", options.ProjectId);

            if (!string.IsNullOrEmpty(options.TagName))
                Add("tag_name", options.TagName);
        }
    }
}
