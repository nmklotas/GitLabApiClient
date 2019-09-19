using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Commits.Requests
{
    public sealed class CommitQueryOptions
    {
        public string ProjectId { get; set; }
        public string RefName { get; set; }
        internal CommitQueryOptions(string projectId = null) => ProjectId = projectId;
    }
}
