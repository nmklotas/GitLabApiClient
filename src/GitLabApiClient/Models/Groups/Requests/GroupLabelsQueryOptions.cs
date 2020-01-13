namespace GitLabApiClient.Models.Groups.Requests
{
    /// <summary>
    ///     Options for group label list
    /// </summary>
    public sealed class GroupLabelsQueryOptions
    {
        internal GroupLabelsQueryOptions()
        {
        }

        /// <summary>
        ///     Whether or not to include issue and merge request counts. Defaults to `false`. (Introduced in GitLab 12.2)
        /// </summary>
        public bool WithCounts { get; set; } = false;

        /// <summary>
        ///     Include ancestor groups. Defaults to `true`.
        /// </summary>
        public bool IncludeAncestorGroups { get; set; } = true;
    }
}
