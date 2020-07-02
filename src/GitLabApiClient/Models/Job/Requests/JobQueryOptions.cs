namespace GitLabApiClient.Models.Job.Requests
{
    public class JobQueryOptions
    {
        internal JobQueryOptions() { }

        /// <summary>
        ///     The scope of jobs, one of: running, pending, finished, branches, tags
        /// </summary>
        public JobScope Scope { get; set; } = JobScope.All;
    }
}
