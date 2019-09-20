using System;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Groups.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class ProjectsGroupQueryBuilder : QueryBuilder<ProjectsGroupQueryOptions>
    {
        protected override void BuildCore(ProjectsGroupQueryOptions options)
        {
            if (options.Archived)
                Add("archived", options.Archived);

            if (options.Visibility != GroupsVisibility.Public)
                Add("visibility", GetVisibilityQueryValue(options.Visibility));

            if (options.Order != GroupsProjectsOrder.CreatedAt)
                Add("order_by", GetOrderQueryValue(options.Order));

            if (options.Sort != GroupsSort.Ascending)
                Add("sort", GetSortQueryValue(options.Sort));

            if (!options.Search.IsNullOrEmpty())
                Add("search", options.Search);

            if (options.Simple)
                Add("simple", options.Simple);

            if (options.Owned)
                Add("owned", options.Owned);

            if (options.Starred)
                Add("starred", options.Starred);

            if (options.WithIssuesEnabled)
                Add("with_issues_enabled", options.WithIssuesEnabled);

            if (options.WithMergeRequestsEnabled)
                Add("with_merge_requests_enabled", options.WithMergeRequestsEnabled);

            if (!options.WithShared)
                Add("with_shared", options.WithShared);

            if (options.IncludeSubgroups)
                Add("include_subgroups", options.IncludeSubgroups);

            if (options.MinAccessLevel != null)
                Add("min_access_level", (int) options.MinAccessLevel);

            if (options.WithCustomAttributes)
                Add("with_custom_attributes", options.WithCustomAttributes);

            if (options.WithSecurityReports)
                Add("with_security_reports", options.WithSecurityReports);

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
                    throw new NotSupportedException($"GroupsVisibility {visibility} is not supported");
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
                    throw new NotSupportedException($"GroupsProjectsOrder {order} is not supported");
            }
        }
    }
}