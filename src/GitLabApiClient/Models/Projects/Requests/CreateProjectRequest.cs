using System.Collections.Generic;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Requests
{
    /// <summary>
    /// Creates a new project owned by the authenticated user.
    /// If <see cref="UserId"/> is specified then it's created on behalf of that user. (Available for admins only).
    /// </summary>
    public sealed class CreateProjectRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProjectRequest"/> class.
        /// <param name="path">Repository name for new project.</param>
        /// </summary>
        public static CreateProjectRequest FromPath(string path)
        {
            Guard.NotEmpty(path, nameof(path));
            return new CreateProjectRequest
            {
                Path = path
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProjectRequest"/> class.
        /// <param name="name">The name of the new project.</param>
        /// </summary>
        public static CreateProjectRequest FromName(string name)
        {
            Guard.NotEmpty(name, nameof(name));
            return new CreateProjectRequest
            {
                Name = name
            };
        }

        private CreateProjectRequest() { }

        /// <summary>
        /// The name of the new project.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Custom repository name for new project. By default generated based on name.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; private set; }

        /// <summary>
        /// The user ID of the project owner.
        /// </summary>
        [JsonProperty("user_id")]
        public int? UserId { get; set; }

        /// <summary>
        /// master by default.
        /// </summary>
        [JsonProperty("default_branch")]
        public string DefaultBranch { get; set; }

        /// <summary>
        /// Namespace for the new project (defaults to the current user's namespace).
        /// </summary>
        [JsonProperty("namespace_id")]
        public int? NamespaceId { get; set; }

        /// <summary>
        /// Short project description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Enable issues for this project.
        /// </summary>
        [JsonProperty("issues_enabled")]
        public bool? EnableIssues { get; set; }

        /// <summary>
        /// Enable merge requests for this project.
        /// </summary>
        [JsonProperty("merge_requests_enabled")]
        public bool? EnableMergeRequests { get; set; }

        /// <summary>
        /// Enable jobs for this project.
        /// </summary>
        [JsonProperty("jobs_enabled")]
        public bool? EnableJobs { get; set; }

        /// <summary>
        /// Enable wiki for this project.
        /// </summary>
        [JsonProperty("wiki_enabled")]
        public bool? EnableWiki { get; set; }

        /// <summary>
        /// Enable snippets for this project.
        /// </summary>
        [JsonProperty("snippets_enabled")]
        public bool? EnableSnippets { get; set; }

        /// <summary>
        /// Enable container registry for this project.
        /// </summary>
        [JsonProperty("container_registry_enabled")]
        public bool? EnableContainerRegistry { get; set; }

        /// <summary>
        /// Enable shared runners for this project.
        /// </summary>
        [JsonProperty("shared_runners_enabled")]
        public bool? EnableSharedRunners { get; set; }

        /// <summary>
        /// Project visibility level.
        /// </summary>
        [JsonProperty("visibility")]
        public ProjectVisibilityLevel? Visibility { get; set; }

        /// <summary>
        /// URL to import repository from.
        /// </summary>
        [JsonProperty("import_url")]
        public string ImportUrl { get; set; }

        /// <summary>
        /// If set, jobs can be viewed by non-project-members.
        /// </summary>
        [JsonProperty("public_jobs")]
        public bool? PublicJobs { get; set; }

        /// <summary>
        /// Set whether merge requests can only be merged with successful jobs.
        /// </summary>
        [JsonProperty("only_allow_merge_if_pipeline_succeeds")]
        public bool? OnlyAllowMergeIfPipelineSucceeds { get; set; }

        /// <summary>
        /// Set whether merge requests can only be merged when all the discussions are resolved.
        /// </summary>
        [JsonProperty("only_allow_merge_if_all_discussions_are_resolved")]
        public bool? OnlyAllowMergeIfAllDiscussionsAreResolved { get; set; }

        /// <summary>
        /// Enable LFS.
        /// </summary>
        [JsonProperty("lfs_enabled")]
        public bool? EnableLfs { get; set; }

        /// <summary>
        /// Allow users to request member access.
        /// </summary>
        [JsonProperty("request_access_enabled")]
        public bool? EnableRequestAccess { get; set; }

        /// <summary>
        /// The list of tags for a project; put array of tags, that should be finally assigned to a project.
        /// </summary>
        [JsonProperty("tag_list")]
        public List<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// Show link to create/view merge request when pushing from the command line.
        /// </summary>
        [JsonProperty("printing_merge_request_link_enabled")]
        public bool? EnablePrintingMergeRequestLink { get; set; }

        /// <summary>
        /// The path to CI config file.
        /// </summary>
        [JsonProperty("ci_config_path")]
        public string CiConfigPath { get; set; }
    }
}
