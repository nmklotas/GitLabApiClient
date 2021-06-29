using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Epics.Requests;
using GitLabApiClient.Models.Epics.Responses;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;
using GitLabApiClient.Models.Notes.Requests;
using GitLabApiClient.Models.Notes.Responses;

namespace GitLabApiClient
{
    public interface IEpicsClient
    {
        /// <summary>
        /// Retrieves epic by its id, path or <see cref="Epic"/>.
        /// </summary>
        /// <param name="epicId">The ID, path or <see cref="Epic"/> of the epic.</param>
        Task<Epic> GetAsync(GroupId groupId, int epicId);

        /// <summary>
        /// Get a list of visible epics for a group of the authenticated user.
        /// </summary>
        /// <param name="options">Query options.</param>
        Task<IList<Epic>> GetAsync(GroupId groupId, Action<EpicsGroupQueryOptions> options = null);

        /// <summary>
        /// Retrieves notes (comments) of an epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicId">Id of the epic.</param>
        /// <param name="options">Notes retrieval options.</param>
        /// <returns>Epic satisfying options.</returns>
        Task<IList<Note>> GetNotesAsync(GroupId groupId, int epicId, Action<NotesQueryOptions> options = null);

        /// <summary>
        /// Retrieves issues linked to an epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicId">Id of the epic.</param>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Epic satisfying options.</returns>
        Task<IList<Issue>> GetIssusAsync(GroupId groupId, int epicId, Action<IssuesQueryOptions> options = null);

        /// <summary>
        /// Creates a new epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Create epic request.</param>
        /// <returns>Newly created epic.</returns>
        Task<Epic> CreateAsync(GroupId groupId, CreateEpicRequest request);

        /// <summary>
        /// Creates a new note (comment) to a single epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicId">The ID of an epic.</param>
        /// <param name="request">Create epic note request.</param>
        /// <returns>The newly created epic note.</returns>
        Task<Note> CreateNoteAsync(GroupId groupId, int epicId, CreateNoteRequest request);

        /// <summary>
        /// Assigns an issue to an epic
        /// </summary>
        /// <returns>Assigned issue</returns>
        Task<Issue> AssignIssueAsync(GroupId groupId, int epicId, int issueId);

        /// <summary>
        /// Updates an epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicId">The ID of an epic.</param>
        /// <param name="request">Update epic request.</param>
        /// <returns>The updated epic.</returns>
        Task<Epic> UpdateAsync(GroupId groupId, int epicId, UpdateEpicRequest request);
    }
}
