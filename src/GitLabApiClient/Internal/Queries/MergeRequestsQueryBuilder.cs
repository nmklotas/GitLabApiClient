using System;
using System.Linq;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.MergeRequests.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class MergeRequestsQueryBuilder : QueryBuilder<MergeRequestsQueryOptions>
    {
        protected override void BuildCore(Query query, MergeRequestsQueryOptions options)
        {
            string stateQueryValue = GetStateQueryValue(options.State);
            if (!stateQueryValue.IsNullOrEmpty())
                query.Add("state", stateQueryValue);

            if (options.Order != MergeRequestsOrder.CreatedAt)
                query.Add("order_by", GetIssuesOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                query.Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (!options.MilestoneTitle.IsNullOrEmpty())
                query.Add("milestone", options.MilestoneTitle);

            if (options.Simple)
                query.Add("view", "simple");

            if (options.Labels.Any())
                query.Add("labels", options.Labels);

            if (options.CreatedAfter.HasValue)
                query.Add("created_after", options.CreatedAfter.Value);

            if (options.CreatedBefore.HasValue)
                query.Add("created_before", options.CreatedBefore.Value);

            query.Add("scope", GetScopeQueryValue(options.Scope));

            if (options.AuthorId.HasValue)
                query.Add("author_id", options.AuthorId.Value);

            if (options.AssigneeId.HasValue)
                query.Add("assignee_id", options.AssigneeId.Value);
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
