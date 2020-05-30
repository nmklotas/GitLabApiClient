using GitLabApiClient.Models.MergeRequests.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class ProjectMergeRequestsQueryBuilder : MergeRequestsQueryBuilder
    {
        protected override void BuildCore(Query query, MergeRequestsQueryOptions options)
        {
            if (!(options is ProjectMergeRequestsQueryOptions projectOptions))
            {
                base.BuildCore(query, options);
                return;
            }

            query.Add(projectOptions.MergeRequestsIds);
            base.BuildCore(query, options);
        }
    }
}
