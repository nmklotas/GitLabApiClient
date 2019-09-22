using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Responses
{
    public sealed class Statistics
    {
        [JsonProperty("job_artifacts_size")]
        public int JobArtifactsSize { get; set; }

        [JsonProperty("repository_size")]
        public int RepositorySize { get; set; }

        [JsonProperty("commit_count")]
        public int CommitCount { get; set; }

        [JsonProperty("lfs_objects_size")]
        public int LfsObjectsSize { get; set; }

        [JsonProperty("storage_size")]
        public int StorageSize { get; set; }
    }
}
