using System;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class ProjectMilestonesQueryBuilder : QueryBuilder<ProjectMilestonesQueryOptions>
    {
        protected override void BuildCore(ProjectMilestonesQueryOptions options)
        {
            if(options.MilestoneIds.Count > 0)
                Add(options.MilestoneIds);

            string stateQueryValue = GetStateQueryValue(options.State);
            if (!string.IsNullOrEmpty(stateQueryValue))
                Add("state", stateQueryValue);

            if (!string.IsNullOrEmpty(options.Search))
                Add("search", options.Search);
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
