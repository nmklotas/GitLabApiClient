using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Models.Issues.Requests;

namespace GitLabApiClient.Models.Groups.Requests
{
    public class EpicsGroupQueryOptions
    {
        internal EpicsGroupQueryOptions()
        {
        }

        /// <summary>
        /// Return epics created by the given user id
        /// </summary>
        public int? AuthorId { get; set; }

        /// <summary>
        /// Return epics matching a comma separated list of labels names. Label
        /// names from the epic group or a parent group can be used 
        /// </summary>
        public IList<string> Labels { get; set; } = new List<string>();

        /// <summary>
        /// If true, response returns more details for each label in labels field: :name,
        /// :color, :description, :description_html, :text_color. Default is false. 
        /// </summary>
        public bool WithLabelsDetails { get; set; }

        /// <summary>
        /// Return epics ordered by created_at or updated_at fields. Default is created_at
        /// </summary>
        public EpicsIssuesOrder Order { get; set; }

        /// <summary>
        /// Return epics sorted in asc or desc order. Default is desc
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// Search epics against their title and description
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Search epics against their state, possible filters: opened, closed and all, default: all
        /// </summary>
        public EpicIssueState State { get; set; }

        /// <summary>
        /// Return epics created on or after the given time (inclusive)
        /// </summary>
        public DateTime? CreatedAfter { get; set; }

        /// <summary>
        /// Return epics created before the given time (inclusive)
        /// </summary>
        public DateTime? CreatedBefore { get; set; }

        /// <summary>
        /// Return epics updated on or after the given time
        /// </summary>
        public DateTime? UpdatedAfter { get; set; }

        /// <summary>
        /// Return epics updated on or before the given time
        /// </summary>
        public DateTime? UpdatedBefore { get; set; }

        /// <summary>
        /// Include epics from the requested group’s ancestors.
        /// Default is false
        /// </summary>
        public bool IncludeAncestorGroups { get; set; }

        /// <summary>
        /// Include epics from the requested group’s descendants.
        /// Default is true
        /// </summary>
        public bool IncludeDescendantGroups { get; set; }
    }
}
