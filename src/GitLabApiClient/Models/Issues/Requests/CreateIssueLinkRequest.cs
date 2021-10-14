using System;
using System.Collections.Generic;
using GitLabApiClient.Internal.Http.Serialization;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Issues.Requests
{
    /// <summary>
    /// Used to create a two way link between issues
    /// </summary>
    public sealed class CreateIssueLinkRequest
    {
        public CreateIssueLinkRequest(string targetProjectID, int targetIssueID)
        {
            TargetProjectID = targetProjectID;
            TargetIssueID = targetIssueID;
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("target_project_id")]
        public string TargetProjectID { get;  }

        /// <summary>
        /// The internal ID of a target project’s issue
        /// </summary>
        [JsonProperty("target_issue_iid")]
        public int TargetIssueID { get;  }

        public enum LinkTypes
        {
            relates_to,
            blocks,
            is_blocked_by
        }
        /// <summary>
        /// The type of the relation (“relates_to”, “blocks”, “is_blocked_by”), defaults to “relates_to”)
        /// </summary>
        [JsonProperty("link_type")]
        public LinkTypes Linktype { get; set; }

    }
}
