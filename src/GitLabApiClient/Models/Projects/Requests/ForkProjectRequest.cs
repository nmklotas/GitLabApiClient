using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Requests
{
    public class ForkProjectRequest
    {
        public ForkProjectRequest(int id) => Id = id;

        /// <summary>
        /// The ID or URL-encoded path of the project.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; private set; }

        /// <summary>
        /// The name assigned to the resultant project after forking.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// The ID of the namespace that the project is forked to.
        /// </summary>
        [JsonProperty("namespace_id")]
        public int? NamespaceId { get; set; }

        /// <summary>
        /// The path of the namespace that the project is forked to.
        /// </summary>
        [JsonProperty("namespace_path")]
        public string NamespacePath { get; set; }

        /// <summary>
        /// (Deprecated) The ID or path of the namespace that the project is forked to.
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// The path assigned to the resultant project after forking.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// The description assigned to the resultant project after forking.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The visibility level assigned to the resultant project after forking.
        /// </summary>
        [JsonProperty("visibility")]
        public bool? Visibility { get; set; }

    }
}
