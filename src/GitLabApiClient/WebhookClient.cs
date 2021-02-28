using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Webhooks.Requests;
using GitLabApiClient.Models.Webhooks.Responses;

namespace GitLabApiClient
{
    public sealed class WebhookClient : IWebhookClient
    {
        private readonly GitLabHttpFacade _httpFacade;

        internal WebhookClient(GitLabHttpFacade httpFacade)
        {
            _httpFacade = httpFacade;
        }

        /// <summary>
        /// Retrieves a hook by its id
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="hookId">The hook ID, you want to retrieve.</param>
        /// <returns></returns>
        public async Task<Webhook> GetAsync(ProjectId projectId, long hookId) =>
            await _httpFacade.Get<Webhook>($"projects/{projectId}/hooks/{hookId}");

        /// <summary>
        /// Retrieves all project hooks
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns></returns>
        public async Task<IList<Webhook>> GetAsync(ProjectId projectId)
        {
            return await _httpFacade.GetPagedList<Webhook>($"projects/{projectId}/hooks");
        }

        /// <summary>
        /// Create new webhook
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="request">Create hook request.</param>
        /// <returns>newly created hook</returns>
        public async Task<Webhook> CreateAsync(ProjectId projectId, CreateWebhookRequest request) =>
            await _httpFacade.Post<Webhook>($"projects/{projectId}/hooks", request);

        /// <summary>
        /// Delete a webhook
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="hookId">The hook ID, you want to delete.</param>
        public async Task DeleteAsync(ProjectId projectId, long hookId) =>
            await _httpFacade.Delete($"projects/{projectId}/hooks/{hookId}");

        /// <summary>
        /// Update new webhook
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="hookId">The hook ID, you want to update.</param>
        /// <param name="request">Create hook request.</param>
        /// <returns>newly created hook</returns>
        public async Task<Webhook> UpdateAsync(ProjectId projectId, long hookId, CreateWebhookRequest request) =>
            await _httpFacade.Put<Webhook>($"projects/{projectId}/hooks/{hookId}", request);
    }


}
