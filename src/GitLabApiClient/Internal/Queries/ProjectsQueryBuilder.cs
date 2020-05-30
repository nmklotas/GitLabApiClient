using System;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Projects.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class ProjectsQueryBuilder : QueryBuilder<ProjectQueryOptions>
    {
        protected override void BuildCore(Query query, ProjectQueryOptions options)
        {
            if (!options.UserId.IsNullOrEmpty())
                query.Add("user_id", options.UserId);

            if (options.Archived)
                query.Add("archived", options.Archived);

            if (options.Visibility != QueryProjectVisibilityLevel.All)
                query.Add("visibility", GetVisibilityQueryValue(options.Visibility));

            if (options.Order != ProjectsOrder.CreatedAt)
                query.Add("order_by", GetProjectOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                query.Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (!options.Filter.IsNullOrEmpty())
                query.Add("search", options.Filter);

            if (options.Simple)
                query.Add("simple", options.Simple);

            if (options.Owned)
                query.Add("owned", options.Owned);

            if (options.IsMemberOf)
                query.Add("membership", options.IsMemberOf);

            if (options.Starred)
                query.Add("starred", options.Starred);

            if (options.IncludeStatistics)
                query.Add("statistics", options.IncludeStatistics);

            if (options.WithIssuesEnabled)
                query.Add("with_issues_enabled", options.WithIssuesEnabled);

            if (options.WithMergeRequestsEnabled)
                query.Add("with_merge_requests_enabled", options.WithMergeRequestsEnabled);

            if (options.LastActivityAfter.Year != 0001) //Not Default year
                query.Add("last_activity_after", options.LastActivityAfter.ToUniversalTime().ToString("o")); //Format: ISO 8601 YYYY-MM-DDTHH:MM:SSZ

            if (options.LastActivityBefore.Year != 0001) //Not Default year
                query.Add("last_activity_before", options.LastActivityBefore.ToUniversalTime().ToString("o")); //Format: ISO 8601 YYYY-MM-DDTHH:MM:SSZ
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
