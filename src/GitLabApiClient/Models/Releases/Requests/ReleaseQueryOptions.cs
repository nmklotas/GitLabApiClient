namespace GitLabApiClient.Models.Releases.Requests
{
    public sealed class ReleaseQueryOptions
    {
        internal ReleaseQueryOptions()
        {
        }

        /// <summary>
        /// Specifies releases order. Default is Released At time.
        /// </summary>
        public ReleasesOrder Order { get; set; }

        /// <summary>
        /// Specifies releases sort order. Default is descending.
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// Response includes HTML rendered Markdown of the release description. 
        /// </summary>
        public bool IncludeHtmlDescription { get; set; } = true;

    }
}
