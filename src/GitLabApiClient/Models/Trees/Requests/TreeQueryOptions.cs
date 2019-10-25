using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Trees.Requests
{
    public sealed class TreeQueryOptions
    {
        public string Branch { get; set; }
        public string Path { get; set; }
        public bool Recursive { get; set; }

        internal TreeQueryOptions(string branch = null, string path = null, bool recursive = true)
        {
            Branch = branch;
            Path = path;
            Recursive = recursive;
        }
    }
}
