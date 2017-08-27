using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects
{
    public class Group
    {
        [JsonProperty("full_path")]
        public string FullPath { get; set; }

        [JsonProperty("parent_id")]
        public object ParentId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("lfs_enabled")]
        public bool LfsEnabled { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("request_access_enabled")]
        public bool RequestAccessEnabled { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("web_url")]
        public string WebUrl { get; set; }
    }
}
