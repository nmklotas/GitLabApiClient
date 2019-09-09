using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Models.Tags.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class TagQueryBuilder : QueryBuilder<TagQueryOptions>
    {
        protected override void BuildCore(TagQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.ProjectId))
                Add("id", options.ProjectId);

            if (!string.IsNullOrEmpty(options.TagName))
                Add("tag_name", options.TagName);
        }
    }
}
