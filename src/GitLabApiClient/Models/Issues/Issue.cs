using System.Collections.Generic;
using GitLabApiClient.Models.Users;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Issues
{
    public sealed class Issue : ModifiableObject
    {
        [JsonProperty("confidential")]
        public bool Confidential { get; set; }

        [JsonProperty("assignees")]
        public List<Assignee> Assignees { get; } = new List<Assignee>();

        [JsonProperty("assignee")]
        public Assignee Assignee { get; set; }

        [JsonProperty("author")]
        public Assignee Author { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("labels")]
        public List<string> Labels { get; } = new List<string>();

        [JsonProperty("milestone")]
        public Milestone Milestone { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("user_notes_count")]
        public int UserNotesCount { get; set; }

        [JsonProperty("state")]
        public IssueState State { get; set; }

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }
    }
}
