using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Tags.Requests
{
    public sealed class TagQueryOptions
    {
        public string ProjectId { get; set; }
        public string TagName { get; set; }

        internal TagQueryOptions(string projectId = null) => ProjectId = projectId;
    }
}
