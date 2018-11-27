using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Groups.Requests
{
    /// <summary>
    /// Used to create milestones in a groups.
    /// </summary>
    public sealed class CreateGroupMilestoneRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGroupMilestoneRequest"/> class.
        /// </summary>
        /// <param name="groupId">The ID or URL-encoded path of the group owned by the authenticated user.</param>
        /// <param name="title">The title of a milestone.</param>
        public CreateGroupMilestoneRequest(string groupId, string title)
        {
            Guard.NotEmpty(groupId, nameof(groupId));
            Guard.NotEmpty(title, nameof(title));
            GroupId = groupId;
            Title = title;
        }

        /// <summary>
        /// The ID or URL-encoded path of the group owned by the authenticated user.
        /// </summary>
        [JsonProperty("id")]
        public string GroupId { get; private set; }

        /// <summary>
        /// The title of a milestone.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; private set; }

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
    }
}
