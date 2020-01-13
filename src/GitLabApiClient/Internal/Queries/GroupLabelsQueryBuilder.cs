using GitLabApiClient.Models.Groups.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class GroupLabelsQueryBuilder : QueryBuilder<GroupLabelsQueryOptions>
    {
        #region Overrides of QueryBuilder<GroupLabelsQueryOptions>

        /// <inheritdoc />
        protected override void BuildCore(GroupLabelsQueryOptions options)
        {
            if (options.WithCounts)
            {
                Add("with_counts", true);
            }

            if (!options.IncludeAncestorGroups)
            {
                Add("include_ancestor_groups", false);
            }
        }

        #endregion
    }
}
