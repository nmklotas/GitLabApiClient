using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;
using GitLabApiClient.Models.Notes.Requests;
using GitLabApiClient.Models.Notes.Responses;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to retrieve, modify, create issues.
    /// Every call to issues API must be authenticated.
    /// <exception cref="GitLabException">Thrown if request to GitLab API does not indicate success</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class IssuesClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly IssuesQueryBuilder _queryBuilder;
        private readonly ProjectIssuesQueryBuilder _projectIssuesQueryBuilder;
        private readonly ProjectIssueNotesQueryBuilder _projectIssueNotesQueryBuilder;

        internal IssuesClient(
            GitLabHttpFacade httpFacade,
            IssuesQueryBuilder queryBuilder,
            ProjectIssuesQueryBuilder projectIssuesQueryBuilder,
            ProjectIssueNotesQueryBuilder projectIssueNotesQueryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
            _projectIssuesQueryBuilder = projectIssuesQueryBuilder;
            _projectIssueNotesQueryBuilder = projectIssueNotesQueryBuilder;
        }

        /// <summary>
        /// Retrieves project issue.
        /// </summary>
        public async Task<Issue> GetAsync(object projectId, int issueId) =>
            await _httpFacade.Get<Issue>($"{IssuesBaseUrl(projectId)}/{issueId}");

        /// <summary>
        /// Retrieves issues from a project.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Issue>> GetAsync(object projectId, Action<ProjectIssuesQueryOptions> options = null)
        {
            var queryOptions = new ProjectIssuesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _projectIssuesQueryBuilder.Build(IssuesBaseUrl(projectId), queryOptions);
            return await _httpFacade.GetPagedList<Issue>(url);
        }

        /// <summary>
        /// Retrieves issues from all projects.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Issue>> GetAsync(Action<IssuesQueryOptions> options = null)
        {
            var queryOptions = new IssuesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build("issues", queryOptions);
            return await _httpFacade.GetPagedList<Issue>(url);
        }

        /// <summary>
        /// Retrieves project issue note.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">Iid of the issue.</param>
        /// <param name="noteId">Id of the note.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<Note> GetNoteAsync(object projectId, int issueIid, int noteId) =>
            await _httpFacade.Get<Note>($"{IssuesBaseUrl(projectId)}/{issueIid}/notes/{noteId}");

        /// <summary>
        /// Retrieves notes (comments) of an issue.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">Iid of the issue.</param>
        /// <param name="options">IssueNotes retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Note>> GetNotesAsync(object projectId, int issueIid, Action<IssueNotesQueryOptions> options = null)
        {
            var queryOptions = new IssueNotesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _projectIssueNotesQueryBuilder.Build($"{IssuesBaseUrl(projectId)}/{issueIid}/notes", queryOptions);
            return await _httpFacade.GetPagedList<Note>(url);
        }

        /// <summary>
        /// Creates new issue.
        /// </summary>
        /// <returns>The newly created issue.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create issue request.</param>
        public async Task<Issue> CreateAsync(object projectId, CreateIssueRequest request) =>
            await _httpFacade.Post<Issue>(IssuesBaseUrl(projectId), request);

        /// <summary>
        /// Creates a new note (comment) to a single project issue.
        /// </summary>
        /// <returns>The newly created issue note.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="request">Create issue note request.</param>
        public async Task<Note> CreateNoteAsync(object projectId, int issueIid, CreateIssueNoteRequest request) =>
            await _httpFacade.Post<Note>($"{IssuesBaseUrl(projectId)}/{issueIid}/notes", request);

        /// <summary>
        /// Updated existing issue.
        /// </summary>
        /// <returns>The updated issue.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="request">Update issue request.</param>
        public async Task<Issue> UpdateAsync(object projectId, int issueIid, UpdateIssueRequest request) =>
            await _httpFacade.Put<Issue>($"{IssuesBaseUrl(projectId)}/{issueIid}", request);

        /// <summary>
        /// Modify existing note (comment) of an issue.
        /// </summary>
        /// <returns>The updated issue note.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="noteId">The ID of a note.</param>
        /// <param name="request">Update issue note request.</param>
        public async Task<Issue> UpdateNoteAsync(object projectId, int issueIid, int noteId, UpdateIssueNoteRequest request) =>
            await _httpFacade.Put<Issue>($"{IssuesBaseUrl(projectId)}/{issueIid}/notes/{noteId}", request);

        /// <summary>
        /// Deletes an existing note (comment) of an issue.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="noteId">The ID of a note.</param>
        public async Task DeleteNoteAsync(object projectId, int issueIid, int noteId) =>
            await _httpFacade.Delete($"{IssuesBaseUrl(projectId)}/{issueIid}/notes/{noteId}");

        public static string IssuesBaseUrl(object projectId) =>
            $"{projectId.ProjectBaseUrl()}/issues";
    }
}
