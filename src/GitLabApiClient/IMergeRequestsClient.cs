using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.AwardEmojis.Responses;
using GitLabApiClient.Models.Discussions.Responses;
using GitLabApiClient.Models.MergeRequests.Requests;
using GitLabApiClient.Models.MergeRequests.Responses;
using GitLabApiClient.Models.Notes.Requests;
using GitLabApiClient.Models.Notes.Responses;
using GitLabApiClient.Models.Pipelines.Responses;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient
{
    public interface IMergeRequestsClient
    {
        /// <summary>
        /// Retrieves merge request from a project.
        /// By default returns opened merged requests created by anyone.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="options">Merge requests retrieval options.</param>
        /// <returns>Merge requests satisfying options.</returns>
        Task<IList<MergeRequest>> GetAsync(ProjectId projectId, Action<ProjectMergeRequestsQueryOptions> options = null);

        /// <summary>
        /// Retrieves merge request from all projects the authenticated user has access to.
        /// By default returns opened merged requests created by anyone.
        /// </summary>
        /// <param name="options">Merge requests retrieval options.</param>
        /// <returns>Merge requests satisfying options.</returns>
        Task<IList<MergeRequest>> GetAsync(Action<MergeRequestsQueryOptions> options = null);

        /// <summary>
        /// Get single Merge requests by id
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The Internal Merge Request Id.</param>
        /// <returns>Shows information about a single merge request.</returns>
        Task<MergeRequest> GetAsync(ProjectId projectId, int mergeRequestId);

        /// <summary>
        /// Creates merge request.
        /// </summary>
        /// <returns>The newly created merge request.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create Merge request.</param>
        Task<MergeRequest> CreateAsync(ProjectId projectId, CreateMergeRequest request);

        /// <summary>
        /// Updates merge request.
        /// </summary>
        /// <returns>The updated merge request.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The Internal Merge Request Id.</param>
        /// <param name="request">Update Merge request.</param>
        Task<MergeRequest> UpdateAsync(ProjectId projectId, int mergeRequestId, UpdateMergeRequest request);

        /// <summary>
        /// Accepts merge request.
        /// </summary>
        /// <returns>The accepted merge request.</returns>
        /// /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The Internal Merge Request Id.</param>
        /// <param name="request">Accept Merge request.</param>
        Task<MergeRequest> AcceptAsync(ProjectId projectId, int mergeRequestId, AcceptMergeRequest request);

        /// <summary>
        /// Deletes merge request.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The Internal Merge Request Id.</param>
        Task DeleteAsync(ProjectId projectId, int mergeRequestId);

        /// <summary>
        /// Creates a new note (comment) to a single Merge Request.
        /// </summary>
        /// <returns>The newly created issue note.</returns>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The IID of an Merge Request.</param>
        /// <param name="request">Create Merge Request note request.</param>
        Task<Note> CreateNoteAsync(ProjectId projectId, int mergeRequestId, CreateMergeRequestNoteRequest request);

        /// <summary>
        /// Retrieves notes (comments) of a merge request.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestIid">Iid of the merge request.</param>
        /// <param name="options">MergeRequestNotes retrieval options.</param>
        /// <returns>Merge requests satisfying options.</returns>
        Task<IList<Note>> GetNotesAsync(ProjectId projectId, int mergeRequestIid, Action<MergeRequestNotesQueryOptions> options = null);

        /// <summary>
        /// List erge request pipelines
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestId">The Internal Merge Request Id.</param>
        /// <returns>Get a list of merge request pipelines.</returns>
        Task<IList<Pipeline>> GetPipelinesAsync(ProjectId projectId, int mergeRequestId);

        /// <summary>
        /// Retrieves discussions of a merge request.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestIid">Iid of the merge request.</param>
        Task<IList<Discussion>> GetDiscussionsAsync(ProjectId projectId, int mergeRequestIid);

        /// <summary>
        /// Retrieves a list of all award emoji for a specified merge request.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="mergeRequestIid">The Internal Merge Request Id.</param>
        Task<IList<AwardEmoji>> GetAwardEmojisAsync(ProjectId projectId, int mergeRequestIid);
    }
}
