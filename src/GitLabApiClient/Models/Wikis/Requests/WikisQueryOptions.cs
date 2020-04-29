using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Wikis.Requests
{
    public sealed class WikisQueryOptions
    {
        internal WikisQueryOptions() { }
        public bool WithContent { get; set; }
        public string Slug { get; set; }
    }
}
