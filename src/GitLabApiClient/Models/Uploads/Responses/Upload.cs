using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Uploads.Responses
{
    public sealed class Upload
    {
        [JsonConstructor]
        public Upload(string alt, string url, string markdown)
        {
            Alt = alt;
            Url = url;
            Markdown = markdown;
        }

        public string Alt { get; }
        public string Url { get; }
        public string Markdown { get; }
    }
}
