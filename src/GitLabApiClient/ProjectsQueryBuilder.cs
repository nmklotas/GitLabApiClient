using System;
using System.Collections.Specialized;
using System.Linq;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Projects;
using GitLabApiClient.Utilities;
using static System.Web.HttpUtility;

namespace GitLabApiClient
{
    public class ProjectsQueryBuilder
    {
        public string Build(string baseUrl, ProjectQueryOptions options)
        {
            var nameValues = new NameValueCollection();

            if (!options.UserId.IsNullOrEmpty())
                nameValues.Add("user_id", options.UserId);

            if (options.Archived)
                nameValues.Add("archived", options.Archived.ToLowerCaseString());

            nameValues.Add("visibility", options.Visibility.ToLowerCaseString());

            if (options.Order != ProjectsOrder.CreatedAt)
                nameValues.Add("order_by", GetProjectOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                nameValues.Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (!options.Filter.IsNullOrEmpty())
                nameValues.Add("search", options.Filter);

            if (options.Simple)
                nameValues.Add("simple", options.Simple.ToLowerCaseString());

            if (options.Owned)
                nameValues.Add("owned", options.Owned.ToLowerCaseString());

            if (options.IsMemberOf)
                nameValues.Add("membership", options.IsMemberOf.ToLowerCaseString());

            if (options.Starred)
                nameValues.Add("starred", options.Starred.ToLowerCaseString());

            if (options.IncludeStatistics)
                nameValues.Add("statistics", options.IncludeStatistics.ToLowerCaseString());

            if (options.WithIssuesEnabled)
                nameValues.Add("with_issues_enabled", options.WithIssuesEnabled.ToLowerCaseString());

            if (options.WithMergeRequestsEnabled)
                nameValues.Add("with_merge_requests_enabled", options.WithMergeRequestsEnabled.ToLowerCaseString());

            return baseUrl + ToQueryString(nameValues);
        }

        private static string ToQueryString(NameValueCollection nvc)
        {
            var array =
                from key in nvc.AllKeys
                from value in nvc.GetValues(key)
                select string.Format("{0}={1}", UrlEncode(key), UrlEncode(value));

            return "?" + string.Join("&", array);
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

        private static string GetSortOrderQueryValue(SortOrder order)
        {
            switch (order)
            {
                case SortOrder.Ascending:
                    return "asc";
                case SortOrder.Descending:
                    return "desc";
                default:
                    throw new NotSupportedException($"Order {order} not supported");
            }
        }
    }
}
