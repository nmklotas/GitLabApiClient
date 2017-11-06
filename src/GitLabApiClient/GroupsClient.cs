using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Groups.Responses;
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
        private readonly ProjectsGroupQueryBuilder _projectsQueryBuilder;

        internal GroupsClient(
            GitLabHttpFacade httpFacade,
            GroupsQueryBuilder queryBuilder,
            ProjectsGroupQueryBuilder projectsQueryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
            _projectsQueryBuilder = projectsQueryBuilder;
        }

        /// <summary>
        /// Get all details of a group. 
        /// This endpoint can be accessed without authentication if the group is publicly accessible.
        /// </summary>
        public async Task<Group> GetByGroupIdAsync(string groupId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _httpFacade.Get<Group>($"groups/{groupId}", cancellationToken);
        }


        /// <summary>
        /// Get all groups that match your string in their name or path.
        /// </summary>
        public async Task<IList<Group>> SearchAsync(string search, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _httpFacade.GetPagedList<Group>($"groups?search={search}", cancellationToken);
        }
            

        /// <summary>
        /// Get a list of visible groups for the authenticated user.
        /// When accessed without authentication, only public groups are returned.
        /// </summary>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Groups satisfying options.</returns>
        public async Task<IList<Group>> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await GetAsync(null, cancellationToken);
        }
        
        /// <summary>
        /// Get a list of visible groups for the authenticated user.
        /// When accessed without authentication, only public groups are returned.
        /// </summary>
        /// <param name="options">Groups retrieval options.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Groups satisfying options.</returns>
        public async Task<IList<Group>> GetAsync(Action<GroupsQueryOptions> options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var queryOptions = new GroupsQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build("groups", queryOptions);
            return await _httpFacade.GetPagedList<Group>(url, cancellationToken);
        }

        /// <summary>
        /// Get a list of projects in this group..
        /// When accessed without authentication, only public projects are returned..
        /// </summary>
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Project>> GetProjectsAsync(string groupId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await GetProjectsAsync(groupId, null, cancellationToken);
        }
        
        /// <summary>
        /// Get a list of projects in this group..
        /// When accessed without authentication, only public projects are returned..
        /// </summary>
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="options">Groups projects retrieval options.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Project>> GetProjectsAsync(string groupId, 
                                                           Action<ProjectsGroupQueryOptions> options,
                                                           CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var queryOptions = new ProjectsGroupQueryOptions(groupId);
            options?.Invoke(queryOptions);

            string url = _projectsQueryBuilder.Build($"groups/{groupId}/projects", queryOptions);
            return await _httpFacade.GetPagedList<Project>(url, cancellationToken);
        }

        /// <summary>
        /// Creates a new project group.
        /// Available only for users who can create groups.
        /// </summary>
        /// <returns>The newly created group.</returns>
        public async Task<Group> CreateAsync(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _httpFacade.Post<Group>("groups", request, cancellationToken);
        }


        /// <summary>
        /// Transfer a project to the Group namespace. Available only for admin
        /// </summary>
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="projectId">The ID or path of a project.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        /// <returns>The newly updated group.</returns>
        public async Task<Group> TransferAsync(string groupId, string projectId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _httpFacade.Post<Group>($"groups/{groupId}/projects/{projectId}", null, cancellationToken);
        }


        /// <summary>
        /// Updates the project group.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <returns>The updated group.</returns>
        public async Task<Group> UpdateAsync(UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _httpFacade.Put<Group>($"groups/{request.Id}", request, cancellationToken);
        }


        /// <summary>
        /// Removes group with all projects inside.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <param name="groupId">The ID or path of a user group.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        public async Task DeleteAsync(string groupId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _httpFacade.Delete($"groups/{groupId}", cancellationToken);
        }


        /// <summary>
        /// Syncs the group with its linked LDAP group.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <param name="groupId">The ID or path of a user group.</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        public async Task SyncLdapAsync(string groupId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _httpFacade.Post($"groups/{groupId}/ldap_sync", cancellationToken);
        }

        /// <summary>
        /// Creates LDAP group link.
        /// </summary>
        public async Task CreateLdapLinkAsync(CreateLdapGroupLinkRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _httpFacade.Post($"groups/{request.Id}/ldap_group_links", request, cancellationToken);
        }


        /// <summary>
        /// Deletes a LDAP group link.
        /// </summary>
        /// <param name="groupId">Id of the group.</param>
        /// <param name="cn">The CN of a LDAP group</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        public async Task DeleteLdapLinkAsync(int groupId, string cn, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _httpFacade.Delete($"groups/{groupId}/ldap_group_links/{cn}", cancellationToken);
        }



        /// <summary>
        /// Deletes a LDAP group link for a specific LDAP provider.
        /// </summary>
        /// <param name="groupId">Id of the group.</param>
        /// <param name="provider">Name of a LDAP provider</param>
        /// <param name="cn">The CN of a LDAP group</param>
        /// <param name="cancellationToken">Request CancellationToken</param>
        public async Task DeleteProviderLdapLinkAsync(int groupId,
                                                      string provider,
                                                      string cn,
                                                      CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _httpFacade.Delete($"groups/{groupId}/ldap_group_links/{provider}/{cn}", cancellationToken);
        }
            
    }
}
