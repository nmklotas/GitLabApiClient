using GitLabApiClient.Internal.Utilities;
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
        public AddGroupMemberRequest(AccessLevel accessLevel) => AccessLevel = (int) accessLevel;

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
