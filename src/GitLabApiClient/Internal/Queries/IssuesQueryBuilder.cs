using System;
using System.Linq;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;

namespace GitLabApiClient.Internal.Queries
{
    internal class IssuesQueryBuilder : QueryBuilder<IssuesQueryOptions>
    {
        protected override void BuildCore(Query query, IssuesQueryOptions options)
        {
            string stateQueryValue = GetStateQueryValue(options.State);
            if (!stateQueryValue.IsNullOrEmpty())
                query.Add("state", stateQueryValue);

            if (options.Labels.Any())
                query.Add("labels", options.Labels);

            if (!options.MilestoneTitle.IsNullOrEmpty())
                query.Add("milestone", options.MilestoneTitle);

            query.Add("scope", GetScopeQueryValue(options.Scope));

            if (options.AuthorId.HasValue)
                query.Add("author_id", options.AuthorId.Value);
            else if (!string.IsNullOrWhiteSpace(options.AuthorUsername))
                query.Add("author_username", options.AuthorUsername);

            if (options.AssigneeId.HasValue)
                query.Add("assignee_id", options.AssigneeId.Value);
            else if (options.AssigneeUsername.Any())
                query.Add("assignee_username", options.AssigneeUsername);

            query.Add(options.IssueIds);

            if (options.Order != IssuesOrder.CreatedAt)
                query.Add("order_by", GetIssuesOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                query.Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (!options.Filter.IsNullOrEmpty())
                query.Add("search", options.Filter);

            if (options.IsConfidential)
                query.Add("confidential", true);

            if (options.CreatedBefore.HasValue)
                query.Add("created_before", options.CreatedBefore.Value);

            if (options.CreatedAfter.HasValue)
                query.Add("created_after", options.CreatedAfter.Value);

            if (options.UpdatedBefore.HasValue)
                query.Add("updated_before", options.UpdatedBefore.Value);

            if (options.UpdatedAfter.HasValue)
                query.Add("updated_after", options.UpdatedAfter.Value);

            if (!string.IsNullOrEmpty(options.IterationId))
                query.Add("iteration_id", options.IterationId);

            if (!string.IsNullOrEmpty(options.IterationTitle))
                query.Add("iteration_title", options.IterationTitle);
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
