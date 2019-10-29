using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models
{
    public class ModifiableObject
    {
        internal ModifiableObject() { }

        [JsonProperty("iid")]
        public int Iid { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
