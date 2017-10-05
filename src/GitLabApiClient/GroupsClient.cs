using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to retrieve, modify, create groups.
    /// <exception cref="GitLabException">Thrown if request to GitLab API does not indicate success</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class GroupsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly GroupsQueryBuilder _queryBuilder;
        private readonly GroupsProjectsQueryBuilder _projectsQueryBuilder;
        internal GroupsClient(GitLabHttpFacade httpFacade, GroupsQueryBuilder queryBuilder, GroupsProjectsQueryBuilder projectsQueryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
            _projectsQueryBuilder = projectsQueryBuilder;
        }

		/// <summary>
		/// Get all details of a group. 
        /// This endpoint can be accessed without authentication if the group is publicly accessible.
		/// </summary>
		public async Task<Group> GetAsync(string groupId) =>
            await _httpFacade.Get<Group>($"groups/{groupId}");


		/// <summary>
		/// Get all groups that match your string in their name or path.
		/// </summary>
		public async Task<IList<Group>> SearchAsync(string search) =>
			await _httpFacade.GetPagedList<Group>($"groups?search={search}");

        /// <summary>
        /// Get a list of visible groups for the authenticated user.
        /// When accessed without authentication, only public groups are returned.
        /// </summary>
        /// <param name="options">Groups retrieval options.</param>
        /// <returns>Groups satisfying options.</returns>
        public async Task<IList<Group>> GetAsync(Action<GroupsQueryOptions> options = null)
        {
            var queryOptions = new GroupsQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build("groups", queryOptions);
            return await _httpFacade.GetPagedList<Group>(url);
        }

        /// <summary>
        /// Get a list of projects in this group..
        /// When accessed without authentication, only public projects are returned..
        /// </summary>
        /// <param name="groupId">Id of the group.</param>
        /// <param name="options">Projects retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Project>> GetAsync(object groupId, Action<GroupsProjectsQueryOptions> options = null)
        {
            var queryOptions = new GroupsProjectsQueryOptions { Id = groupId };
            options?.Invoke(queryOptions);

            string url = _projectsQueryBuilder.Build($"groups/{groupId}/projects", queryOptions);
            return await _httpFacade.GetPagedList<Project>(url);
        }

		/// <summary>
		/// Creates a new project group.
        /// Available only for users who can create groups.
		/// </summary>
		/// <returns>The newly created group.</returns>
		public async Task<Group> CreateAsync(CreateGroupRequest request) =>
            await _httpFacade.Post<Group>($"groups", request);

		/// <summary>
		/// Transfer a project to the Group namespace. Available only for admin
		/// </summary>
		/// <returns>The newly updated group.</returns>
		public async Task<Group> TransferAsync(string groupId, string projectId) =>
            await _httpFacade.Post<Group>($"groups/{groupId}/projects/{projectId}", null);

		/// <summary>
		/// Updates the project group.
        /// Only available to group owners and administrators.
		/// </summary>
		/// <returns>The updated group.</returns>
		public async Task<Group> UpdateAsync(UpdateGroupRequest request) =>
            await _httpFacade.Put<Group>($"groups/{request.Id}", request);

		/// <summary>
		/// Removes group with all projects inside.
		/// Only available to group owners and administrators.
		/// </summary>
		/// <param name="groupId">Id of the group.</param>
		public async Task DeleteAsync(int groupId) =>
            await _httpFacade.Delete($"groups/{groupId}");

		/// <summary>
		/// Syncs the group with its linked LDAP group.
		/// Only available to group owners and administrators.
		/// </summary>
		/// <param name="groupId">Id of the group.</param>
		public async Task SyncLdapAsync(string groupId) =>
			await _httpFacade.Post($"groups/{groupId}/ldap_sync", null);

		/// <summary>
		/// Adds LDAP group link
		/// </summary>
		public async Task AddLdapLinkAsync(GroupLdapLinkRequest request) =>
            await _httpFacade.Post($"groups/{request.Id}/ldap_group_links", request);

		/// <summary>
		/// Deletes a LDAP group link
		/// </summary>
		/// <param name="groupId">Id of the group.</param>
		/// <param name="cn">The CN of a LDAP group</param>
		public async Task DeleteLdapAsync(int groupId, string cn) =>
            await _httpFacade.Delete($"groups/{groupId}/ldap_group_links/{cn}");


		/// <summary>
		/// Deletes a LDAP group link for a specific LDAP provider
		/// </summary>
		/// <param name="groupId">Id of the group.</param>
		/// <param name="provider">Name of a LDAP provider</param>
		/// <param name="cn">The CN of a LDAP group</param>
		public async Task DeleteProvidersLdapAsync(int groupId, string provider, string cn) =>
            await _httpFacade.Delete($"groups/{groupId}/ldap_group_links/{provider}/{cn}");
	}
}
