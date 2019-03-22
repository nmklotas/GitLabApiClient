using System;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Groups.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class GroupsQueryBuilder : QueryBuilder<GroupsQueryOptions>
    {
        protected override void BuildCore(GroupsQueryOptions options)
        {
            Add("skip_groups", options.SkipGroups);

            if (options.AllAvailable)
                Add("all_available", options.AllAvailable);

            if (!options.Search.IsNullOrEmpty())
                Add("search", options.Search);

            if (options.Order != GroupsOrder.Name)
                Add("order_by", GetOrderQueryValue(options.Order));

            if (options.Sort != GroupsSort.Ascending)
                Add("sort", GetSortQueryValue(options.Sort));

            if (options.Statistics)
                Add("statistics", options.Statistics);

            if (options.WithCustomAttributes)
                Add("with_custom_attributes", options.WithCustomAttributes);

            if (options.Owned)
                Add("owned", options.Owned);

            if (options.MinAccessLevel.HasValue)
                Add("min_access_level", (int)options.MinAccessLevel.Value);
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