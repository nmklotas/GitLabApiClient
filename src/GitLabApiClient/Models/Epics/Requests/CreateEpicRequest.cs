using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Http.Serialization;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Epics.Requests
{
    /// <summary>
    /// Used to create epics in a group.
    /// </summary>
    public sealed class CreateEpicRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEpicRequest"/> class.
        /// </summary>
        /// <param name="title">Title of the epic.</param>
        public CreateEpicRequest(string title)
        {
            Guard.NotEmpty(title, nameof(title));
            Title = title;
        }

        /// <summary>
        /// The title of an epic.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; }

        /// <summary>
        /// The comma separated list of labels 
        /// </summary>
        [JsonProperty("labels")]
        [JsonConverter(typeof(CollectionToCommaSeparatedValuesConverter))]
        public IList<string> Labels { get; set; } = new List<string>();

        /// <summary>
        /// The description of the epic. Limited to 1,048,576 characters. 
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Whether the epic should be confidential 
        /// </summary>
        [JsonProperty("confidential")]
        public bool? Confidential { get; set; }

        /// <summary>
        /// When the epic was created. Date time string, ISO 8601 formatted,
        /// for example 2016-03-11T03:45:40Z (requires admin or project owner rights).
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Whether start date should be sourced from start_date_fixed or from milestones.
        /// </summary>
        [JsonProperty("start_date_is_fixed")]
        public bool StartDateIsFixed { get; set; }

        /// <summary>
        /// The fixed start date of an epic.
        /// </summary>
        [JsonProperty("start_date_fixed")]
        public string StartDateFixed { get; set; }

        /// <summary>
        /// Whether due date should be sourced from due_date_fixed or from milestones.
        /// </summary>
        [JsonProperty("due_date_is_fixed")]
        public bool DueDateIsFixed { get; set; }

        /// <summary>
        /// The fixed due date of an epic.
        /// </summary>
        [JsonProperty("due_date_fixed")]
        public string DueDateFixed { get; set; }

        /// <summary>
        /// The ID of a parent epic.
        /// </summary>
        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }
    }
}
