using GitLabApiClient.Internal.Paths;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Groups.Requests
{
    /// <summary>
    /// Used to add members in a group.
    /// </summary>
    public sealed class AddGroupMemberRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddGroupMemberRequest"/> class.
        /// </summary>
        /// <param name="accessLevel">The access level of the new member.</param>
        public AddGroupMemberRequest(AccessLevel accessLevel, UserId userId)
        {
            AccessLevel = (int)accessLevel;
            UserId = System.Convert.ToInt32(userId.ToString());
        }

        /// <summary>
        /// The desired access level
        /// </summary>
        [JsonProperty("access_level")]
        public int AccessLevel { get; private set; }

        /// <summary>
        /// The UserID to add
        /// </summary>
        [JsonProperty("user_id")]
        public int UserId { get; private set; }

        /// <summary>
        /// The membership expiration date. Date time string in the format YEAR-MONTH-DAY, e.g. 2016-03-11.
        /// </summary>
        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }
    }
}
