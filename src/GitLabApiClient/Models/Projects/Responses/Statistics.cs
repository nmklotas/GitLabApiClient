using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Responses
{
    public sealed class Statistics
    {
        [JsonProperty("job_artifacts_size")]
        public long JobArtifactsSize { get; set; }

        [JsonProperty("repository_size")]
        public long RepositorySize { get; set; }

        [JsonProperty("commit_count")]
        public long CommitCount { get; set; }

        [JsonProperty("lfs_objects_size")]
        public long LfsObjectsSize { get; set; }

        [JsonProperty("storage_size")]
        public long StorageSize { get; set; }
    }
}
