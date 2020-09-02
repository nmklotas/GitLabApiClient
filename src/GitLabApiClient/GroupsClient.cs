using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Groups.Responses;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Runners.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to retrieve, modify, create groups.
    /// <exception cref="GitLabException">Thrown if request to GitLab API does not indicate success</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class GroupsClient : IGroupsClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly GroupsQueryBuilder _queryBuilder;
        private readonly ProjectsGroupQueryBuilder _projectsQueryBuilder;
        private readonly MilestonesQueryBuilder _queryMilestonesBuilder;
        private readonly GroupLabelsQueryBuilder _queryGroupLabelBuilder;

        internal GroupsClient(
            GitLabHttpFacade httpFacade,
            GroupsQueryBuilder queryBuilder,
            ProjectsGroupQueryBuilder projectsQueryBuilder,
            MilestonesQueryBuilder queryMilestonesBuilder,
            GroupLabelsQueryBuilder queryGroupLabelBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
            _projectsQueryBuilder = projectsQueryBuilder;
            _queryMilestonesBuilder = queryMilestonesBuilder;
            _queryGroupLabelBuilder = queryGroupLabelBuilder;
        }

        /// <summary>
        /// Get all details of a group.
        /// This endpoint can be accessed without authentication if the group is publicly accessible.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        public async Task<Group> GetAsync(GroupId groupId) =>
            await _httpFacade.Get<Group>($"groups/{groupId}");

        /// <summary>
        /// Get all subgroups of a group.
        /// This endpoint can be accessed without authentication if the group is publicly accessible.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        public async Task<IList<Group>> GetSubgroupsAsync(GroupId groupId) =>
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
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="options">Groups projects retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        public async Task<IList<Project>> GetProjectsAsync(GroupId groupId, Action<ProjectsGroupQueryOptions> options = null)
        {
            var queryOptions = new ProjectsGroupQueryOptions();
            options?.Invoke(queryOptions);

            string url = _projectsQueryBuilder.Build($"groups/{groupId}/projects", queryOptions);
            return await _httpFacade.GetPagedList<Project>(url);
        }

        /// <summary>
        /// Get a list of members in this group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="search">A query string to search for members.</param>
        /// <returns>Group members satisfying options.</returns>
        public async Task<IList<Member>> GetMembersAsync(GroupId groupId, string search = null)
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
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="search">A query string to search for members.</param>
        /// <returns>Group members satisfying options.</returns>
        public async Task<IList<Member>> GetAllMembersAsync(GroupId groupId, string search = null)
        {
            string url = $"groups/{groupId}/members/all";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"?search={search}";
            }

            return await _httpFacade.GetPagedList<Member>(url);
        }

        /// <summary>
        /// Add a new milestone to a group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Create milestone request.</param>
        /// <returns>Newly created milestone.</returns>
        public async Task<Milestone> CreateMilestoneAsync(GroupId groupId, CreateGroupMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Milestone>($"groups/{groupId}/milestones", request);
        }

        /// <summary>
        /// Get a list of milestones in this group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="options">Query options.</param>
        public async Task<IList<Milestone>> GetMilestonesAsync(GroupId groupId, Action<MilestonesQueryOptions> options = null)
        {
            var queryOptions = new MilestonesQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryMilestonesBuilder.Build($"groups/{groupId}/milestones", queryOptions);
            return await _httpFacade.GetPagedList<Milestone>(url);
        }

        /// <summary>
        /// Retrieves a group milestone by its id.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="milestoneId">Id of the milestone.</param>
        public async Task<Milestone> GetMilestoneAsync(GroupId groupId, int milestoneId) =>
            await _httpFacade.Get<Milestone>($"groups/{groupId}/milestones/{milestoneId}");

        /// <summary>
        /// Get a list of runners in a group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        public async Task<IList<Runner>> GetRunnersAsync(GroupId groupId) =>
            await _httpFacade.Get<IList<Runner>>($"groups/{groupId}/runners");

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
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Add group member request.</param>
        /// <returns>Newly created membership.</returns>
        public async Task<Member> AddMemberAsync(GroupId groupId, AddGroupMemberRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Member>($"groups/{groupId}/members", request);
        }

        /// <summary>
        /// Updates a user's group membership.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="userId">The user ID of the member.</param>
        /// <param name="request">Update group member request.</param>
        /// <returns>Updated membership.</returns>
        public async Task<Member> UpdateMemberAsync(GroupId groupId, int userId, AddGroupMemberRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Member>($"groups/{groupId}/members/{userId}", request);
        }

        /// <summary>
        /// Removes a user as a member of the group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="userId">The user ID of the member.</param>
        public async Task RemoveMemberAsync(GroupId groupId, int userId) =>
            await _httpFacade.Delete($"groups/{groupId}/members/{userId}");

        /// <summary>
        /// Transfer a project to the Group namespace. Available only for admin
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>The newly updated group.</returns>
        public async Task<Group> TransferAsync(GroupId groupId, ProjectId projectId) =>
            await _httpFacade.Post<Group>($"groups/{groupId}/projects/{projectId}");

        /// <summary>
        /// Updates the project group.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <returns>The updated group.</returns>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Update group request.</param>
        public async Task<Group> UpdateAsync(GroupId groupId, UpdateGroupRequest request) =>
            await _httpFacade.Put<Group>($"groups/{groupId}", request);

        /// <summary>
        /// Updates an existing group milestone.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="milestoneId">The ID of the group's milestone.</param>
        /// <param name="request">Update milestone request.</param>
        /// <returns>Newly modified milestone.</returns>
        public async Task<Milestone> UpdateMilestoneAsync(GroupId groupId, int milestoneId, UpdateGroupMilestoneRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Milestone>($"groups/{groupId}/milestones/{milestoneId}", request);
        }

        /// <summary>
        /// Removes group with all projects inside.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        public async Task DeleteAsync(GroupId groupId) =>
            await _httpFacade.Delete($"groups/{groupId}");

        /// <summary>
        /// Deletes a group milestone. Only for user with developer access to the group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="milestoneId">The ID of the group's milestone.</param>
        public async Task DeleteMilestoneAsync(GroupId groupId, int milestoneId) =>
            await _httpFacade.Delete($"groups/{groupId}/milestones/{milestoneId}");

        /// <summary>
        /// Syncs the group with its linked LDAP group.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        public async Task SyncLdapAsync(GroupId groupId) =>
            await _httpFacade.Post($"groups/{groupId}/ldap_sync");

        /// <summary>
        /// Creates LDAP group link.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Create LDAP group link request.</param>
        public async Task CreateLdapLinkAsync(GroupId groupId, CreateLdapGroupLinkRequest request) =>
            await _httpFacade.Post($"groups/{groupId}/ldap_group_links", request);

        /// <summary>
        /// Deletes a LDAP group link.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="cn">The CN of a LDAP group</param>
        public async Task DeleteLdapLinkAsync(GroupId groupId, string cn) =>
            await _httpFacade.Delete($"groups/{groupId}/ldap_group_links/{cn}");


        /// <summary>
        /// Deletes a LDAP group link for a specific LDAP provider.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="provider">Name of a LDAP provider</param>
        /// <param name="cn">The CN of a LDAP group</param>
        public async Task DeleteProviderLdapLinkAsync(GroupId groupId, string provider, string cn) =>
            await _httpFacade.Delete($"groups/{groupId}/ldap_group_links/{provider}/{cn}");


        /// <summary>
        /// Get all labels for a given group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="options">Query options</param>
        public async Task<IList<GroupLabel>> GetLabelsAsync(GroupId groupId,
            Action<GroupLabelsQueryOptions> options = null)
        {
            var labelOptions = new GroupLabelsQueryOptions();
            options?.Invoke(labelOptions);

            string url = _queryGroupLabelBuilder.Build($"groups/{groupId}/labels", labelOptions);
            return await _httpFacade.GetPagedList<GroupLabel>(url);
        }

        /// <summary>
        /// Creates new group label.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Create label request.</param>
        /// <returns>Newly created label.</returns>
        public async Task<GroupLabel> CreateLabelAsync(GroupId groupId, CreateGroupLabelRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<GroupLabel>($"groups/{groupId}/labels", request);
        }

        /// <summary>
        /// Updates an existing label with new name or new color. At least one parameter is required, to update the label.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Update label request.</param>
        /// <returns>Newly modified label.</returns>
        public async Task<GroupLabel> UpdateLabelAsync(GroupId groupId, UpdateGroupLabelRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<GroupLabel>($"groups/{groupId}/labels", request);
        }

        /// <summary>
        /// Deletes group labels.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="name">Name of the label.</param>
        public async Task DeleteLabelAsync(GroupId groupId, string name) =>
            await _httpFacade.Delete($"groups/{groupId}/labels?name={name}");

        /// <summary>
        /// Retrieves group variables by its id.
        /// </summary>
        /// <param name="groupId">Id of the group.</param>
        public async Task<IList<Variable>> GetVariablesAsync(GroupId groupId) =>
            await _httpFacade.GetPagedList<Variable>($"groups/{groupId}/variables");

        /// <summary>
        /// Creates new project variable.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Create variable request.</param>
        /// <returns>Newly created variable.</returns>
        public async Task<Variable> CreateVariableAsync(GroupId groupId, CreateGroupVariableRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Post<Variable>($"groups/{groupId}/variables", request);
        }

        /// <summary>
        /// Updates an existing group variable.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Update variable request.</param>
        /// <returns>Newly modified variable.</returns>
        public async Task<Variable> UpdateVariableAsync(GroupId groupId, UpdateGroupVariableRequest request)
        {
            Guard.NotNull(request, nameof(request));
            return await _httpFacade.Put<Variable>($"groups/{groupId}/variables/{request.Key}", request);
        }

        /// <summary>
        /// Deletes group variable
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="key">The Key ID of the variable.</param>
        public async Task DeleteVariableAsync(GroupId groupId, string key) =>
            await _httpFacade.Delete($"groups/{groupId}/variables/{key}");
    }
}
