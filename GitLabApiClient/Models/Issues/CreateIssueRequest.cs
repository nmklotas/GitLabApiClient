using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Issues
{
    public class CreateIssueRequest
    {
        [JsonProperty("id")]
        public long ProjectId { get; set; }

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

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("merge_request_to_resolve_discussions_of")]
        public int? MergeRequestIdToResolveDiscussions { get; set; }

        [JsonProperty("discussion_to_resolve")]
        public int? DiscussionToResolveId { get; set; }
    }
}
