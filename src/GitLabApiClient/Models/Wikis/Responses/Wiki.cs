using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Wikis.Responses
{
    public sealed class Wiki
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("format")]
        public string Format { get; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
