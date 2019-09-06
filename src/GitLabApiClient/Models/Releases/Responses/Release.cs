using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Models.Milestones.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Releases.Responses
{
    public sealed class Release
    {
        [JsonProperty("tag_name")]
        public string TagName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description_html")]
        public string DescriptionHtml { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("released_at")]
        public string ReleasedAt { get; set; }

        [JsonProperty("author")]
        public Member Author { get; set; }

        [JsonProperty("commit")]
        public Commit Commit { get; set; }

        [JsonProperty("milestone")]
        public Milestone Milestone { get; set; }

        [JsonProperty("assets")]
        public Asset Assets { get; set; }
    }
}
