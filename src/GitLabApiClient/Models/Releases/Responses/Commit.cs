using Newtonsoft.Json;

namespace GitLabApiClient.Models.Releases.Responses
{
    public sealed class Commit
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("short_id")]
        public string ShortId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("parent_ids")]
        public string[] ParentIds { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_email")]
        public string AuthorEmail { get; set; }

        [JsonProperty("authored_date")]
        public string AuthoredDate { get; set; }

        [JsonProperty("committer_name")]
        public string CommitterName { get; set; }

        [JsonProperty("committer_email")]
        public string CommitterEmail { get; set; }

        [JsonProperty("committed_date")]
        public string CommittedDate { get; set; }
    }
}
