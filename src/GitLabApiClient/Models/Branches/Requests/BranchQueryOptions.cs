using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Branches.Requests
{
    public sealed class BranchQueryOptions
    {
        public string Search { get; set; }

        internal BranchQueryOptions()
        {
        }
    }
}
