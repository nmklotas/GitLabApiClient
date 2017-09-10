using System;
using System.Linq;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Issues;
using GitLabApiClient.Utilities;

namespace GitLabApiClient
{
    internal class IssuesQueryBuilder : QueryBuilder<IssuesQueryOptions>
    {
        protected override void BuildCore(IssuesQueryOptions options)
        {
            string stateQueryValue = GetStateQueryValue(options.State);
            if (!stateQueryValue.IsNullOrEmpty())
                Add("state", stateQueryValue);

            if (options.Labels.Any())
                Add("labels", options.Labels);

            if (!options.MilestoneTitle.IsNullOrEmpty())
                Add("milestone", options.MilestoneTitle);

            Add("scope", GetScopeQueryValue(options.Scope));

            if (options.AuthorId.HasValue)
                Add("author_id", options.AuthorId.Value);

            if (options.AssigneeId.HasValue)
                Add("assignee_id", options.AssigneeId.Value);

            Add(options.IssueIds);

            if (options.Order != IssuesOrder.CreatedAt)
                Add("order_by", GetIssuesOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (!options.Filter.IsNullOrEmpty())
                Add("search", options.Filter);
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