﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Commits.Requests;
using GitLabApiClient.Models.Commits.Responses;

namespace GitLabApiClient
{
    public sealed class CommitsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly CommitQueryBuilder _commitQueryBuilder;

        internal CommitsClient(GitLabHttpFacade httpFacade, CommitQueryBuilder commitQueryBuilder)
        {
            _httpFacade = httpFacade;
            _commitQueryBuilder = commitQueryBuilder;
        }

        public async Task<Commit> GetAsync(object projectId, string sha) =>
           await _httpFacade.Get<Commit>($"{CommitsBaseUrl(projectId)}/{sha}");

        public async Task<IList<Commit>> GetAsync(object projectId, Action<CommitQueryOptions> options = null)
        {
            var queryOptions = new CommitQueryOptions();
            options?.Invoke(queryOptions);

            string url = _commitQueryBuilder.Build(CommitsBaseUrl(projectId), queryOptions);
            return await _httpFacade.GetPagedList<Commit>(url);
        }

        private static string CommitsBaseUrl(object projectId)
        {
            return $"{projectId.ProjectBaseUrl()}/repository/commits";
        }
    }
}
