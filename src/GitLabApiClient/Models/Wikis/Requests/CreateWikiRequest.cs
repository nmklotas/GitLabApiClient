using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Wikis.Requests
{
    public sealed class CreateWikiRequest
    {
        public CreateWikiRequest(string title)
        {
            Guard.NotEmpty(title, nameof(title));
            Title = title;
        }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("title")]
        public string Title { get; }
    }
}
