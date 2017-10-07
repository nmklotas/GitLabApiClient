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

        [JsonProperty("lfs_enabled")]
        public bool LfsEnabled { get; set; }

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

        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }

        [JsonProperty("projects")]
        public IList<Project> Projects { get; } = new List<Project>();
    }
}
