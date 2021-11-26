using System;
using System.Collections.Generic;

namespace GitLabApiClient.Models.Job.Requests
{
    public class JobQueryOptions
    {
        internal JobQueryOptions() { }

        /// <summary>
        /// The scope of jobs, one of: created, pending, running, failed, success, canceled, skipped, or manual.
        /// </summary>
        [Obsolete("Use Scopes instead.")]
        public JobScope Scope { get; set; } = JobScope.All;

        /// <summary>
        /// Scope of jobs to show.
        /// Either one of or an array of the following: created, pending, running, failed, success, canceled, skipped, or manual.
        /// All jobs are returned if scope is not provided.
        /// </summary>
        public IList<JobScope> Scopes { get; set; }
    }
}
