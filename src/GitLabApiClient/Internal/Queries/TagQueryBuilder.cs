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
            if (!string.IsNullOrEmpty(options.Search))
                Add("search", options.Search);

            Add("order_by", options.OrderBy.ToString().ToLower());
            Add("sort", options.Sort.ToString().ToLower());
        }
    }
}
