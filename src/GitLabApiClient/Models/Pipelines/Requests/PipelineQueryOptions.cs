namespace GitLabApiClient.Models.Pipelines.Requests
{
    public class PipelineQueryOptions
    {
        internal PipelineQueryOptions() { }

        /// <summary>
        ///     The scope of pipelines, one of: running, pending, finished, branches, tags
        /// </summary>
        public PipelineScope Scope { get; set; } = PipelineScope.All;

        /// <summary>
        ///     The status of pipelines, one of: running, pending, success, failed, canceled, skipped
        /// </summary>
        public PipelineStatus Status { get; set; } = PipelineStatus.All;

        /// <summary>
        ///     The ref of pipelines
        /// </summary>
        public string Ref { get; set; }

        /// <summary>
        ///     The sha or pipelines
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        ///     Returns pipelines with invalid configurations
        /// </summary>
        public bool? YamlErrors { get; set; }

        /// <summary>
        ///     Order pipelines by id, status, ref, or user_id (default: id)
        /// </summary>
        public PipelineOrder Order { get; set; } = PipelineOrder.Id;

        /// <summary>
        ///     Sort pipelines in asc or desc order (default: desc)
        /// </summary>
        public SortOrder SortOrder { get; set; } = SortOrder.Descending;
    }
}
