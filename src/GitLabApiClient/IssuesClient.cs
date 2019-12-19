using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
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
        public async Task<Issue> GetAsync(ProjectId projectId, int issueId) =>
            await _httpFacade.Get<Issue>($"projects/{projectId}/issues/{issueId}");

        /// <summary>
        /// Retrieves issues from a project.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Issue>> GetAsync(ProjectId projectId, Action<ProjectIssuesQueryOptions> options = null)
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
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">Iid of the issue.</param>
        /// <param name="noteId">Id of the note.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<Note> GetNoteAsync(ProjectId projectId, int issueIid, int noteId) =>
            await _httpFacade.Get<Note>($"projects/{projectId}/issues/{issueIid}/notes/{noteId}");

        /// <summary>
        /// Retrieves notes (comments) of an issue.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">Iid of the issue.</param>
        /// <param name="options">IssueNotes retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Note>> GetNotesAsync(ProjectId projectId, int issueIid, Action<IssueNotesQueryOptions> options = null)
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
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create issue request.</param>
        public async Task<Issue> CreateAsync(ProjectId projectId, CreateIssueRequest request) =>
            await _httpFacade.Post<Issue>($"projects/{projectId}/issues", request);

        /// <summary>
        /// Creates a new note (comment) to a single project issue.
        /// </summary>
        /// <returns>The newly created issue note.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="request">Create issue note request.</param>
        public async Task<Note> CreateNoteAsync(ProjectId projectId, int issueIid, CreateIssueNoteRequest request) =>
            await _httpFacade.Post<Note>($"projects/{projectId}/issues/{issueIid}/notes", request);

        /// <summary>
        /// Updated existing issue.
        /// </summary>
        /// <returns>The updated issue.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="request">Update issue request.</param>
        public async Task<Issue> UpdateAsync(ProjectId projectId, int issueIid, UpdateIssueRequest request) =>
            await _httpFacade.Put<Issue>($"projects/{projectId}/issues/{issueIid}", request);

        /// <summary>
        /// Modify existing note (comment) of an issue.
        /// </summary>
        /// <returns>The updated issue note.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="noteId">The ID of a note.</param>
        /// <param name="request">Update issue note request.</param>
        public async Task<Note> UpdateNoteAsync(ProjectId projectId, int issueIid, int noteId, UpdateIssueNoteRequest request) =>
            await _httpFacade.Put<Note>($"projects/{projectId}/issues/{issueIid}/notes/{noteId}", request);

        /// <summary>
        /// Deletes an existing note (comment) of an issue.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="noteId">The ID of a note.</param>
        public async Task DeleteNoteAsync(ProjectId projectId, int issueIid, int noteId) =>
            await _httpFacade.Delete($"projects/{projectId}/issues/{issueIid}/notes/{noteId}");
    }
}
