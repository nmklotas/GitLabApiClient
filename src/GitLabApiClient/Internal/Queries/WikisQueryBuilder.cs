using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Wikis.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Internal.Queries
{
    internal class WikisQueryBuilder : QueryBuilder<WikisQueryOptions>
    {
        protected override void BuildCore(WikisQueryOptions options)
        {
            if (!options.Slug.IsNullOrEmpty())
                Add("slug", options.Slug);
            if (options.WithContent)
                Add("with_content", options.WithContent);
        }
    }
}
