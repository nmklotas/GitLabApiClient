using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Groups.Requests
{
    /// <summary>
    /// Used to update members in a group.
    /// </summary>
    public sealed class UpdateGroupMemberRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGroupMemberRequest"/> class.
        /// </summary>
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="userId">The id of the user to add as member.</param>
        /// <param name="accessLevel">The access level of the new member.</param>
        public UpdateGroupMemberRequest(string groupId, int userId, AccessLevel accessLevel)
        {
            Guard.NotEmpty(groupId, nameof(groupId));
            GroupId = groupId;
            UserId = userId;
            AccessLevel = (int)accessLevel;
        }

        /// <summary>
        /// The ID or URL-encoded path of the group owned by the authenticated user.
        /// </summary>
        [JsonProperty("id")]
        public string GroupId { get; private set; }

        /// <summary>
        /// The id of the user.
        /// </summary>
        [JsonProperty("user_id")]
        public int UserId { get; private set; }

        /// <summary>
        /// The desired access level
        /// </summary>
        [JsonProperty("access_level")]
        public int AccessLevel { get; private set; }

        /// <summary>
        /// The membership expiration date. Date time string in the format YEAR-MONTH-DAY, e.g. 2016-03-11.
        /// </summary>
        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }
    }
}
