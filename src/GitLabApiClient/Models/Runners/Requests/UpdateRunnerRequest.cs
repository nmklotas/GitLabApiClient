using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Runners.Requests
{
    /// <summary>
    /// Modifies an existing user.
    /// </summary>
    public sealed class UpdateRunnerRequest
    {
        /// <summary>
        /// The description of a runner.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The state of a runner; can be set to true or false.
        /// </summary>
        [JsonProperty("active")]
        public bool? Active { get; set; }

        /// <summary>
        /// The list of tags for a runner; put array of tags, that should be finally assigned to a runner.
        /// </summary>
        [JsonProperty("tag_list")]
        public List<string> TagList { get; set; }

        /// <summary>
        /// Flag indicating the runner can execute untagged jobs.
        /// </summary>
        [JsonProperty("run_untagged")]
        public bool? RunUntagged { get; set; }

        /// <summary>
        ///	Flag indicating the runner is locked.
        /// </summary>
        [JsonProperty("locked")]
        public bool? Locked { get; set; }

        /// <summary>
        /// The access_level of the runner; not_protected or ref_protected.
        /// </summary>
        [JsonProperty("access_level")]
        public string AccessLevel { get; set; }

        /// <summary>
        /// Maximum timeout set when this Runner will handle the job.
        /// </summary>
        [JsonProperty("maximum_timeout")]
        public int? MaximumTimeout { get; set; }
    }
}
