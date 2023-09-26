using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.LabelEvents.Responses;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient
{
    public interface IResourceLabelEventsClient
    {
        /// <summary>
        /// Retrieves label events from issues.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="eventResourceType">Resource type (supported resources: Issues, Epics, MergeRequests)</param>
        /// <param name="resourceId">The IID of an issue, ID of an Epic, or IID of a Merge Request</param>
        /// <returns>Label Events for the given resource type and id</returns>
        Task<IList<LabelEvent>> GetAllAsync(ProjectId projectId, EventResourceType eventResourceType, int resourceId);

        /// <summary>
        /// Retrieves a single label event from issues.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="eventResourceType">Resource type (supported resources: Issues, Epics, MergeRequests)</param>
        /// <param name="resourceId">The IID of an issue, ID of an Epic, or IID of a Merge Request</param>
        /// <param name="eventId">The ID of a label event</param>
        /// <returns>Label Events for the given issue Id</returns>
        Task<IList<LabelEvent>> GetAsync(ProjectId projectId, EventResourceType eventResourceType, int resourceId,
            int eventId);
    }
}
