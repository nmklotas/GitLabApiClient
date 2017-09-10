using System;
using System.Linq;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Merges;
using GitLabApiClient.Utilities;

namespace GitLabApiClient
{
    internal class MergeRequestsQueryBuilder : QueryBuilder<MergeRequestsQueryOptions>
    {
        protected override void BuildCore(MergeRequestsQueryOptions options)
        {
            string stateQueryValue = GetStateQueryValue(options.State);
            if (!stateQueryValue.IsNullOrEmpty())
                Add("state", stateQueryValue);

            if (options.Order != MergeRequestsOrder.CreatedAt)
                Add("order_by", GetIssuesOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (!options.MilestoneTitle.IsNullOrEmpty())
                Add("milestone", options.MilestoneTitle);

            if (options.Simple)
                Add("view", "simple");

            if (options.Labels.Any())
                Add("labels", options.Labels);

            if (options.CreatedAfter.HasValue)
                Add("created_after", options.CreatedAfter.Value);

            if (options.CreatedBefore.HasValue)
                Add("created_before", options.CreatedBefore.Value);

            Add("scope", GetScopeQueryValue(options.Scope));

            if (options.AuthorId.HasValue)
                Add("author_id", options.AuthorId.Value);

            if (options.AssigneeId.HasValue)
                Add("assignee_id", options.AssigneeId.Value);
        }

        private static string GetStateQueryValue(QueryMergeRequestState state)
        {
            switch (state)
            {
                case QueryMergeRequestState.Opened:
                    return "opened";
                case QueryMergeRequestState.Closed:
                    return "closed";
                case QueryMergeRequestState.Merged:
                    return "merged";
                case QueryMergeRequestState.All:
                    return "";
                default:
                    throw new NotSupportedException($"State {state} is not supported");
            }
        }

        private static string GetIssuesOrderQueryValue(MergeRequestsOrder order)
        {
            switch (order)
            {
                case MergeRequestsOrder.CreatedAt:
                    return "created_at";
                case MergeRequestsOrder.UpdatedAt:
                    return "updated_at";
                default:
                    throw new NotSupportedException($"Order {order} is not supported");
            }
        }
    }
}
