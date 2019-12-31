using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Files.Responses
{
    public sealed class File
    {
        [JsonProperty("file_name")]
        public string Filename { get; set; }

        [JsonProperty("file_path")]
        public string FullPath { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("encoding")]
        public string Encoding { get; set; }

        [JsonProperty("content_sha256")]
        public string ContentSha256 { get; set; }

        [JsonProperty("ref")]
        public string Reference { get; set; }

        [JsonProperty("blob_id")]
        public string BlobId { get; set; }

        [JsonProperty("commit_id")]
        public string CommitId { get; set; }

        [JsonProperty("last_commit_id")]
        public string LastCommitId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        public string ContentDecoded => System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Content));
    }
}
