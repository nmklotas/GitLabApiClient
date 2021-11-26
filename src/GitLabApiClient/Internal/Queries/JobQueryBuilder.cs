using System.Collections.Generic;
using System.Linq;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Job.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class JobQueryBuilder : QueryBuilder<JobQueryOptions>
    {
        #region Overrides of QueryBuilder<JobQueryOptions>

        /// <inheritdoc />
        protected override void BuildCore(Query query, JobQueryOptions options)
        {
            var scopes = options.Scopes;
#pragma warning disable 618
            if (options.Scope != JobScope.All)
            {
                scopes = scopes ?? new List<JobScope>();
                scopes.Add(options.Scope);
            }
#pragma warning restore 618
            if (options.Scopes?.Count > 0)
            {
                query.Add("scope", options.Scopes.Select(AsString).ToList());
            }
        }

        #endregion

        private static string AsString(JobScope scope)
        {
            return scope.ToLowerCaseString();
        }
    }
}
