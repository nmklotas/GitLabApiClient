using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Webhooks.Responses
{
    public sealed class Webhook
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("push_events")]
        public bool PushEvents { get; set; }

        [JsonProperty("push_events_branch_filter")]
        public string PushEventsBranchFilter { get; set; }

        [JsonProperty("issues_events")]
        public bool IssuesEvents { get; set; }

        [JsonProperty("confidential_issues_events")]
        public bool ConfidentialIssuesEvents { get; set; }

        [JsonProperty("merge_requests_events")]
        public bool MergeRequestsEvents { get; set; }

        [JsonProperty("tag_push_events")]
        public bool TagPushEvents { get; set; }

        [JsonProperty("note_events")]
        public bool NoteEvents { get; set; }

        [JsonProperty("job_events")]
        public bool JobEvents { get; set; }

        [JsonProperty("pipeline_events")]
        public bool PipelineEvents { get; set; }

        [JsonProperty("wiki_page_events")]
        public bool WikiPageEvents { get; set; }

        [JsonProperty("enable_ssl_verification")]
        public bool EnableSslVerification { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

    }
}
