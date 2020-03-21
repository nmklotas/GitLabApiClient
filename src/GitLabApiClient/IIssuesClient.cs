using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;
using GitLabApiClient.Models.Notes.Requests;
using GitLabApiClient.Models.Notes.Responses;

namespace GitLabApiClient
{
    public interface IIssuesClient
    {
        /// <summary>
        /// Retrieves issues.
        /// By default retrieves opened issues from all users. The more specific setting win (if both project and group are set, only project issues will be retrieved).
        /// </summary>
        /// <example>
        /// <code>
        /// /* Get all issues */
        /// var client = new GitLabClient("https://gitlab.com", "PRIVATE-TOKEN");
        /// var allIssues = await client.Issues.GetAllAsync();
        /// </code>
        /// <code>
        /// /* Get project issues */
        /// var client = new GitLabClient("https://gitlab.com", "PRIVATE-TOKEN");
        /// string projectPath = "dev/group/project-1";
        /// var allIssues = await client.Issues.GetAllAsync(projectId: projectPath);
        /// // OR
        /// int projectId = 55;
        /// var allIssues = await client.Issues.GetAllAsync(projectId: projectId);
        /// // OR - Group ID is skipped, project ID is more specific
        /// int projectId = 55;
        /// int groupId = 181;
        /// var allIssues = await client.Issues.GetAllAsync(projectId: projectId, groupId: groupId);
        /// </code>
        /// <code>
        /// /* Get group issues */
        /// var client = new GitLabClient("https://gitlab.com", "PRIVATE-TOKEN");
        /// string groupPath = "dev/group1/subgroup-1";
        /// var allIssues = await client.Issues.GetAllAsync(groupId: groupPath);
        /// // OR
        /// int groupId = 55;
        /// var allIssues = await client.Issues.GetAllAsync(groupId: groupId);
        /// </code>
        /// </example>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        Task<IList<Issue>> GetAllAsync(ProjectId projectId = null, GroupId groupId = null, Action<IssuesQueryOptions> options = null);

        /// <summary>
        /// Retrieves project issue.
        /// </summary>
        Task<Issue> GetAsync(ProjectId projectId, int issueId);

        /// <summary>
        /// Retrieves issues from a project.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        [Obsolete("Use GetAllAsync instead")]
        Task<IList<Issue>> GetAsync(ProjectId projectId, Action<IssuesQueryOptions> options = null);

        /// <summary>
        /// Retrieves issues from all projects.
        /// By default retrieves opened issues from all users.
        /// </summary>
        /// <param name="options">Issues retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        [Obsolete("Use GetAllAsync instead")]
        Task<IList<Issue>> GetAsync(Action<IssuesQueryOptions> options = null);

        /// <summary>
        /// Retrieves project issue note.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">Iid of the issue.</param>
        /// <param name="noteId">Id of the note.</param>
        /// <returns>Issues satisfying options.</returns>
        Task<Note> GetNoteAsync(ProjectId projectId, int issueIid, int noteId);

        /// <summary>
        /// Retrieves notes (comments) of an issue.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">Iid of the issue.</param>
        /// <param name="options">IssueNotes retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        Task<IList<Note>> GetNotesAsync(ProjectId projectId, int issueIid, Action<IssueNotesQueryOptions> options = null);

        /// <summary>
        /// Creates new issue.
        /// </summary>
        /// <returns>The newly created issue.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create issue request.</param>
        Task<Issue> CreateAsync(ProjectId projectId, CreateIssueRequest request);

        /// <summary>
        /// Creates a new note (comment) to a single project issue.
        /// </summary>
        /// <returns>The newly created issue note.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="request">Create issue note request.</param>
        Task<Note> CreateNoteAsync(ProjectId projectId, int issueIid, CreateIssueNoteRequest request);

        /// <summary>
        /// Updated existing issue.
        /// </summary>
        /// <returns>The updated issue.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="request">Update issue request.</param>
        Task<Issue> UpdateAsync(ProjectId projectId, int issueIid, UpdateIssueRequest request);

        /// <summary>
        /// Modify existing note (comment) of an issue.
        /// </summary>
        /// <returns>The updated issue note.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="noteId">The ID of a note.</param>
        /// <param name="request">Update issue note request.</param>
        Task<Note> UpdateNoteAsync(ProjectId projectId, int issueIid, int noteId, UpdateIssueNoteRequest request);

        /// <summary>
        /// Deletes an existing note (comment) of an issue.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="issueIid">The IID of an issue.</param>
        /// <param name="noteId">The ID of a note.</param>
        Task DeleteNoteAsync(ProjectId projectId, int issueIid, int noteId);
    }
}
