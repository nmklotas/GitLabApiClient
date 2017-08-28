using System.Collections.Generic;
using GitLabApiClient.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects
{
    public class CreateProjectRequest
    {
        public static CreateProjectRequest FromPath(string path)
        {
            Guard.NotEmpty(path, nameof(path));
            return new CreateProjectRequest
            {
                Path = path
            };
        }

        public static CreateProjectRequest FromName(string name)
        {
            Guard.NotEmpty(name, nameof(name));
            return new CreateProjectRequest
            {
                Name = name
            };
        }

        private CreateProjectRequest() { }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("path")]
        public string Path { get; private set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("default_branch")]
        public string DefaultBranch { get; set; }

        [JsonProperty("namespace_id")]
        public int NamespaceId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("issues_enabled")]
        public bool EnableIssues { get; set; }

        [JsonProperty("merge_requests_enabled")]
        public bool EnableMergeRequests { get; set; }

        [JsonProperty("jobs_enabled")]
        public bool EnableJobs { get; set; }

        [JsonProperty("wiki_enabled")]
        public bool EnableWiki { get; set; }

        [JsonProperty("snippets_enabled")]
        public bool EnableSnippets { get; set; }

        [JsonProperty("container_registry_enabled")]
        public bool EnableContainerRegistry { get; set; }

        [JsonProperty("shared_runners_enabled")]
        public bool EnableSharedRunners { get; set; }

        [JsonProperty("visibility")]
        public VisibilityLevel Visibility { get; set; }

        [JsonProperty("import_url")]
        public string ImportUrl { get; set; }

        [JsonProperty("public_jobs")]
        public bool PublicJobs { get; set; }

        [JsonProperty("only_allow_merge_if_pipeline_succeeds")]
        public bool? OnlyAllowMergeIfPipelineSucceeds { get; set; }

        [JsonProperty("only_allow_merge_if_all_discussions_are_resolved")]
        public bool? OnlyAllowMergeIfAllDiscussionsAreResolved { get; set; }

        [JsonProperty("lfs_enabled")]
        public bool EnableLfs { get; set; }

        [JsonProperty("request_access_enabled")]
        public bool EnableRequestAccess { get; set; }

        [JsonProperty("tag_list")]
        public List<string> Tags { get; } = new List<string>();

        [JsonProperty("printing_merge_request_link_enabled")]
        public bool EnablePrintingMergeRequestLink { get; set; }

        [JsonProperty("ci_config_path")]
        public string CiConfigPath { get; set; }
    }
}
