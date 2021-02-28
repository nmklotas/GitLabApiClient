using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Groups.Responses;
using GitLabApiClient.Models.Milestones.Requests;
using GitLabApiClient.Models.Milestones.Responses;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Runners.Responses;

namespace GitLabApiClient
{
    public interface IGroupsClient
    {
        /// <summary>
        /// Get all details of a group.
        /// This endpoint can be accessed without authentication if the group is publicly accessible.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        Task<Group> GetAsync(GroupId groupId);

        /// <summary>
        /// Get all subgroups of a group.
        /// This endpoint can be accessed without authentication if the group is publicly accessible.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        Task<IList<Group>> GetSubgroupsAsync(GroupId groupId);

        /// <summary>
        /// Get all groups that match your string in their name or path.
        /// </summary>
        Task<IList<Group>> SearchAsync(string search);

        /// <summary>
        /// Get a list of visible groups for the authenticated user.
        /// When accessed without authentication, only public groups are returned.
        /// </summary>
        /// <param name="options">Groups retrieval options.</param>
        /// <returns>Groups satisfying options.</returns>
        Task<IList<Group>> GetAsync(Action<GroupsQueryOptions> options = null);

        /// <summary>
        /// Get a list of projects in this group.
        /// When accessed without authentication, only public projects are returned.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="options">Groups projects retrieval options.</param>
        /// <returns>Issues satisfying options.</returns>
        Task<IList<Project>> GetProjectsAsync(GroupId groupId, Action<ProjectsGroupQueryOptions> options = null);

        /// <summary>
        /// Get a list of members in this group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="search">A query string to search for members.</param>
        /// <returns>Group members satisfying options.</returns>
        Task<IList<Member>> GetMembersAsync(GroupId groupId, string search = null);

        /// <summary>
        /// Get a list of all members (including inherited) in this group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="search">A query string to search for members.</param>
        /// <returns>Group members satisfying options.</returns>
        Task<IList<Member>> GetAllMembersAsync(GroupId groupId, string search = null);

        /// <summary>
        /// Adds a member to the group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Create milestone request.</param>
        /// <returns>Newly created milestone.</returns>
        Task<Milestone> CreateMilestoneAsync(GroupId groupId, CreateGroupMilestoneRequest request);

        /// <summary>
        /// Get a list of milestones in this group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="options">Query options.</param>
        Task<IList<Milestone>> GetMilestonesAsync(GroupId groupId, Action<MilestonesQueryOptions> options = null);

        /// <summary>
        /// Retrieves a group milestone by its id.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="milestoneId">Id of the milestone.</param>
        Task<Milestone> GetMilestoneAsync(GroupId groupId, int milestoneId);

        /// <summary>
        /// Get a list of runners in a group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        Task<IList<Runner>> GetRunnersAsync(GroupId groupId);

        /// <summary>
        /// Creates a new project group.
        /// Available only for users who can create groups.
        /// </summary>
        /// <returns>The newly created group.</returns>
        /// <param name="request">Create group request.</param>
        Task<Group> CreateAsync(CreateGroupRequest request);

        /// <summary>
        /// Adds a user to a group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Add group member request.</param>
        /// <returns>Newly created membership.</returns>
        Task<Member> AddMemberAsync(GroupId groupId, AddGroupMemberRequest request);

        /// <summary>
        /// Updates a user's group membership.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="userId">The user ID of the member.</param>
        /// <param name="request">Update group member request.</param>
        /// <returns>Updated membership.</returns>
        Task<Member> UpdateMemberAsync(GroupId groupId, int userId, AddGroupMemberRequest request);

        /// <summary>
        /// Removes a user as a member of the group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="userId">The user ID of the member.</param>
        Task RemoveMemberAsync(GroupId groupId, int userId);

        /// <summary>
        /// Transfer a project to the Group namespace. Available only for admin
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <returns>The newly updated group.</returns>
        Task<Group> TransferAsync(GroupId groupId, ProjectId projectId);

        /// <summary>
        /// Updates the project group.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <returns>The updated group.</returns>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Update group request.</param>
        Task<Group> UpdateAsync(GroupId groupId, UpdateGroupRequest request);

        /// <summary>
        /// Updates an existing group milestone.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="milestoneId">The ID of the group's milestone.</param>
        /// <param name="request">Update milestone request.</param>
        /// <returns>Newly modified milestone.</returns>
        Task<Milestone> UpdateMilestoneAsync(GroupId groupId, int milestoneId, UpdateGroupMilestoneRequest request);

        /// <summary>
        /// Removes group with all projects inside.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        Task DeleteAsync(GroupId groupId);

        /// <summary>
        /// Deletes a group milestone. Only for user with developer access to the group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="milestoneId">The ID of the group's milestone.</param>
        Task DeleteMilestoneAsync(GroupId groupId, int milestoneId);

        /// <summary>
        /// Syncs the group with its linked LDAP group.
        /// Only available to group owners and administrators.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        Task SyncLdapAsync(GroupId groupId);

        /// <summary>
        /// Creates LDAP group link.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Create LDAP group link request.</param>
        Task CreateLdapLinkAsync(GroupId groupId, CreateLdapGroupLinkRequest request);

        /// <summary>
        /// Deletes a LDAP group link.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="cn">The CN of a LDAP group</param>
        Task DeleteLdapLinkAsync(GroupId groupId, string cn);

        /// <summary>
        /// Deletes a LDAP group link for a specific LDAP provider.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="provider">Name of a LDAP provider</param>
        /// <param name="cn">The CN of a LDAP group</param>
        Task DeleteProviderLdapLinkAsync(GroupId groupId, string provider, string cn);

        /// <summary>
        /// Get all labels for a given group.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="options">Query options</param>
        Task<IList<GroupLabel>> GetLabelsAsync(GroupId groupId,
            Action<GroupLabelsQueryOptions> options = null);

        /// <summary>
        /// Creates new group label.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Create label request.</param>
        /// <returns>Newly created label.</returns>
        Task<GroupLabel> CreateLabelAsync(GroupId groupId, CreateGroupLabelRequest request);

        /// <summary>
        /// Updates an existing label with new name or new color. At least one parameter is required, to update the label.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Update label request.</param>
        /// <returns>Newly modified label.</returns>
        Task<GroupLabel> UpdateLabelAsync(GroupId groupId, UpdateGroupLabelRequest request);

        /// <summary>
        /// Deletes group labels.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="name">Name of the label.</param>
        Task DeleteLabelAsync(GroupId groupId, string name);

        /// <summary>
        /// Retrieves group variables by its id.
        /// </summary>
        /// <param name="groupId">Id of the group.</param>
        Task<IList<Variable>> GetVariablesAsync(GroupId groupId);

        /// <summary>
        /// Creates new project variable.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Create variable request.</param>
        /// <returns>Newly created variable.</returns>
        Task<Variable> CreateVariableAsync(GroupId groupId, CreateGroupVariableRequest request);

        /// <summary>
        /// Updates an existing group variable.
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="request">Update variable request.</param>
        /// <returns>Newly modified variable.</returns>
        Task<Variable> UpdateVariableAsync(GroupId groupId, UpdateGroupVariableRequest request);

        /// <summary>
        /// Deletes group variable
        /// </summary>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="key">The Key ID of the variable.</param>
        Task DeleteVariableAsync(GroupId groupId, string key);
    }
}
