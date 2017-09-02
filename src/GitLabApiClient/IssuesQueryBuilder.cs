using System;
using System.Collections.Specialized;
using System.Linq;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Issues;
using GitLabApiClient.Utilities;

namespace GitLabApiClient
{
    internal class IssuesQueryBuilder : QueryBuilder<IssuesQueryOptions>
    {
        protected override void BuildCore(NameValueCollection nameValues, IssuesQueryOptions options)
        {
            string stateQueryValue = GetStateQueryValue(options.State);
            if (!stateQueryValue.IsNullOrEmpty())
                nameValues.Add("state", stateQueryValue);

            if (options.Labels.Any())
                nameValues.Add("labels", ToQueryString(options.Labels));

            if (!options.MilestoneTitle.IsNullOrEmpty())
                nameValues.Add("milestone", options.MilestoneTitle);

            nameValues.Add("scope", GetScopeQueryValue(options.Scope));

            if (options.AuthorId.HasValue)
                nameValues.Add("author_id", options.AuthorId.Value.ToString());

            if (options.AssigneeId.HasValue)
                nameValues.Add("assignee_id", options.AssigneeId.Value.ToString());

            foreach (int iid in options.IssueIds)
                nameValues.Add("iids[]", iid.ToString());

            if (options.Order != IssuesOrder.CreatedAt)
                nameValues.Add("order_by", GetIssuesOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                nameValues.Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (!options.Filter.IsNullOrEmpty())
                nameValues.Add("search", options.Filter);
        }

        private static string GetStateQueryValue(IssueState state)
        {
            switch (state)
            {
                case IssueState.Opened:
                    return "opened";
                case IssueState.Closed:
                    return "closed";
                case IssueState.All:
                    return "";
                default:
                    throw new NotSupportedException($"State {state} is not supported");
            }
        }

        private static string GetIssuesOrderQueryValue(IssuesOrder order)
        {
            switch (order)
            {
                case IssuesOrder.CreatedAt:
                    return "created_at";
                case IssuesOrder.UpdatedAt:
                    return "updated_at";
                default:
                    throw new NotSupportedException($"Order {order} is not supported");
            }
        }
    }
}