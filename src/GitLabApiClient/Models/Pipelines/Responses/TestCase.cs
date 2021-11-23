using Newtonsoft.Json;

namespace GitLabApiClient.Models.Pipelines.Responses {
    public class TestCase
    {
        [JsonProperty("status")]
        public TestCaseStatus Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("classname")]
        public string ClassName { get; set; }

        [JsonProperty("execution_time")]
        public float ExecutionTime { get; set; }

        [JsonProperty("system_output")]
        public string SystemOutput { get; set; }

        [JsonProperty("stack_trace")]
        public string StackTrace { get; set; }
    }
}
