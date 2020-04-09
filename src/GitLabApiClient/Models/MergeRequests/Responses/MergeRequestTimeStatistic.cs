using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequests.Responses
{
    public class MergeRequestTimeStatistic
    {
        [JsonProperty("time_estimate")]
        public int TimeEstimate { get; set; }

        [JsonProperty("total_time_spent")]
        public int TotalTimeSpent { get; set; }

        [JsonProperty("human_time_estimate")]
        public string HumanTimeEstimate { get; set; }

        [JsonProperty("human_total_time_spent")]
        public string HumanTotalTimeSpent { get; set; }
    }
}
