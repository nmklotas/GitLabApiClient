using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient.Models.Releases.Requests
{
    public sealed class ReleaseQueryOptions
    {
        public string ProjectId { get; set; }
        public string TagName { get; set; }

        internal ReleaseQueryOptions(string projectId = null) => ProjectId = projectId;
    }
}
