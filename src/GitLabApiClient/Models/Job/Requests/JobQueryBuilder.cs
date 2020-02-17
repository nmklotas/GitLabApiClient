using System;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;

namespace GitLabApiClient.Models.Job.Requests
{
    internal sealed class JobQueryBuilder : QueryBuilder<JobQueryOptions>
    {
        #region Overrides of QueryBuilder<PipelineQueryOptions>

        /// <inheritdoc />
        protected override void BuildCore(JobQueryOptions options)
        {
            if (options.Scope != JobScope.All)
            {
                Add("scope", options.Scope.ToLowerCaseString());
            }
        }

        #endregion
    }
}
