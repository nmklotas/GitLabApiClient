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
        public async Task<Group> GetAsync(string groupId) =>
            await _httpFacade.Get<Group>($"groups/{groupId}");

        /// <summary>
        /// Get all subgroups of a group.
        /// This endpoint can be accessed without authentication if the group is publicly accessible.
        /// </summary>
        public async Task<IList<Group>> GetSubgroupsAsync(string groupId) =>
            await _httpFacade.GetPagedList<Group>($"groups/{groupId}/subgroups");

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
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="options">Groups projects retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Project>> GetProjectsAsync(string groupId, Action<ProjectsGroupQueryOptions> options = null)
        {
            var queryOptions = new ProjectsGroupQueryOptions();
            options?.Invoke(queryOptions);

            string url = _projectsQueryBuilder.Build($"groups/{groupId}/projects", queryOptions);
            return await _httpFacade.GetPagedList<Project>(url);
        }

        /// <summary>
        /// Get a list of members in this group.
        /// </summary>
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="search">A query string to search for members.</param>
        /// <returns>Group members satisfying options.</returns>
        public async Task<IList<Member>> GetMembersAsync(string groupId, string search = null)
        {
            string url = $"groups/{groupId}/members";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"?search={search}";
            }

            return await _httpFacade.GetPagedList<Member>(url);
        }

        /// <summary>
        /// Get a list of all members (including inherited) in this group.
        /// </summary>
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="search">A query string to search for members.</param>
        /// <returns>Group members satisfying options.</returns>
        public async Task<IList<Member>> GetAllMembersAsync(string groupId, string search = null)
        {
            string url = $"groups/{groupId}/members/all";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"?search={search}";
            }

            return await _httpFacade.GetPagedList<Member>(url);
        }

        /// <summary>
        /// Adds a member to the group.
        /// </summary>
        /// <param name="request">Create milestone request.</param>
        /// <returns>Newly created milestone.</returns>
        public async Task<Milestone> CreateMilestoneAsync(CreateGroupMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Milestone>($"groups/{request.GroupId}/milestones", request);
        }

        /// <summary>
        /// Get a list of milestones in this group.
        /// </summary>
        /// <param name="groupId">Id of the group.</param>
        /// <param name="options">Query options.</param>
        public async Task<IList<Milestone>> GetMilestonesAsync(int groupId, Action<MilestonesQueryOptions> options = null)
        {
            var queryOptions = new MilestonesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryMilestonesBuilder.Build($"groups/{groupId}/milestones", queryOptions);
            return await _httpFacade.GetPagedList<Milestone>(url);
        }

        /// <summary>
        /// Retrieves a group milestone by its id.
        /// </summary>
        /// <param name="groupId">Id of the group.</param>
        /// <param name="milestoneId">Id of the milestone.</param>
        public async Task<Milestone> GetMilestoneAsync(int groupId, int milestoneId) =>
            await _httpFacade.Get<Milestone>($"groups/{groupId}/milestones/{milestoneId}");

        /// <summary>
        /// Creates a new project group.
        /// Available only for users who can create groups.
        /// </summary>
        /// <returns>The newly created group.</returns>
        public async Task<Group> CreateAsync(CreateGroupRequest request) =>
            await _httpFacade.Post<Group>("groups", request);

        /// <summary>
        /// Adds a user to a group.
        /// </summary>
        /// <param name="request">Add group member request.</param>
        /// <returns>Newly created membership.</returns>
        public async Task<Member> AddMemberAsync(AddGroupMemberRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Member>($"groups/{request.GroupId}/members", request);
        }

        /// <summary>
        /// Updates a user's group membership.
        /// </summary>
        /// <param name="request">Update group member request.</param>
        /// <returns>Updated membership.</returns>
        public async Task<Member> UpdateMemberAsync(AddGroupMemberRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Member>($"groups/{request.GroupId}/members/{request.UserId}", request);
        }

        /// <summary>
        /// Removes a user as a member of the group.
        /// </summary>
        /// <param name="groupId">The ID or path of a group.</param>
        /// <param name="userId">The id of the user.</param>
        public async Task RemoveMemberAsync(string groupId, int userId) =>
            await _httpFacade.Delete($"groups/{groupId}/members/{userId}");

        /// <summary>
        /// Transfer a project to the Group namespace. Available only for admin
        /// </summary>
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="projectId">The ID or path of a project.</param>
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
        /// Updates an existing group milestone.
        /// </summary>
        /// <param name="request">Update milestone request.</param>
        /// <returns>Newly modified milestone.</returns>
        public async Task<Milestone> UpdateMilestoneAsync(UpdateGroupMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Milestone>($"groups/{request.GroupId}/milestones/{request.MilestoneId}", request);
        }

        /// <summary>
        /// Removes group with all projects inside.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <param name="groupId">The ID or path of a user group.</param>
        public async Task DeleteAsync(string groupId) =>
            await _httpFacade.Delete($"groups/{groupId}");

        /// <summary>
        /// Deletes a group milestone. Only for user with developer access to the group.
        /// </summary>
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="milestoneId">The ID of the group's milestone.</param>
        public async Task DeleteMilestoneAsync(int groupId, int milestoneId) =>
            await _httpFacade.Delete($"groups/{groupId}/milestones/{milestoneId}");

        /// <summary>
        /// Syncs the group with its linked LDAP group.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <param name="groupId">The ID or path of a user group.</param>
        public async Task SyncLdapAsync(string groupId) =>
            await _httpFacade.Post($"groups/{groupId}/ldap_sync");

        /// <summary>
        /// Creates LDAP group link.
        /// </summary>
        public async Task CreateLdapLinkAsync(CreateLdapGroupLinkRequest request) =>
            await _httpFacade.Post($"groups/{request.Id}/ldap_group_links", request);

        /// <summary>
        /// Deletes a LDAP group link.
        /// </summary>
        /// <param name="groupId">Id of the group.</param>
        /// <param name="cn">The CN of a LDAP group</param>
        public async Task DeleteLdapLinkAsync(int groupId, string cn) =>
            await _httpFacade.Delete($"groups/{groupId}/ldap_group_links/{cn}");


        /// <summary>
        /// Deletes a LDAP group link for a specific LDAP provider.
        /// </summary>
        /// <param name="groupId">Id of the group.</param>
        /// <param name="provider">Name of a LDAP provider</param>
        /// <param name="cn">The CN of a LDAP group</param>
        public async Task DeleteProviderLdapLinkAsync(int groupId, string provider, string cn) =>
            await _httpFacade.Delete($"groups/{groupId}/ldap_group_links/{provider}/{cn}");
    }
}
