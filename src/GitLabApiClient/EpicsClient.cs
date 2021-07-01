using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Epics.Requests;
using GitLabApiClient.Models.Epics.Responses;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;
using GitLabApiClient.Models.Notes.Requests;
using GitLabApiClient.Models.Notes.Responses;

namespace GitLabApiClient
{
    public sealed class EpicsClient : IEpicsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly EpicsGroupQueryBuilder _queryBuilder;
        private readonly NotesQueryBuilder _notesQueryBuilder;
        private readonly IssuesQueryBuilder _issuesQueryBuilder;

        internal EpicsClient(GitLabHttpFacade httpFacade, EpicsGroupQueryBuilder queryBuilder, NotesQueryBuilder notesQueryBuilder, IssuesQueryBuilder issuesQueryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
            _notesQueryBuilder = notesQueryBuilder;
            _issuesQueryBuilder = issuesQueryBuilder;
        }

        public async Task<Epic> CreateAsync(GroupId groupId, CreateEpicRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Epic>($"groups/{groupId}/epics", request);
        }

        /// <summary>
        /// Retrieves epic by its id from a group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicId">The ID of the epic.</param>
        /// <returns></returns>
        public async Task<Epic> GetAsync(GroupId groupId, int epicId) =>
            await _httpFacade.Get<Epic>($"groups/{groupId}/epics/{epicId}");

        /// <summary>
        /// Retrieves notes (comments) of an epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicId">Id of the epic.</param>
        /// <param name="options">Notes retrieval options.</param>
        /// <returns>Epic satisfying options.</returns>
        public async Task<IList<Note>> GetNotesAsync(GroupId groupId, int epicId, Action<NotesQueryOptions> options = null)
        {
            var queryOptions = new NotesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _notesQueryBuilder.Build($"groups/{groupId}/epics/{epicId}/notes", queryOptions);
            return await _httpFacade.GetPagedList<Note>(url);
        }
        /// <summary>
        /// Retrieves issues linked to an epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicId">Id of the epic.</param>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Epic satisfying options.</returns>
        public async Task<IList<Issue>> GetIssusAsync(GroupId groupId, int epicId, Action<IssuesQueryOptions> options = null)
        {
            var queryOptions = new IssuesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _issuesQueryBuilder.Build($"groups/{groupId}/epics/{epicId}/issues", queryOptions);
            return await _httpFacade.GetPagedList<Issue>(url);
        }

        /// <summary>
        /// Creates a new note (comment) to a single epic.
        /// </summary>
        /// <returns>The newly created epic note.</returns>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicIid">The IID of an epic.</param>
        /// <param name="request">Create epic note request.</param>
        public async Task<Note> CreateNoteAsync(GroupId groupId, int epicId, CreateNoteRequest request) =>
                    await _httpFacade.Post<Note>($"groups/{groupId}/epics/{epicId}/notes", request);


        /// <summary>
        /// Get a list of visible epics for a group of the authenticated user.
        /// </summary>
        /// <param name="options">Query options.</param>
        public async Task<IList<Epic>> GetAsync(GroupId groupId, Action<EpicsGroupQueryOptions> options = null)
        {
            var queryOptions = new EpicsGroupQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build($"groups/{groupId}/epics", queryOptions);
            return await _httpFacade.GetPagedList<Epic>(url);
        }

        /// <summary>
        /// Assigns an issue to an epic
        /// </summary>
        /// <returns>Assigned issue</returns>
        public async Task<Issue> AssignIssueAsync(GroupId groupId, int epicId, int issueId) =>
            await _httpFacade.Post<Issue>($"groups/{groupId}/epics/{epicId}/issues/{issueId}");

        /// <summary>
        /// Updates an epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicId">The ID of an epic.</param>
        /// <param name="request">Update epic request.</param>
        /// <returns>The updated epic.</returns>
        public async Task<Epic> UpdateAsync(GroupId groupId, int epicId, UpdateEpicRequest request) =>
            await _httpFacade.Put<Epic>($"groups/{groupId}/epics/{epicId}", request);
    }
}
