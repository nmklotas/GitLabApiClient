using System.Collections.Generic;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Projects.Responses;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Groups.Responses
{
    public sealed class Group
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("visibility")]
        public GroupsVisibility Visibility { get; set; }

        [JsonProperty("share_with_group_lock")]
        public bool? ShareWithGroupLock { get; set; }

        [JsonProperty("require_two_factor_authentication")]
        public bool? RequireTwoFactorAuthentication { get; set; }

        [JsonProperty("two_factor_grace_period")]
        public int? TwoFactorGracePeriod { get; set; }

        [JsonProperty("project_creation_level")]
        public ProjectCreationLevel? ProjectCreationLevel { get; set; }

        [JsonProperty("auto_devops_enabled")]
        public bool? AutoDevOpsEnabled { get; set; }

        [JsonProperty("subgroup_creation_level")]
        public SubgroupCreationLevel? SubgroupCreationLevel { get; set; }

        [JsonProperty("emails_disabled")]
        public bool? EmailsDisabled { get; set; }

        [JsonProperty("mentions_disabled")]
        public bool? MentionsDisabled { get; set; }

        [JsonProperty("lfs_enabled")]
        public bool LfsEnabled { get; set; }

        [JsonProperty("default_branch_protection")]
        public int? DefaultBranchProtection { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }

        [JsonProperty("request_access_enabled")]
        public bool RequestAccessEnabled { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("full_path")]
        public string FullPath { get; set; }

        [JsonProperty("file_template_project_id")]
        public int? FileTemplateProjectId { get; set; }

        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }

        [JsonProperty("created_at")]
        public System.DateTime? CreatedAt { get; set; }

        [JsonProperty("projects")]
        public IList<Project> Projects { get; } = new List<Project>();

        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }
    }
}
