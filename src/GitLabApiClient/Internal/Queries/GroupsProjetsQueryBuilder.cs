using System;
using System.Linq;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient.Internal.Queries
{
    internal class GroupsProjectsQueryBuilder : QueryBuilder<GroupsProjectsQueryOptions>
    {
        protected override void BuildCore(GroupsProjectsQueryOptions options)
        {
            
            Add("id", options.Id?.ToString());

            Add("archived", options.Archived);

            Add("visibility", GetVisibilityQueryValue(options.Visibility));

            Add("order_by", GetOrderQueryValue(options.Order));

            Add("sort", GetSortQueryValue(options.Sort));

            if (!options.Search.IsNullOrEmpty())
                Add("search", options.Search);

            Add("simple", options.Simple);

            Add("owned", options.Owned);

            Add("starred", options.Starred);
        }

        private static string GetVisibilityQueryValue(GroupsVisibility visibility)
        {
            switch (visibility)
            {
                case GroupsVisibility.Public:
                    return "public";
                case GroupsVisibility.Internal:
                    return "internal";
                case GroupsVisibility.Private:
                    return "private";
                default:
                    return "public";
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

        private static string GetOrderQueryValue(GroupsProjectsOrder order)
        {
            switch (order)
            {
                case GroupsProjectsOrder.Id:
                    return "id";
                case GroupsProjectsOrder.Name:
                    return "name";
                case GroupsProjectsOrder.Path:
                    return "path";
                case GroupsProjectsOrder.CreatedAt:
                    return "created_at";
                case GroupsProjectsOrder.UpdatedAt:
                    return "updated_at";
                case GroupsProjectsOrder.LastiActivityAt:
                    return "last_activity_at";
                default:
                    return "created_at";
            }
        }
    }
}