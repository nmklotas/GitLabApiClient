using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Groups.Responses;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
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
        private readonly MilestonesQueryBuilder _queryMilestonesBuilder;

        internal GroupsClient(
            GitLabHttpFacade httpFacade,
            GroupsQueryBuilder queryBuilder,
            ProjectsGroupQueryBuilder projectsQueryBuilder,
            MilestonesQueryBuilder queryMilestonesBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
            _projectsQueryBuilder = projectsQueryBuilder;
            _queryMilestonesBuilder = queryMilestonesBuilder;
        }

        /// <summary>
        /// Get all details of a group.
        /// This endpoint can be accessed without authentication if the group is publicly accessible.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        public async Task<Group> GetAsync(object groupId) =>
            await _httpFacade.Get<Group>(groupId.GroupBaseUrl());

        /// <summary>
        /// Get all subgroups of a group.
        /// This endpoint can be accessed without authentication if the group is publicly accessible.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        public async Task<IList<Group>> GetSubgroupsAsync(object groupId) =>
            await _httpFacade.GetPagedList<Group>($"{groupId.GroupBaseUrl()}/subgroups");

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
        /// Get a list of projects in this group.
        /// When accessed without authentication, only public projects are returned.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="options">Groups projects retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Project>> GetProjectsAsync(object groupId, Action<ProjectsGroupQueryOptions> options = null)
        {
            var queryOptions = new ProjectsGroupQueryOptions();
            options?.Invoke(queryOptions);

            string url = _projectsQueryBuilder.Build($"{groupId.GroupBaseUrl()}/projects", queryOptions);
            return await _httpFacade.GetPagedList<Project>(url);
        }

        /// <summary>
        /// Get a list of members in this group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="search">A query string to search for members.</param>
        /// <returns>Group members satisfying options.</returns>
        public async Task<IList<Member>> GetMembersAsync(object groupId, string search = null)
        {
            string url = $"{groupId.GroupBaseUrl()}/members";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"?search={search}";
            }

            return await _httpFacade.GetPagedList<Member>(url);
        }

        /// <summary>
        /// Get a list of all members (including inherited) in this group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="search">A query string to search for members.</param>
        /// <returns>Group members satisfying options.</returns>
        public async Task<IList<Member>> GetAllMembersAsync(object groupId, string search = null)
        {
            string url = $"{groupId.GroupBaseUrl()}/members/all";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"?search={search}";
            }

            return await _httpFacade.GetPagedList<Member>(url);
        }

        /// <summary>
        /// Adds a member to the group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="request">Create milestone request.</param>
        /// <returns>Newly created milestone.</returns>
        public async Task<Milestone> CreateMilestoneAsync(object groupId, CreateGroupMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Milestone>($"{groupId.GroupBaseUrl()}/milestones", request);
        }

        /// <summary>
        /// Get a list of milestones in this group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="options">Query options.</param>
        public async Task<IList<Milestone>> GetMilestonesAsync(object groupId, Action<MilestonesQueryOptions> options = null)
        {
            var queryOptions = new MilestonesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryMilestonesBuilder.Build($"{groupId.GroupBaseUrl()}/milestones", queryOptions);
            return await _httpFacade.GetPagedList<Milestone>(url);
        }

        /// <summary>
        /// Retrieves a group milestone by its id.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="milestoneId">Id of the milestone.</param>
        public async Task<Milestone> GetMilestoneAsync(object groupId, int milestoneId) =>
            await _httpFacade.Get<Milestone>($"{groupId.GroupBaseUrl()}/milestones/{milestoneId}");

        /// <summary>
        /// Creates a new project group.
        /// Available only for users who can create groups.
        /// </summary>
        /// <returns>The newly created group.</returns>
        /// <param name="request">Create group request.</param>
        public async Task<Group> CreateAsync(CreateGroupRequest request) =>
            await _httpFacade.Post<Group>("groups", request);

        /// <summary>
        /// Adds a user to a group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="request">Add group member request.</param>
        /// <returns>Newly created membership.</returns>
        public async Task<Member> AddMemberAsync(object groupId, AddGroupMemberRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Member>($"{groupId.GroupBaseUrl()}/members", request);
        }

        /// <summary>
        /// Updates a user's group membership.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="userId">The user ID of the member.</param>
        /// <param name="request">Update group member request.</param>
        /// <returns>Updated membership.</returns>
        public async Task<Member> UpdateMemberAsync(object groupId, int userId, AddGroupMemberRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Member>($"{groupId.GroupBaseUrl()}/members/{userId}", request);
        }

        /// <summary>
        /// Removes a user as a member of the group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="userId">The user ID of the member.</param>
        public async Task RemoveMemberAsync(object groupId, int userId) =>
            await _httpFacade.Delete($"{groupId.GroupBaseUrl()}/members/{userId}");

        /// <summary>
        /// Transfer a project to the Group namespace. Available only for admin
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>The newly updated group.</returns>
        public async Task<Group> TransferAsync(object groupId, object projectId) =>
            await _httpFacade.Post<Group>($"{groupId.GroupBaseUrl()}/projects/{projectId.ProjectIdOrPath()}");

        /// <summary>
        /// Updates the project group.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <returns>The updated group.</returns>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="request">Update group request.</param>
        public async Task<Group> UpdateAsync(object groupId, UpdateGroupRequest request) =>
            await _httpFacade.Put<Group>(groupId.GroupBaseUrl(), request);

        /// <summary>
        /// Updates an existing group milestone.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="milestoneId">The ID of the group's milestone.</param>
        /// <param name="request">Update milestone request.</param>
        /// <returns>Newly modified milestone.</returns>
        public async Task<Milestone> UpdateMilestoneAsync(object groupId, int milestoneId, UpdateGroupMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Milestone>($"{groupId.GroupBaseUrl()}/milestones/{milestoneId}", request);
        }

        /// <summary>
        /// Removes group with all projects inside.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        public async Task DeleteAsync(object groupId) =>
            await _httpFacade.Delete(groupId.GroupBaseUrl());

        /// <summary>
        /// Deletes a group milestone. Only for user with developer access to the group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="milestoneId">The ID of the group's milestone.</param>
        public async Task DeleteMilestoneAsync(object groupId, int milestoneId) =>
            await _httpFacade.Delete($"{groupId.GroupBaseUrl()}/milestones/{milestoneId}");

        /// <summary>
        /// Syncs the group with its linked LDAP group.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        public async Task SyncLdapAsync(object groupId) =>
            await _httpFacade.Post($"{groupId.GroupBaseUrl()}/ldap_sync");

        /// <summary>
        /// Creates LDAP group link.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="request">Create LDAP group link request.</param>
        public async Task CreateLdapLinkAsync(object groupId, CreateLdapGroupLinkRequest request) =>
            await _httpFacade.Post($"{groupId.GroupBaseUrl()}/ldap_group_links", request);

        /// <summary>
        /// Deletes a LDAP group link.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="cn">The CN of a LDAP group</param>
        public async Task DeleteLdapLinkAsync(object groupId, string cn) =>
            await _httpFacade.Delete($"{groupId.GroupBaseUrl()}/ldap_group_links/{cn}");


        /// <summary>
        /// Deletes a LDAP group link for a specific LDAP provider.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the project.</param>
        /// <param name="provider">Name of a LDAP provider</param>
        /// <param name="cn">The CN of a LDAP group</param>
        public async Task DeleteProviderLdapLinkAsync(object groupId, string provider, string cn) =>
            await _httpFacade.Delete($"{groupId.GroupBaseUrl()}/ldap_group_links/{provider}/{cn}");
    }
}
