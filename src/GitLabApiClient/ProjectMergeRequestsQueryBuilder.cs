using System.Collections.Specialized;
using GitLabApiClient.Models.Merges;

namespace GitLabApiClient
{
    internal sealed class ProjectMergeRequestsQueryBuilder : MergeRequestsQueryBuilder
    {
        protected override void BuildCore(NameValueCollection nameValues, MergeRequestsQueryOptions options)
        {
            if (!(options is ProjectMergeRequestsQueryOptions projectOptions))
            {
                base.BuildCore(nameValues, options);
                return;
            }

            nameValues.Add("id", projectOptions.ProjectId.ToString());

            foreach (int iid in projectOptions.MergeRequestsIds)
                nameValues.Add("iids[]", iid.ToString());

            base.BuildCore(nameValues, options);
        }
    }
}
