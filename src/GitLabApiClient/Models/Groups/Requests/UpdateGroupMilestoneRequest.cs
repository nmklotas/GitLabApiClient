using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Milestones.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GitLabApiClient.Models.Groups.Requests
{
    /// <summary>
    /// Updates an existing group milestone.
    /// </summary>
    public sealed class UpdateGroupMilestoneRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGroupMilestoneRequest"/> class.
        /// </summary>
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="milestoneId">The ID of a group milestone.</param>
        public UpdateGroupMilestoneRequest(string groupId, int milestoneId)
        {
            Guard.NotEmpty(groupId, nameof(groupId));
            GroupId = groupId;
            MilestoneId = milestoneId;
        }

        /// <summary>
        /// The ID or URL-encoded path of the group owned by the authenticated user.
        /// </summary>
        [JsonProperty("id")]
        public string GroupId { get; private set; }

        /// <summary>
        /// The ID of a group milestone.
        /// </summary>
        [JsonProperty("milestone_id")]
        public int MilestoneId { get; private set; }

        /// <summary>
        /// The title of a milestone.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// The description of the milestone
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The due date of the milestone. Date time string in the format YEAR-MONTH-DAY, e.g. 2016-03-11.
        /// </summary>
        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        /// <summary>
        /// The start date of the milestone. Date time string in the format YEAR-MONTH-DAY, e.g. 2016-03-11.
        /// </summary>
        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        /// <summary>
        /// The state event of the milestone (close, active)
        /// </summary>
        [JsonProperty("state_event")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UpdatedMilestoneState? State { get; set; }
    }
}
