using System;
using Newtonsoft.Json;


namespace GitLabApiClient.Models.Packages.Responses
{
    public sealed class Package
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("package_type")]
        public string PackageType { get; set; }

    }

    public sealed class PackageFile
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("package_id")]
        public string PackageId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("file_md5")]
        public string FileMD5 { get; set; }

        [JsonProperty("file_sha1")]
        public string FileSHA1 { get; set; }

        [JsonProperty("file_sha256")]
        public string FileSHA256 { get; set; }

    }
}
