using System;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Groups.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class GroupsQueryBuilder : QueryBuilder<GroupsQueryOptions>
    {
        protected override void BuildCore(Query query, GroupsQueryOptions options)
        {
            query.Add("skip_groups", options.SkipGroups);

            if (options.AllAvailable)
                query.Add("all_available", options.AllAvailable);

            if (!options.Search.IsNullOrEmpty())
                query.Add("search", options.Search);

            if (options.Order != GroupsOrder.Name)
                query.Add("order_by", GetOrderQueryValue(options.Order));

            if (options.Sort != GroupsSort.Ascending)
                query.Add("sort", GetSortQueryValue(options.Sort));

            if (options.Statistics)
                query.Add("statistics", options.Statistics);

            if (options.WithCustomAttributes)
                query.Add("with_custom_attributes", options.WithCustomAttributes);

            if (options.Owned)
                query.Add("owned", options.Owned);

            if (options.MinAccessLevel.HasValue)
                query.Add("min_access_level", (int)options.MinAccessLevel.Value);
        }

        private static string GetOrderQueryValue(GroupsOrder order)
        {
            switch (order)
            {
                case GroupsOrder.Name:
                    return "name";
                case GroupsOrder.Path:
                    return "path";
                default:
                    throw new NotSupportedException($"GroupsOrder {order} is not supported");
            }
        }

        private static string GetSortQueryValue(GroupsSort sort)
        {
            switch (sort)
            {
                case GroupsSort.Ascending:
                    return "asc";
                case GroupsSort.Descending:
                    return "desc";
                default:
                    throw new NotSupportedException($"GroupsSort {sort} is not supported");
            }
        }
    }
}
