using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.LabelEvents.Responses;

namespace GitLabApiClient
{
    public sealed class ResourceLabelEventsClient : IResourceLabelEventsClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal ResourceLabelEventsClient(GitLabHttpFacade httpFacade) =>
            _httpFacade = httpFacade;

        public async Task<IList<LabelEvent>> GetAllAsync(
            ProjectId projectId,
            EventResourceType eventResourceType,
            int resourceId)
        {
            string query =
                $"projects/{projectId}/{eventResourceType.GetDescription()}/{resourceId}/resource_label_events";
            return await _httpFacade.GetPagedList<LabelEvent>(query);
        }

        public async Task<IList<LabelEvent>> GetAsync(ProjectId projectId,
            EventResourceType eventResourceType,
            int resourceId,
            int eventId)
        {
            string query =
                $"projects/{projectId}/{eventResourceType.GetDescription()}/{resourceId}/resource_label_events/{eventId}";
            return await _httpFacade.GetPagedList<LabelEvent>(query);
        }
    }
}
