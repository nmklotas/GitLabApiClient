using System;
using GitLabApiClient.Models.Environments.Requests;

namespace GitLabApiClient.Internal.Queries
{
    class EnvironmentsQueryBuilder : QueryBuilder<EnvironmentsQueryOptions>
    {
        protected override void BuildCore(Query query, EnvironmentsQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.Name) && !string.IsNullOrEmpty(options.Search))
                throw new InvalidOperationException("Environment queries for 'name' and 'search' are mutually exclusive.");

            if (!string.IsNullOrEmpty(options.Name))
                query.Add("name", options.Name);

            if (!string.IsNullOrEmpty(options.Search))
                query.Add("search", options.Search);

            if (options.States != null)
                query.Add("states", options.States.ToString().ToLowerInvariant());
        }
    }
}
