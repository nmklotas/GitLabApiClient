using System;
using GitLabApiClient.Models.Iterations;
using GitLabApiClient.Models.Iterations.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class IterationsQueryBuilder : QueryBuilder<IterationsQueryOptions>
    {
        protected override void BuildCore(Query query, IterationsQueryOptions options)
        {
            if (options.State.HasValue)
                query.Add("state", GetStateQueryValue(options.State.Value));

            if (!string.IsNullOrEmpty(options.Title))
                query.Add("title", options.Title);

            if (!string.IsNullOrEmpty(options.Search))
                query.Add("search", options.Search);

            query.Add("include_ancestors", options.IncludeAncestors);
        }

        private static string GetStateQueryValue(IterationState state)
        {
            switch (state)
            {
                case IterationState.Opened:
                    return "opened";
                case IterationState.Upcoming:
                    return "upcoming";
                case IterationState.Current:
                    return "current";
                case IterationState.Closed:
                    return "closed";
                case IterationState.All:
                    return "all";
                default:
                    throw new NotSupportedException($"State {state} is not supported");
            }
        }
    }
}
