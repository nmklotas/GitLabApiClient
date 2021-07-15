using System;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Releases.Requests;

namespace GitLabApiClient.Internal.Queries
{
    class ReleaseQueryBuilder : QueryBuilder<ReleaseQueryOptions>
    {
        protected override void BuildCore(Query query, ReleaseQueryOptions options)
        {
            if (options.Order != ReleasesOrder.ReleasedAt)
                query.Add("order_by", GetReleasesOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                query.Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (options.IncludeHtmlDescription)
                query.Add("include_html_description", true);
        }

        private static string GetReleasesOrderQueryValue(ReleasesOrder order)
        {
            switch (order)
            {
                case ReleasesOrder.CreatedAt:
                    return "created_at";
                case ReleasesOrder.ReleasedAt:
                    return "released_at";
                default:
                    throw new NotSupportedException($"Order {order} is not supported");
            }
        }
    }
}
