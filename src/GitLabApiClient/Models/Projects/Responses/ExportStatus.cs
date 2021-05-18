using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Responses
{
    public sealed class ExportStatus
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_with_namespace")]
        public string NameWithNamespace { get; set; }

        [JsonProperty("path_with_namespace")]
        public string PathWithNamespace { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("export_status")]
        public ExportStatusEnum Status { get; set; }

        [JsonProperty("_links")]
        public ExportStatusLinks Links { get; set; }
    }
}
