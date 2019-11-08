using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Models.Files.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class FileQueryBuilder : QueryBuilder<FileQueryOptions>
    {
        protected override void BuildCore(FileQueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.Reference))
                Add("ref", options.Reference);

        }
    }
}
