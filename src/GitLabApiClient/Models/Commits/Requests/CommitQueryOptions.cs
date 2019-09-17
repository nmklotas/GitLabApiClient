using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Tags.Requests
{
    public sealed class CommitQueryOptions
    {
        public string ProjectId { get; set; }
        public string Sha { get; set; }
        internal CommitQueryOptions(string projectId = null) => ProjectId = projectId;
    }
}
