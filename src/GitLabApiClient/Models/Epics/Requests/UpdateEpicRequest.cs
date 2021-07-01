using System;
using System.Collections.Generic;
using GitLabApiClient.Internal.Http.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GitLabApiClient.Models.Epics.Requests
{
    /// <summary>
    /// Used to update epics in a group.
    /// </summary>
    public sealed class UpdateEpicRequest
    {
        /// <summary>
        /// The title of an epic.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// The description of an epic.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Updates an epic to be confidential.
        /// </summary>
        [JsonProperty("confidential")]
        public bool? Confidential { get; set; }

        /// <summary>
        /// Label names for an epic.
        /// </summary>
        [JsonProperty("labels")]
        [JsonConverter(typeof(CollectionToCommaSeparatedValuesConverter))]
        public IList<string> Labels { get; set; } = new List<string>();

        /// <summary>
        /// The state event of an epic. Set close to close the epic and reopen to reopen it.
        /// </summary>
        [JsonProperty("state_event")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UpdatedEpicIssueState? State { get; set; }

        /// <summary>
        /// Date time string, ISO 8601 formatted, e.g. 2016-03-11T03:45:40Z (requires admin or project owner rights).
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Whether start date should be sourced from start_date_fixed or from milestones
        /// </summary>
        [JsonProperty("start_date_is_fixed")]
        public bool? StartDateIsFixed { get; set; }

        /// <summary>
        /// Date time string in the format YEAR-MONTH-DAY, e.g. 2016-03-11.
        /// </summary>
        [JsonProperty("start_date_fixed")]
        public string StartDateFixed { get; set; }

        /// <summary>
        /// Whether due date should be sourced from due_date_fixed or from milestones
        /// </summary>
        [JsonProperty("due_date_is_fixed")]
        public bool? DueDateIsFixed { get; set; }

        /// <summary>
        /// Date time string in the format YEAR-MONTH-DAY, e.g. 2016-03-11.
        /// </summary>
        [JsonProperty("due_date_fixed")]
        public string DueDateFixed { get; set; }

    }
}
