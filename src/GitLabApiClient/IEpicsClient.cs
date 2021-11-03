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
        /// <param name="epicIid">The ID, path or <see cref="Epic"/> of the epic.</param>
        Task<Epic> GetAsync(GroupId groupId, int epicIid);

        /// <summary>
        /// Get a list of visible epics for a group of the authenticated user.
        /// </summary>
        /// <param name="options">Query options.</param>
        Task<IList<Epic>> GetAsync(GroupId groupId, Action<EpicsGroupQueryOptions> options = null);

        /// <summary>
        /// Retrieves notes (comments) of an epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicIid">Id of the epic.</param>
        /// <param name="options">Notes retrieval options.</param>
        /// <returns>Epic satisfying options.</returns>
        Task<IList<Note>> GetNotesAsync(GroupId groupId, int epicIid, Action<NotesQueryOptions> options = null);

        /// <summary>
        /// Retrieves issues linked to an epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicIid">Id of the epic.</param>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Epic satisfying options.</returns>
        Task<IList<Issue>> GetIssusAsync(GroupId groupId, int epicIid, Action<IssuesQueryOptions> options = null);

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
        /// <param name="epicIid">The Iid of an epic.</param>
        /// <param name="request">Create epic note request.</param>
        /// <returns>The newly created epic note.</returns>
        Task<Note> CreateNoteAsync(GroupId groupId, int epicIid, CreateNoteRequest request);

        /// <summary>
        /// Assigns an issue to an epic
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicIid">The Iid of an epic</param>
        /// <param name="issueId">The Id of the Issue</param>
        /// <returns>Assigned issue</returns>
        Task<Issue> AssignIssueAsync(GroupId groupId, int epicIid, int issueId);

        /// <summary>
        /// Updates an epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicIid">The Iid of an epic.</param>
        /// <param name="request">Update epic request.</param>
        /// <returns>The updated epic.</returns>
        Task<Epic> UpdateAsync(GroupId groupId, int epicIid, UpdateEpicRequest request);

        /// <summary>
        /// Delete an epic.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="epicIid">The Iid of an epic.</param>
        Task DeleteAsync(GroupId groupId, int epicIid);
    }
}
