using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;
namespace GitLabApiClient.Models.Groups.Requests
{
    /// <summary>
    /// Used to create a new project group. Available only for users who can create groups.
    /// </summary>
    public sealed class CreateGroupRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGroupRequest"/> class.
        /// </summary>
        /// <param name="name">The name of the group.</param>
        /// <param name="path">The path of the group.</param>
        public CreateGroupRequest(string name, string path)
        {
            Guard.NotEmpty(name, nameof(name));
            Guard.NotEmpty(path, nameof(path));
            Name = name;
            Path = path;
        }

        /// <summary>
        /// The name of the group
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }

        /// <summary>
        /// The path of the group
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; }

        /// <summary>
        /// The group's description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Prevent adding new members to project membership within this group
        /// </summary>
        [JsonProperty("membership_lock")]
        public bool? MembershipLock { get; set; }

        /// <summary>
        /// Prevent sharing a project with another group within this group
        /// </summary>
        [JsonProperty("share_with_group_lock")]
        public bool? ShareWithGroupLock { get; set; }

        /// <summary>
        /// The group's visibility. Can be private, internal, or public.
        /// </summary>
        [JsonProperty("visibility")]
        public GroupsVisibility? Visibility { get; set; }

        /// <summary>
        /// Enable/disable Large File Storage (LFS) for the projects in this group
        /// </summary>
        [JsonProperty("lfs_enabled")]
        public bool? LfsEnabled { get; set; }

        /// <summary>
        /// Allow users to request member access.
        /// </summary>
        [JsonProperty("request_access_enabled")]
        public bool? RequestAccessEnabled { get; set; }

        /// <summary>
        /// The parent group id for creating nested group.
        /// </summary>
        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }

        /// <summary>
        /// Pipeline minutes quota for this group
        /// </summary>
        [JsonProperty("shared_runners_minutes_limit")]
        public int? SharedRunnersMinutesLimit { get; set; }
    }
}
