using Newtonsoft.Json;

namespace GitLabApiClient.Models.Milestones.Responses
{
    public sealed class Milestone : ModifiableObject
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("project_id")]
        public int? ProjectId { get; set; }

        [JsonProperty("group_id")]
        public int? GroupId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }

        [JsonProperty("state")]
        public MilestoneState State { get; set; }
    }
}
