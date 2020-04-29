using System;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class MilestonesQueryBuilder : QueryBuilder<MilestonesQueryOptions>
    {
        protected override void BuildCore(Query query, MilestonesQueryOptions options)
        {
            if (options.MilestoneIds.Count > 0)
                query.Add(options.MilestoneIds);

            string stateQueryValue = GetStateQueryValue(options.State);
            if (!string.IsNullOrEmpty(stateQueryValue))
                query.Add("state", stateQueryValue);

            if (!string.IsNullOrEmpty(options.Search))
                query.Add("search", options.Search);
        }

        private static string GetStateQueryValue(MilestoneState state)
        {
            switch (state)
            {
                case MilestoneState.Active:
                    return "active";
                case MilestoneState.Closed:
                    return "closed";
                case MilestoneState.All:
                    return "";
                default:
                    throw new NotSupportedException($"State {state} is not supported");
            }
        }
    }
}
