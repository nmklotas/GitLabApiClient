using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Trees.Requests;
using GitLabApiClient.Models.Trees.Responses;

namespace GitLabApiClient
{
    public interface ITreesClient
    {
        Task<IList<Tree>> GetAsync(ProjectId projectId, Action<TreeQueryOptions> options = null);
    }
}
