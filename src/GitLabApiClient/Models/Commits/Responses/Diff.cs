using Newtonsoft.Json;

namespace GitLabApiClient.Models.Commits.Responses
{
    public sealed class Diff
    {
        [JsonProperty("diff")]
        public string DiffText { get; set; }

        [JsonProperty("new_path")]
        public string NewPath { get; set; }

        [JsonProperty("old_path")]
        public string OldPath { get; set; }

        [JsonProperty("a_mode")]
        public string AMode { get; set; }

        [JsonProperty("b_mode")]
        public string BMode { get; set; }

        [JsonProperty("new_file")]
        public bool IsNewFile { get; set; }

        [JsonProperty("renamed_file")]
        public bool IsRenamedFile { get; set; }

        [JsonProperty("deleted_file")]
        public bool IsDeletedFile { get; set; }
    }
}
