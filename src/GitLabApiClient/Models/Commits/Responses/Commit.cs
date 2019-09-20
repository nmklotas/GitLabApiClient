using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Models.Releases.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Commits.Responses
{
    public sealed class Commit
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("short_id")]
        public string ShortId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }
        [JsonProperty("author_email")]
        public string AuthorEmail { get; set; }
        [JsonProperty("authored_date")]
        public DateTime AuthoredDate { get; set; }
        [JsonProperty("committer_name")]
        public string CommitterName { get; set; }
        [JsonProperty("committer_email")]
        public string CommitterEmail { get; set; }
        [JsonProperty("committed_date")]
        public DateTime CommittedDate { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("parent_ids")]
        public List<string> ParentIds { get; } = new List<string>();

    }
}
