using Newtonsoft.Json;

namespace GitLabApiClient.Models.Groups.Responses
{
    public sealed class Statistics
    {
        [JsonProperty("storage_size")]
        public int? StorageSize { get; set; }

        [JsonProperty("repository_size")]
        public int? RepositorySize { get; set; }

        [JsonProperty("wiki_size")]
        public int? WikiSize { get; set; }

        [JsonProperty("lfs_objects_size")]
        public int? LfsObjectsSize { get; set; }

        [JsonProperty("job_artifacts_size")]
        public int? JobArtifactsSize { get; set; }

        [JsonProperty("packages_size")]
        public int? PackagesSize { get; set; }

        [JsonProperty("snippets_size")]
        public int? SnippetsSize { get; set; }
    }
}
