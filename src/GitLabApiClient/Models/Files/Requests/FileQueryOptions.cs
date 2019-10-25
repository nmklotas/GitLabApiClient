using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Files.Requests
{
    public sealed class FileQueryOptions
    {
        public string Branch { get; set; }
        internal FileQueryOptions(string branch = null, string path = null, bool recursive = true) => Branch = branch;
    }
}
