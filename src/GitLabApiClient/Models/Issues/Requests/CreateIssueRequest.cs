using System;
using System.Collections.Generic;
using GitLabApiClient.Internal.Http.Serialization;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Issues.Requests
{
    /// <summary>
    /// Used to create issues in a project.
    /// </summary>
    public sealed class CreateIssueRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateIssueRequest"/> class.
        /// </summary>
        /// <param name="title">Title of the issue.</param>
        public CreateIssueRequest(string title)
        {
            Guard.NotEmpty(title, nameof(title));
            Title = title;
        }

        /// <summary>
        /// The title of an issue.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; }

        /// <summary>
        /// The description of an issue.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Set an issue to be confidential. Default is false.
        /// </summary>
        [JsonProperty("confidential")]
        public bool? Confidential { get; set; }

        /// <summary>
        /// The IDs of the users to assign issue.
        /// </summary>
        [JsonProperty("assignee_ids")]
        public List<int> Assignees { get; set; } = new List<int>();

        /// <summary>
        /// The ID of a milestone to assign issue.
        /// </summary>
        [JsonProperty("milestone_id")]
        public int? MilestoneId { get; set; }

        /// <summary>
        /// Label names for an issue.
        /// </summary>
        [JsonProperty("labels")]
        [JsonConverter(typeof(CollectionToCommaSeparatedValuesConverter))]
        public IList<string> Labels { get; set; } = new List<string>();

        /// <summary>
        /// Date time string in the format YEAR-MONTH-DAY, e.g. 2016-03-11.
        /// </summary>
        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        /// <summary>
        /// Date time string, ISO 8601 formatted, e.g. 2016-03-11T03:45:40Z (requires admin or project owner rights).
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The IID of a merge request in which to resolve all issues.
        /// This will fill the issue with a default description and mark all discussions as resolved.
        /// When passing a description or title, these values will take precedence over the default values.
        /// </summary>
        [JsonProperty("merge_request_to_resolve_discussions_of")]
        public int? MergeRequestIdToResolveDiscussions { get; set; }

        /// <summary>
        /// The ID of a discussion to resolve.
        /// This will fill in the issue with a default description and mark the discussion as resolved.
        ///  Use in combination with <see cref="MergeRequestIdToResolveDiscussions"/>
        /// </summary>
        [JsonProperty("discussion_to_resolve")]
        public int? DiscussionToResolveId { get; set; }

        /// <summary>
        /// The weight of an issue. Valid values are greater than or equal to 0.
        /// </summary>
        [JsonProperty("weight")]
        public int? Weight { get; set; }
    }
}
