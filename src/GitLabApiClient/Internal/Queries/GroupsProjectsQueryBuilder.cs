using System;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Groups.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class ProjectsGroupQueryBuilder : QueryBuilder<ProjectsGroupQueryOptions>
    {
        protected override void BuildCore(Query query, ProjectsGroupQueryOptions options)
        {
            if (options.Archived)
                query.Add("archived", options.Archived);

            if (options.Visibility != GroupsVisibility.Public)
                query.Add("visibility", GetVisibilityQueryValue(options.Visibility));

            if (options.Order != GroupsProjectsOrder.CreatedAt)
                query.Add("order_by", GetOrderQueryValue(options.Order));

            if (options.Sort != GroupsSort.Ascending)
                query.Add("sort", GetSortQueryValue(options.Sort));

            if (!options.Search.IsNullOrEmpty())
                query.Add("search", options.Search);

            if (options.Simple)
                query.Add("simple", options.Simple);

            if (options.Owned)
                query.Add("owned", options.Owned);

            if (options.Starred)
                query.Add("starred", options.Starred);

            if (options.WithIssuesEnabled)
                query.Add("with_issues_enabled", options.WithIssuesEnabled);

            if (options.WithMergeRequestsEnabled)
                query.Add("with_merge_requests_enabled", options.WithMergeRequestsEnabled);

            if (!options.WithShared)
                query.Add("with_shared", options.WithShared);

            if (options.IncludeSubgroups)
                query.Add("include_subgroups", options.IncludeSubgroups);

            if (options.MinAccessLevel != null)
                query.Add("min_access_level", (int)options.MinAccessLevel);

            if (options.WithCustomAttributes)
                query.Add("with_custom_attributes", options.WithCustomAttributes);

            if (options.WithSecurityReports)
                query.Add("with_security_reports", options.WithSecurityReports);
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
