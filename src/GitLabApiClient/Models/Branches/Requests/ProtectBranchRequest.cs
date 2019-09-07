﻿using System;
using System.Collections.Generic;
using System.Text;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Branches.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Branches.Requests
{
    /// <summary>
    /// Protects a branch.
    /// </summary>
    public sealed class ProtectBranchRequest
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectBranchRequest"/> class.
        /// </summary>
        /// <param name="projectId">ID or URL-Encoded path of the project.</param>
        /// <param name="name">The name of the branch or wildcard.</param>
        /// <param name="pushAccessLevel">Access levels allowed to push.</param>
        /// <param name="mergeAccessLevel">Access levels allowed to merge.</param>
        /// <param name="unprotectAccessLevel">Access levels allowed to unprotect.</param>
        public ProtectBranchRequest(
            string projectId, 
            string name, 
            ProtectedRefAccessLevels? pushAccessLevel = ProtectedRefAccessLevels.MAINTAINER_ACCESS, 
            ProtectedRefAccessLevels? mergeAccessLevel = ProtectedRefAccessLevels.MAINTAINER_ACCESS, 
            ProtectedRefAccessLevels? unprotectAccessLevel = ProtectedRefAccessLevels.MAINTAINER_ACCESS)
        {
            Guard.NotEmpty(projectId, nameof(projectId));
            Guard.NotEmpty(name, nameof(name));

            PushAccessLevel = pushAccessLevel.ToString();
            MergeAccessLevel = mergeAccessLevel.ToString();
            UnprotectAccessLevel = unprotectAccessLevel.ToString();
        }

        /// <summary>
        /// Id or URL-Encoded path of the project.
        /// </summary>
        [JsonProperty("id")]
        public string ProjectId { get; set; }

        /// <summary>
        /// The name of the branch or wildcard.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Access levels allowed to push (defaults: 40, maintainer access level).
        /// </summary>
        [JsonProperty("push_access_level")]
        public string PushAccessLevel { get; set; };

        /// <summary>
        /// Access levels allowed to merge (defaults: 40, maintainer access level).
        /// </summary>
        [JsonProperty("merge_access_level")]
        public string MergeAccessLevel { get; set; }

        /// <summary>
        /// Access levels allowed to unprotect (defaults: 40, maintainer access level).
        /// </summary>
        [JsonProperty("unprotect_access_level")]
        public string UnprotectAccessLevel { get; set; };
    }
}