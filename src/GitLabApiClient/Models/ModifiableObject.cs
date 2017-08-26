using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models
{
    public class ModifiableObject
    {
        [JsonProperty("iid")]
        public long Iid { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
