using System.Collections.Generic;
using GitLabApiClient.Models.Projects.Requests;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Responses
{
    public sealed class Project
    {
        [JsonProperty("last_activity_at")]
        public string LastActivityAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("container_registry_enabled")]
        public bool ContainerRegistryEnabled { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("creator_id")]
        public int CreatorId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("default_branch")]
        public string DefaultBranch { get; set; }

        [JsonProperty("import_error")]
        public string ImportError { get; set; }

        [JsonProperty("http_url_to_repo")]
        public string HttpUrlToRepo { get; set; }

        [JsonProperty("forks_count")]
        public int ForksCount { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("issues_enabled")]
        public bool IssuesEnabled { get; set; }

        [JsonProperty("import_status")]
        public string ImportStatus { get; set; }

        [JsonProperty("jobs_enabled")]
        public bool JobsEnabled { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("namespace")]
        public Namespace Namespace { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("merge_requests_enabled")]
        public bool MergeRequestsEnabled { get; set; }

        [JsonProperty("name_with_namespace")]
        public string NameWithNamespace { get; set; }

        [JsonProperty("only_allow_merge_if_pipeline_succeeds")]
        public bool? OnlyAllowMergeIfPipelineSucceeds { get; set; }

        [JsonProperty("only_allow_merge_if_all_discussions_are_resolved")]
        public bool? OnlyAllowMergeIfAllDiscussionsAreResolved { get; set; }

        [JsonProperty("open_issues_count")]
        public int OpenIssuesCount { get; set; }

        [JsonProperty("public_jobs")]
        public bool PublicJobs { get; set; }

        [JsonProperty("path_with_namespace")]
        public string PathWithNamespace { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("permissions")]
        public Permissions Permissions { get; set; }

        [JsonProperty("runners_token")]
        public string RunnersToken { get; set; }

        [JsonProperty("request_access_enabled")]
        public bool RequestAccessEnabled { get; set; }

        [JsonProperty("shared_runners_enabled")]
        public bool SharedRunnersEnabled { get; set; }

        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }

        [JsonProperty("ssh_url_to_repo")]
        public string SshUrlToRepo { get; set; }

        [JsonProperty("snippets_enabled")]
        public bool SnippetsEnabled { get; set; }

        [JsonProperty("star_count")]
        public int StarCount { get; set; }

        [JsonProperty("visibility")]
        public ProjectVisibilityLevel Visibility { get; set; }

        [JsonProperty("tag_list")]
        public List<string> TagList { get; } = new List<string>();

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }

        [JsonProperty("wiki_enabled")]
        public bool WikiEnabled { get; set; }
    }
}
