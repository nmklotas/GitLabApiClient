using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Files.Requests
{
    public sealed class FileQueryOptions
    {
        public string Reference { get; set; }
        internal FileQueryOptions(string reference = null) => Reference = reference;
    }
}
