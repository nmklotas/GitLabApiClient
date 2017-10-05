using System;
using System.Linq;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient.Internal.Queries
{
    internal class GroupsQueryBuilder : QueryBuilder<GroupsQueryOptions>
    {
        protected override void BuildCore(GroupsQueryOptions options)
        {

            if (options.SkipGroups.Any())
                Add("skip_groups", options.SkipGroups);


            Add("all_available", options.AllAvailable);

            Add("order_by", GetOrderQueryValue(options.Order));

            Add("sort", GetSortQueryValue(options.Sort));

            Add("statistics", options.Statistics);

            Add("owned", options.Owned);
        }

        private static string GetOrderQueryValue(GroupsOrder order)
        {
            switch (order)
            {
                case GroupsOrder.Path:
                    return "path";
                default:
                    return "name";
            }
        }

        private static string GetSortQueryValue(GroupsSort sort)
        {
            switch (sort)
            {
                case GroupsSort.Descending:
                    return "desc";
                default:
                    return "asc";
            }
        }
    }
}