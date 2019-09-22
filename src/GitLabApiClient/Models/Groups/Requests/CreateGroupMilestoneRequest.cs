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
        /// <param name="title">The title of a milestone.</param>
        public CreateGroupMilestoneRequest(string title)
        {
            Guard.NotEmpty(title, nameof(title));
            Title = title;
        }

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
