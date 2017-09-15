using System;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Projects.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class ProjectsQueryBuilder : QueryBuilder<ProjectQueryOptions>
    {
        protected override void BuildCore(ProjectQueryOptions options)
        {
            if (!options.UserId.IsNullOrEmpty())
                Add("user_id", options.UserId);

            if (options.Archived)
                Add("archived", options.Archived);

            if (options.Visibility != QueryProjectVisibilityLevel.All)
                Add("visibility", GetVisibilityQueryValue(options.Visibility));

            if (options.Order != ProjectsOrder.CreatedAt)
                Add("order_by", GetProjectOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (!options.Filter.IsNullOrEmpty())
                Add("search", options.Filter);

            if (options.Simple)
                Add("simple", options.Simple);

            if (options.Owned)
                Add("owned", options.Owned);

            if (options.IsMemberOf)
                Add("membership", options.IsMemberOf);

            if (options.Starred)
                Add("starred", options.Starred);

            if (options.IncludeStatistics)
                Add("statistics", options.IncludeStatistics);

            if (options.WithIssuesEnabled)
                Add("with_issues_enabled", options.WithIssuesEnabled);

            if (options.WithMergeRequestsEnabled)
                Add("with_merge_requests_enabled", options.WithMergeRequestsEnabled);
        }

        private static string GetProjectOrderQueryValue(ProjectsOrder order)
        {
            switch (order)
            {
                case ProjectsOrder.CreatedAt:
                    return "created_at";
                case ProjectsOrder.UpdatedAt:
                    return "updated_at";
                case ProjectsOrder.LastActivityAt:
                    return "last_activity_at";
                case ProjectsOrder.Id:
                    return "id";
                case ProjectsOrder.Name:
                    return "name";
                case ProjectsOrder.Path:
                    return "path";
                default:
                    throw new NotSupportedException($"Order {order} not supported");
            }
        }

        private static string GetVisibilityQueryValue(QueryProjectVisibilityLevel visibility)
        {
            switch (visibility)
            {
                case QueryProjectVisibilityLevel.Private:
                    return "private";
                case QueryProjectVisibilityLevel.Internal:
                    return "internal";
                case QueryProjectVisibilityLevel.Public:
                    return "public";
                case QueryProjectVisibilityLevel.All:
                    return "";
                default:
                    throw new NotSupportedException($"Visibility {visibility} not supported");
            }
        }
    }
}
