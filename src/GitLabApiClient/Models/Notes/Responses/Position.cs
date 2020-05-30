using Newtonsoft.Json;

namespace GitLabApiClient.Models.Notes.Responses
{
    public sealed class Position
    {
        [JsonProperty("base_sha")]
        public string BaseSha { get; set; }

        [JsonProperty("start_sha")]
        public string StartSha { get; set; }

        [JsonProperty("head_sha")]
        public string HeadSha { get; set; }

        [JsonProperty("position_type")]
        public PositionType PositionType { get; set; }

        [JsonProperty("old_path")]
        public string OldPath { get; set; }

        [JsonProperty("new_path")]
        public string NewPath { get; set; }

        [JsonProperty("old_line")]
        public int? OldLine { get; set; }

        [JsonProperty("new_line")]
        public int? NewLine { get; set; }

    }
}
