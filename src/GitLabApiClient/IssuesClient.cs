using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;
using GitLabApiClient.Models.Notes.Requests;
using GitLabApiClient.Models.Notes.Responses;

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
        public async Task<Issue> GetAsync(int projectId, int issueId) =>
            await _httpFacade.Get<Issue>($"projects/{projectId}/issues/{issueId}");

        /// <summary>
        /// Retrieves issues from a project.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Issue>> GetAsync(string projectId, Action<ProjectIssuesQueryOptions> options = null)
        {
            var queryOptions = new ProjectIssuesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _projectIssuesQueryBuilder.Build($"projects/{projectId}/issues", queryOptions);
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
        /// <param name="projectId">Id of the project.</param>
        /// <param name="issueIid">Iid of the issue.</param>
        /// <param name="noteId">Id of the note.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<Note> GetNoteAsync(int projectId, int issueIid, int noteId) =>
            await _httpFacade.Get<Note>($"projects/{projectId}/issues/{issueIid}/notes/{noteId}");

        /// <summary>
        /// Retrieves notes (comments) of an issue.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="issueIid">Iid of the issue.</param>
        /// <param name="options">IssueNotes retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Note>> GetNotesAsync(int projectId, int issueIid, Action<IssueNotesQueryOptions> options = null)
        {
            var queryOptions = new IssueNotesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _projectIssueNotesQueryBuilder.Build($"projects/{projectId}/issues/{issueIid}/notes", queryOptions);
            return await _httpFacade.GetPagedList<Note>(url);
        }

        /// <summary>
        /// Creates new issue.
        /// </summary>
        /// <returns>The newly created issue.</returns>
        public async Task<Issue> CreateAsync(CreateIssueRequest request) =>
            await _httpFacade.Post<Issue>($"projects/{request.ProjectId}/issues", request);

        /// <summary>
        /// Creates a new note (comment) to a single project issue.
        /// </summary>
        /// <returns>The newly created issue note.</returns>
        public async Task<Note> CreateNoteAsync(CreateIssueNoteRequest request) =>
            await _httpFacade.Post<Note>($"projects/{request.ProjectId}/issues/{request.IssueIid}/notes", request);

        /// <summary>
        /// Updated existing issue.
        /// </summary>
        /// <returns>The updated issue.</returns>
        public async Task<Issue> UpdateAsync(UpdateIssueRequest request) =>
            await _httpFacade.Put<Issue>($"projects/{request.ProjectId}/issues/{request.IssueId}", request);

        /// <summary>
        /// Modify existing note (comment) of an issue.
        /// </summary>
        /// <returns>The updated issue note.</returns>
        public async Task<Issue> UpdateNoteAsync(UpdateIssueNoteRequest request) =>
            await _httpFacade.Put<Issue>($"projects/{request.ProjectId}/issues/{request.IssueIid}/notes/{request.NoteId}", request);

        /// <summary>
        /// Deletes an existing note (comment) of an issue.
        /// </summary>
        /// <param name="projectId">The ID or URL-encoded path of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="noteId">The ID of a note.</param>
        public async Task DeleteNoteAsync(int projectId, int issueIid, int noteId) =>
            await _httpFacade.Delete($"projects/{projectId}/issues/{issueIid}/notes/{noteId}");
    }
}
