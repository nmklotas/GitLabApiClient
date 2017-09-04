using System;
using System.Collections.Specialized;
using System.Linq;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Merges;
using GitLabApiClient.Utilities;

namespace GitLabApiClient
{
    internal class MergeRequestsQueryBuilder : QueryBuilder<MergeRequestsQueryOptions>
    {
        protected override void BuildCore(NameValueCollection nameValues, MergeRequestsQueryOptions options)
        {
            string stateQueryValue = GetStateQueryValue(options.State);
            if (!stateQueryValue.IsNullOrEmpty())
                nameValues.Add("state", stateQueryValue);

            if (options.Order != MergeRequestsOrder.CreatedAt)
                nameValues.Add("order_by", GetIssuesOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                nameValues.Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (!options.MilestoneTitle.IsNullOrEmpty())
                nameValues.Add("milestone", options.MilestoneTitle);

            if (options.Simple)
                nameValues.Add("view", "simple");

            if (options.Labels.Any())
                nameValues.Add("labels", ToQueryString(options.Labels));

            if (options.CreatedAfter.HasValue)
                nameValues.Add("created_after", GetDateValue(options.CreatedAfter.Value));

            if (options.CreatedBefore.HasValue)
                nameValues.Add("created_before", GetDateValue(options.CreatedBefore.Value));

            nameValues.Add("scope", GetScopeQueryValue(options.Scope));

            if (options.AuthorId.HasValue)
                nameValues.Add("author_id", options.AuthorId.Value.ToString());

            if (options.AssigneeId.HasValue)
                nameValues.Add("assignee_id", options.AssigneeId.Value.ToString());
        }

        private static string GetStateQueryValue(MergeRequestQueryState state)
        {
            switch (state)
            {
                case MergeRequestQueryState.Opened:
                    return "opened";
                case MergeRequestQueryState.Closed:
                    return "closed";
                case MergeRequestQueryState.Merged:
                    return "merged";
                case MergeRequestQueryState.All:
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
