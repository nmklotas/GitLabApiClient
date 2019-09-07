using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Branches.Requests
{
    public sealed class BranchQueryOptions
    {
        public string ProjectId { get; set; }
        public string BranchName { get; set; }

        internal BranchQueryOptions(string projectId = null) => ProjectId = projectId;
    }
}
