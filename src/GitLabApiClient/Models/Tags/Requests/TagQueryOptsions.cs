using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Tags.Requests
{
    public sealed class TagQueryOptsions
    {
        public string ProjectId { get; set; }
        public string TagName { get; set; }

        internal TagQueryOptsions(string projectId = null) => ProjectId = projectId;
    }
}
