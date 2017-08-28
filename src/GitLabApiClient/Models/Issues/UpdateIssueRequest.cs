using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Issues
{
    public class UpdateIssueRequest
    {
        public UpdateIssueRequest(int projectId, int issueId)
        {
            ProjectId = projectId;
            IssueId = issueId;
        }

        [JsonProperty("id")]
        public int ProjectId { get; }

        [JsonProperty("issue_iid")]
        public int IssueId { get; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("confidential")]
        public bool Confidential { get; set; }

        [JsonProperty("assignee_ids")]
        public List<int> Assignees { get; set; } = new List<int>();

        [JsonProperty("milestone_id")]
        public int? MilestoneId { get; set; }

        [JsonProperty("labels")]
        public string Labels { get; set; }

        [JsonProperty("state_event")]
        public string State { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }
    }
}