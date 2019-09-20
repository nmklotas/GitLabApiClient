using GitLabApiClient.Models.MergeRequests.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class ProjectMergeRequestsQueryBuilder : MergeRequestsQueryBuilder
    {
        protected override void BuildCore(MergeRequestsQueryOptions options)
        {
            if (!(options is ProjectMergeRequestsQueryOptions projectOptions))
            {
                base.BuildCore(options);
                return;
            }

            Add(projectOptions.MergeRequestsIds);
            base.BuildCore(options);
        }
    }
}
