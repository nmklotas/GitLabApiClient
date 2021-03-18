using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Pipelines.Responses
{
    public class TestReport
    {
        [JsonProperty("total_time")]
        public float TotalTime { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("success_count")]
        public int SuccessCount { get; set; }

        [JsonProperty("failed_count")]
        public int FailedCount { get; set; }

        [JsonProperty("skipped_count")]
        public int SkippedCount { get; set; }

        [JsonProperty("error_count")]
        public int ErrorCount { get; set; }

        [JsonProperty("test_suites")]
        public IList<TestSuite> TestSuites { get; set; }
    }
}
