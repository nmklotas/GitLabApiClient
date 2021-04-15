using System;

namespace GitLabApiClient.Models.Commits.Requests
{
    public sealed class CommitQueryOptions
    {
        public string RefName { get; set; }

        public DateTime? Since { get; set; }

        public DateTime? Until { get; set; }

        public string Path { get; set; }

        public bool? All { get; set; }

        public bool? WithStats { get; set; }

        public bool? FirstParent { get; set; }

        internal CommitQueryOptions()
        {
        }
    }
}
